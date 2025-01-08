namespace aoc24.Problems;

// https://adventofcode.com/2024/day/18
public sealed class Day18 : IProblem<int>
{
    public int Solve(string input) => CorruptMapAndCountStepsOptimized(input);

    private readonly PriorityQueue<FacingPoint, int> queue = new();

    private int CorruptMapAndCountStepsOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine);

        iterator.MoveNext();
        int size = int.Parse(input[iterator.Current]);

        iterator.MoveNext();
        int bytes = int.Parse(input[iterator.Current]);

        Span<char> innerMatrix = stackalloc char[size * size];
        var matrix = Matrix<char>.CreateEmpty(size, size, innerMatrix, '.');

        Span<Direction> innerHistory = stackalloc Direction[size * size];
        var history = Matrix<Direction>.CreateEmpty(size, size, innerHistory, default);

        int @byte = 0;
        while (@byte++ < bytes && iterator.MoveNext())
        {
            var point = Point.FromString(input[iterator.Current]);
            matrix.ReplaceAt(point, '#');
        }

        var goal = new Point(size - 1, size - 1);

        queue.Clear();
        queue.Enqueue(new(Direction.Right, new()), 0);

        while (queue.TryDequeue(out var current, out var priority))
        {
            if (current.Position == goal)
                return priority;

            foreach (var direction in NavigableDirections)
            {
                var next = current.Rotate(direction).MoveForward(matrix.Width, matrix.Height);

                if (next is null || matrix.ItemAt(next.Value.Position) != '.')
                    continue;

                var visited = history.ItemAt(next.Value.Position)!.Value;

                if (visited.HasFlag(direction))
                    continue;

                history.ReplaceAt(next.Value.Position, visited | direction);
                queue.Enqueue(next.Value, priority + 1);
            }
        }

        return 0;
    }

    private static int CorruptMapAndCountSteps(string input)
    {
        if (input.Split(InputReader.NewLine) is not [var sizeStr, var bytesStr, .. var positions])
            return 0;

        int size = int.Parse(sizeStr);
        int bytes = int.Parse(bytesStr);

        var matrix = Matrix<char>.CreateEmpty(size, size, '.');

        int @byte = -1;
        while (++@byte < bytes && @byte < positions.Length)
        {
            var point = Point.FromString(positions[@byte]);
            matrix.ReplaceAt(point, '#');
        }

        var queue = new PriorityQueue<FacingPoint, int>();
        var history = new HashSet<FacingPoint>();
        var goal = new Point(size - 1, size - 1);

        queue.Enqueue(new(Direction.Right, new()), 0);

        while (queue.TryDequeue(out var current, out var priority))
        {
            if (current.Position == goal)
                return priority;

            foreach (var direction in NavigableDirections)
            {
                var next = current.Rotate(direction).MoveForward(matrix.Width, matrix.Height);

                if (next is null || matrix.ItemAt(next.Value.Position) != '.' || history.Contains(next.Value))
                    continue;

                history.Add(next.Value);
                queue.Enqueue(next.Value, priority + 1);
            }
        }

        return 0;
    }

    private static readonly Direction[] NavigableDirections = [Direction.Right, Direction.Down, Direction.Up, Direction.Left];
}
