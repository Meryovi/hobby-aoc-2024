namespace aoc24.Problems;

public sealed class Day6 : IProblem<int>
{
    public int Solve(string input) => CountGuardStepsOptimized(input);

    private readonly HashSet<Point> history = [];

    public int CountGuardStepsOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[CharMatrix.SizeFor(input)];
        var matrix = CharMatrix.CreateFrom(input, inner);
        history.Clear();

        var start = matrix.SeekChar('^').GetValueOrDefault();
        var current = new FacingPoint(Direction.Up, start);
        history.Add(start);

        while (true)
        {
            var nextPosition = current.MoveForward();
            var nextChar = matrix.CharAt(nextPosition.X, nextPosition.Y);

            if (nextChar is null) // Went out of bounds.
                break;

            if (nextChar == '#')
            {
                current = TurnRight(current);
                continue;
            }

            current = nextPosition;
            history.Add(current.Point);
        }

        return history.Count;
    }

    private static FacingPoint TurnRight(FacingPoint facingPoint)
    {
        var newDirection = facingPoint.Direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Right => Direction.Down,
            Direction.Left => Direction.Up,
            _ => facingPoint.Direction
        };
        return facingPoint with { Direction = newDirection };
    }
}
