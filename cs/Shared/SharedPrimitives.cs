namespace aoc24.Shared;

public readonly record struct Point(int X, int Y)
{
    public static readonly Point Zero = new();

    public Point? MoveWithin(Point howMuch, int width, int height)
    {
        var newPoint = this + howMuch;
        return newPoint.X >= width || newPoint.Y >= height ? null : newPoint;
    }

    public static Point FromDirection(Direction direction)
    {
        var point = new Point(0, 0);
        if (direction.HasFlag(Direction.Up))
            point += new Point(0, -1);
        if (direction.HasFlag(Direction.Down))
            point += new Point(0, 1);
        if (direction.HasFlag(Direction.Right))
            point += new Point(1, 0);
        if (direction.HasFlag(Direction.Left))
            point += new Point(-1, 0);
        return point;
    }

    public static Point FromString(ReadOnlySpan<char> pointString)
    {
        int sepInx = pointString.IndexOf(',');
        int x = int.Parse(pointString[..sepInx]);
        int y = int.Parse(pointString[(sepInx + 1)..]);
        return new(x, y);
    }

    public bool IsBetween(Point start, Point end) => X >= start.X && X <= end.X && Y >= start.Y && Y <= end.Y;

    public static Point operator +(Point first, Point other) => new(first.X + other.X, first.Y + other.Y);

    public static Point operator -(Point first, Point other) => new(first.X - other.X, first.Y - other.Y);

    public static Point operator *(Point first, Point other) => new(first.X * other.X, first.Y * other.Y);

    public static Point operator *(Point point, int value) => point * new Point(value, value);

    public static bool operator >(Point a, Point b) => a.X > b.X || a.Y > b.Y;

    public static bool operator <(Point a, Point b) => a.X < b.X && a.Y < b.Y;
}

public readonly record struct FacingPoint(Direction Direction, Point Position)
{
    public FacingPoint(Direction Direction, int X, int Y)
        : this(Direction, new(X, Y)) { }

    public FacingPoint? MoveWithin(Point howMuch, int width, int height)
    {
        var newPosition = Position.MoveWithin(howMuch, width, height);

        if (newPosition is null)
            return null;

        return new(Direction, newPosition.Value);
    }

    public FacingPoint MoveForward() => this + Point.FromDirection(Direction);

    public FacingPoint? MoveForward(int width, int height)
    {
        var howMuch = Point.FromDirection(Direction);
        return MoveWithin(howMuch, width, height);
    }

    public readonly FacingPoint Rotate(Direction relativeDirection) =>
        (Direction, relativeDirection) switch
        {
            (Direction.Right, Direction.Right) => new(Direction.Down, Position),
            (Direction.Right, Direction.Left) => new(Direction.Up, Position),
            (Direction.Right, Direction.Down) => new(Direction.Left, Position),
            (Direction.Left, Direction.Right) => new(Direction.Up, Position),
            (Direction.Left, Direction.Left) => new(Direction.Down, Position),
            (Direction.Left, Direction.Down) => new(Direction.Right, Position),
            (Direction.Down, Direction.Right) => new(Direction.Left, Position),
            (Direction.Down, Direction.Left) => new(Direction.Right, Position),
            (Direction.Down, Direction.Down) => new(Direction.Up, Position),
            (Direction.Up, Direction.Right) => new(Direction.Right, Position),
            (Direction.Up, Direction.Left) => new(Direction.Left, Position),
            (Direction.Up, Direction.Down) => new(Direction.Down, Position),
            _ => this
        };

    public static FacingPoint operator +(FacingPoint first, Point other) =>
        new(first.Direction, first.Position.X + other.X, first.Position.Y + other.Y);

    public static FacingPoint operator -(FacingPoint first, Point other) =>
        new(first.Direction, first.Position.X - other.X, first.Position.Y - other.Y);
}

public readonly ref struct Matrix<T>
    where T : struct
{
    private Span<T> Inner { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }

    public static Matrix<char> CreateFrom(ReadOnlySpan<char> input)
    {
        var inner = new char[SizeFor(input)];
        return CreateFrom(input, inner);
    }

    public static Matrix<char> CreateFrom(ReadOnlySpan<char> input, Span<char> innerSpan)
    {
        int width = input.IndexOf(InputReader.NewLine);

        if (width == 0)
            throw new ArgumentException("No lines provided in input for matrix");

        int height = input.Count(InputReader.NewLine) + 1;

        if (innerSpan.Length != width * height)
            throw new ArgumentException($"Incorrectly sized inner span for matrix, expected size {width * height}");

        var matrix = new Matrix<char>()
        {
            Width = width,
            Height = height,
            Inner = innerSpan
        };

        var enumerator = input.Split(InputReader.NewLine);

        int y = 0;
        while (enumerator.MoveNext())
        {
            var line = input[enumerator.Current];

            if (line.Length != matrix.Width)
                throw new InvalidDataException("Equal width rows are required to create a matrix");

            for (int x = 0; x < line.Length; x++)
                innerSpan[(y * matrix.Width) + x] = line[x];

            y++;
        }

        return matrix;
    }

    public static Matrix<T> CreateEmpty(int width, int height, Span<T> innerSpan, T? fill = null)
    {
        if (innerSpan.Length != width * height)
            throw new ArgumentException($"Incorrectly sized inner span for matrix, expected size {width * height}");

        if (fill is not null)
            innerSpan.Fill(fill.Value);

        var matrix = new Matrix<T>()
        {
            Width = width,
            Height = height,
            Inner = innerSpan
        };
        return matrix;
    }

    public static Matrix<T> CreateEmpty(int width, int height, T fill = default) =>
        CreateEmpty(width, height, new T[width * height], fill);

    public static int SizeFor(ReadOnlySpan<char> input)
    {
        int width = input.IndexOf(InputReader.NewLine);

        if (width == 0)
            return 0;

        int height = input.Count(InputReader.NewLine) + 1;
        return width * height;
    }

    public readonly T? ItemAt(Point position) => ItemAt(position.X, position.Y);

    public readonly T? ItemAt(int x, int y)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
            return null;

        return Inner[(y * Width) + x];
    }

    public readonly void ReplaceAt(Point position, T newItem) => ReplaceAt(position.X, position.Y, newItem);

    public readonly void ReplaceAt(int x, int y, T newItem)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
            return;

        Inner[(y * Width) + x] = newItem;
    }

    public readonly Span<T> Row(int y) => Inner.Slice(y * Width, Width);

    public readonly Point? SeekItem(T item)
    {
        for (int inx = 0; inx < Inner.Length; inx++)
            if (Inner[inx].GetHashCode() == item.GetHashCode())
                return new(inx % Width, inx / Width);

        return null;
    }

    public override string ToString()
    {
        var builder = new System.Text.StringBuilder();
        builder.AppendLine($"{{Width: {Width}, Height: {Height}}}");
        for (int i = 0; i < Inner.Length; i++)
        {
            if (i % Width == 0)
                builder.AppendLine();

            builder.Append(Inner[i]).Append(' ');
        }
        var result = builder.ToString();
        return result;
    }
}

[Flags]
public enum Direction
{
    Up = 1,
    Down = 2,
    Right = 4,
    Left = 8
}
