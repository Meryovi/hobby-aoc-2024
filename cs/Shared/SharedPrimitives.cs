namespace aoc24.Shared;

public readonly record struct Point(int X, int Y)
{
    public Point? MoveWithin(Point howMuch, int width, int height)
    {
        var newPoint = this + howMuch;
        return newPoint.X >= width || newPoint.Y >= height ? null : newPoint;
    }

    public static Point FromDirections(Direction direction)
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

    public static Point operator +(Point first, Point other) => new(first.X + other.X, first.Y + other.Y);

    public static Point operator -(Point first, Point other) => new(first.X - other.X, first.Y - other.Y);

    public static Point operator *(Point first, Point other) => new(first.X * other.X, first.Y * other.Y);

    public static Point operator *(Point point, int value) => point * new Point(value, value);
}

public readonly record struct FacingPoint(Direction Direction, Point Point)
{
    public int X => Point.X;

    public int Y => Point.Y;

    public FacingPoint(Direction Direction, int X, int Y)
        : this(Direction, new(X, Y)) { }

    public FacingPoint? MoveWithin(Point howMuch, int width, int height)
    {
        var newPoint = Point.MoveWithin(howMuch, width, height);

        if (newPoint is null)
            return null;

        return new(Direction, newPoint.Value);
    }

    public FacingPoint MoveForward() => this + Point.FromDirections(Direction);

    public FacingPoint? MoveForward(int width, int height)
    {
        var howMuch = Point.FromDirections(Direction);
        return MoveWithin(howMuch, width, height);
    }

    public static FacingPoint operator +(FacingPoint first, Point other) =>
        new(first.Direction, first.X + other.X, first.Y + other.Y);

    public static FacingPoint operator -(FacingPoint first, Point other) =>
        new(first.Direction, first.X - other.X, first.Y - other.Y);
}

public readonly ref struct CharMatrix
{
    private Span<char> Matrix { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }

    public static CharMatrix CreateFrom(ReadOnlySpan<char> input)
    {
        var inner = new char[SizeFor(input)];
        return CreateFrom(input, inner);
    }

    public static CharMatrix CreateFrom(ReadOnlySpan<char> input, Span<char> innerSpan)
    {
        int width = input.IndexOf(InputReader.NewLine);

        if (width == 0)
            throw new ArgumentException("No lines provided in input for matrix");

        int height = input.Count(InputReader.NewLine) + 1;

        if (innerSpan.Length != width * height)
            throw new ArgumentException($"Incorrectly sized inner span for matrix, expected size {width * height}");

        var matrix = new CharMatrix()
        {
            Width = width,
            Height = height,
            Matrix = innerSpan
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

    public static int SizeFor(ReadOnlySpan<char> input)
    {
        int width = input.IndexOf(InputReader.NewLine);

        if (width == 0)
            return 0;

        int height = input.Count(InputReader.NewLine) + 1;
        return width * height;
    }

    public readonly char? CharAt(Point point) => CharAt(point.X, point.Y);

    public readonly char? CharAt(int x, int y)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
            return null;

        return Matrix[(y * Width) + x];
    }

    public readonly void ReplaceCharAt(int x, int y, char newChar)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
            return;

        Matrix[(y * Width) + x] = newChar;
    }

    public readonly Point? SeekChar(char character)
    {
        var inx = Matrix.IndexOf(character);

        if (inx == -1)
            return null;

        return new(inx % Width, inx / Width);
    }

    public override string ToString()
    {
        var builder = new System.Text.StringBuilder();
        builder.AppendLine($"{{Width: {Width}, Height: {Height}}}");
        for (int i = 0; i < Matrix.Length; i++)
        {
            if (i % Height == 0)
                builder.AppendLine();

            builder.Append(Matrix[i]).Append(' ');
        }
        return builder.ToString();
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
