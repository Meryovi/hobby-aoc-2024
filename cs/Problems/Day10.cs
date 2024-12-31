namespace aoc24.Problems;

// https://adventofcode.com/2024/day/10
public sealed class Day10 : IProblem<int>
{
    public int Solve(string input) => CountTrailHeadScoresOptimized(input);

    private readonly HashSet<(Point, Point)> paths = [];

    public int CountTrailHeadScoresOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[CharMatrix.SizeFor(input)];
        var matrix = CharMatrix.CreateFrom(input, inner);

        paths.Clear();

        for (int y = 0; y < matrix.Height; y++)
        {
            for (int x = 0; x < matrix.Width; x++)
            {
                var start = new Point(x, y);
                BuildTrailHeadPaths(matrix, start, start, paths);
            }
        }

        return paths.Count;
    }

    private static void BuildTrailHeadPaths(
        CharMatrix matrix,
        Point start,
        Point current,
        HashSet<(Point, Point)> paths,
        int expected = 0
    )
    {
        int value = matrix.CharAt(current) is char curr ? curr - '0' : int.MinValue;

        if (value != expected)
            return;

        if (value == expected && expected == 9)
        {
            paths.Add((start, current));
            return;
        }

        foreach (var direction in NavigableDirections)
            BuildTrailHeadPaths(matrix, start, current + Point.FromDirection(direction), paths, expected + 1);
    }

    private static readonly Direction[] NavigableDirections = [Direction.Right, Direction.Down, Direction.Left, Direction.Up];
}
