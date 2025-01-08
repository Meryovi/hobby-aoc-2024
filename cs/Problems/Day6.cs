namespace aoc24.Problems;

// https://adventofcode.com/2024/day/6
public sealed class Day6 : IProblem<int>
{
    public int Solve(string input) => CountGuardStepsOptimized(input);

    private readonly HashSet<Point> history = [];

    public int CountGuardStepsOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[Matrix<char>.SizeFor(input)];
        var matrix = Matrix<char>.CreateFrom(input, inner);
        history.Clear();

        var start = matrix.SeekItem('^').GetValueOrDefault();
        var current = new FacingPoint(Direction.Up, start);
        history.Add(start);

        while (true)
        {
            var next = current.MoveForward();
            var nextChar = matrix.ItemAt(next.Position);

            if (nextChar is null) // Went out of bounds.
                break;

            if (nextChar == '#')
            {
                current = TurnRight(current);
                continue;
            }

            current = next;
            history.Add(current.Position);
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
