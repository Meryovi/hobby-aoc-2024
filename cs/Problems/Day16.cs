namespace aoc24.Problems;

// https://adventofcode.com/2024/day/16
public sealed class Day16 : IProblem<int>
{
    public int Solve(string input) => CountReindeerStepsScoreOptimized(input);

    private readonly PriorityQueue<FacingPoint, int> queue = new();

    private int CountReindeerStepsScoreOptimized(ReadOnlySpan<char> input)
    {
        Span<char> innerMatrix = stackalloc char[CharMatrix.SizeFor(input)];
        var matrix = CharMatrix.CreateFrom(input, innerMatrix);

        Span<char> innerHistory = stackalloc char[innerMatrix.Length];
        var history = CharMatrix.CreateEmpty(matrix.Width, matrix.Height, innerHistory, '0');

        var start = matrix.SeekChar('S')!.Value;
        var goal = matrix.SeekChar('E')!.Value;
        var first = new FacingPoint(Direction.Right, start);

        queue.Clear();
        TryEnqueue(matrix, history, first, 0);

        while (queue.TryDequeue(out var reindeer, out var points))
        {
            if (reindeer.Point == goal)
                return points;

            var forward = reindeer.MoveForward();
            var rightTurn = reindeer.Rotate(Direction.Right);
            var leftTurn = reindeer.Rotate(Direction.Left);

            TryEnqueue(matrix, history, forward, points + 1);
            TryEnqueue(matrix, history, rightTurn, points + 1000);
            TryEnqueue(matrix, history, leftTurn, points + 1000);
        }

        void TryEnqueue(CharMatrix matrix, CharMatrix history, FacingPoint point, int points)
        {
            if (IsInHistory(history, point) || matrix.CharAt(point.Point) is not ('.' or 'S' or 'E'))
                return;

            queue.Enqueue(point, points);
            AddToHistory(history, point);
        }

        // Just for fun, use a char matrix as a bitmask table encoding the visited directions
        // Could still have used a HashSet, but this is effectively 0 alloc.
        static bool IsInHistory(CharMatrix history, FacingPoint point)
        {
            var currentChar = history.CharAt(point.Point)!.Value;
            var directions = (Direction)(currentChar - '0');
            return directions.HasFlag(point.Direction);
        }

        static void AddToHistory(CharMatrix history, FacingPoint point)
        {
            var currentChar = history.CharAt(point.Point)!.Value;
            var directions = (Direction)(currentChar - '0') | point.Direction; // Add the new direction
            var newChar = (char)(directions + '0');
            history.ReplaceCharAt(point.Point, newChar);
        }

        return 0;
    }

    private int CountReindeerStepsScore(ReadOnlySpan<char> input)
    {
        var matrix = CharMatrix.CreateFrom(input);
        var history = new HashSet<FacingPoint>();

        var start = matrix.SeekChar('S')!.Value;
        var goal = matrix.SeekChar('E')!.Value;
        var first = new FacingPoint(Direction.Right, start);

        queue.Clear();
        TryEnqueue(matrix, first, 0);

        while (queue.TryDequeue(out var reindeer, out var points))
        {
            if (reindeer.Point == goal)
                return points;

            var forward = reindeer.MoveForward();
            var rightTurn = reindeer.Rotate(Direction.Right);
            var leftTurn = reindeer.Rotate(Direction.Left);

            TryEnqueue(matrix, forward, points + 1);
            TryEnqueue(matrix, rightTurn, points + 1000);
            TryEnqueue(matrix, leftTurn, points + 1000);
        }

        void TryEnqueue(CharMatrix matrix, FacingPoint point, int points)
        {
            if (history.Contains(point) || matrix.CharAt(point.Point) is not ('.' or 'S' or 'E'))
                return;

            queue.Enqueue(point, points);
            history.Add(point);
        }

        return 0;
    }
}
