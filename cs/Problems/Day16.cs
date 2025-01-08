namespace aoc24.Problems;

// https://adventofcode.com/2024/day/16
public sealed class Day16 : IProblem<int>
{
    public int Solve(string input) => CountReindeerStepsScoreOptimized(input);

    private readonly PriorityQueue<FacingPoint, int> queue = new();

    private int CountReindeerStepsScoreOptimized(ReadOnlySpan<char> input)
    {
        Span<char> innerMatrix = stackalloc char[Matrix<char>.SizeFor(input)];
        var matrix = Matrix<char>.CreateFrom(input, innerMatrix);

        Span<Direction> innerHistory = stackalloc Direction[innerMatrix.Length];
        var history = Matrix<Direction>.CreateEmpty(matrix.Width, matrix.Height, innerHistory, default);

        var start = matrix.SeekItem('S')!.Value;
        var goal = matrix.SeekItem('E')!.Value;
        var first = new FacingPoint(Direction.Right, start);

        queue.Clear();
        TryEnqueue(matrix, history, first, 0);

        while (queue.TryDequeue(out var reindeer, out var points))
        {
            if (reindeer.Position == goal)
                return points;

            var forward = reindeer.MoveForward();
            var rightTurn = reindeer.Rotate(Direction.Right);
            var leftTurn = reindeer.Rotate(Direction.Left);

            TryEnqueue(matrix, history, forward, points + 1);
            TryEnqueue(matrix, history, rightTurn, points + 1000);
            TryEnqueue(matrix, history, leftTurn, points + 1000);
        }

        void TryEnqueue(Matrix<char> matrix, Matrix<Direction> history, FacingPoint point, int points)
        {
            if (IsInHistory(history, point) || matrix.ItemAt(point.Position) is not ('.' or 'S' or 'E'))
                return;

            queue.Enqueue(point, points);
            AddToHistory(history, point);
        }

        // Just for fun, use a char matrix as a bitmask table encoding the visited directions
        // Could still have used a HashSet, but this is effectively 0 alloc.
        static bool IsInHistory(Matrix<Direction> history, FacingPoint point)
        {
            var directions = history.ItemAt(point.Position)!.Value;
            return directions.HasFlag(point.Direction);
        }

        static void AddToHistory(Matrix<Direction> history, FacingPoint point)
        {
            var currentDirs = history.ItemAt(point.Position)!.Value;
            var directions = currentDirs | point.Direction; // Add the new direction
            history.ReplaceAt(point.Position, directions);
        }

        return 0;
    }

    private int CountReindeerStepsScore(ReadOnlySpan<char> input)
    {
        var matrix = Matrix<char>.CreateFrom(input);
        var history = new HashSet<FacingPoint>();

        var start = matrix.SeekItem('S')!.Value;
        var goal = matrix.SeekItem('E')!.Value;
        var first = new FacingPoint(Direction.Right, start);

        queue.Clear();
        TryEnqueue(matrix, first, 0);

        while (queue.TryDequeue(out var reindeer, out var points))
        {
            if (reindeer.Position == goal)
                return points;

            var forward = reindeer.MoveForward();
            var rightTurn = reindeer.Rotate(Direction.Right);
            var leftTurn = reindeer.Rotate(Direction.Left);

            TryEnqueue(matrix, forward, points + 1);
            TryEnqueue(matrix, rightTurn, points + 1000);
            TryEnqueue(matrix, leftTurn, points + 1000);
        }

        void TryEnqueue(Matrix<char> matrix, FacingPoint point, int points)
        {
            if (history.Contains(point) || matrix.ItemAt(point.Position) is not ('.' or 'S' or 'E'))
                return;

            queue.Enqueue(point, points);
            history.Add(point);
        }

        return 0;
    }
}
