namespace aoc24.Problems;

public sealed class Day4 : IProblem<int>
{
    public int Solve(string input) => CountXmasOccurrencesOptimized(input);

    public static int CountXmasOccurrencesOptimized(ReadOnlySpan<char> input)
    {
        Span<char> matrixSpan = stackalloc char[CharMatrix.SizeFor(input)];
        var matrix = CharMatrix.CreateFrom(input, matrixSpan);

        int occurrences = 0;

        for (int x = 0; x < matrix.Width; x++)
        {
            for (int y = 0; y < matrix.Height; y++)
            {
                Point point = new(x, y);
                occurrences += FindWordMatches(matrix, point, "XMAS");
            }
        }

        return occurrences;
    }

    public static int CountXmasOccurrences(string input)
    {
        // The only difference is that this version of "CreateFrom" will allocate a backing array for the matrix,
        // as opposed to the other one which will use the provided and stack allocated structure...
        var matrix = CharMatrix.CreateFrom(input);

        int occurrences = 0;

        for (int x = 0; x < matrix.Width; x++)
        {
            for (int y = 0; y < matrix.Height; y++)
            {
                Point point = new(x, y);
                occurrences += FindWordMatches(matrix, point, "XMAS");
            }
        }

        return occurrences;
    }

    private static int FindWordMatches(CharMatrix matrix, Point starting, ReadOnlySpan<char> chars)
    {
        if (matrix.CharAt(starting) != chars[0])
            return 0;

        ReadOnlySpan<Point> directions =
        [
            Point.FromDirections(Direction.Right),
            Point.FromDirections(Direction.Left),
            Point.FromDirections(Direction.Down),
            Point.FromDirections(Direction.Up),
            Point.FromDirections(Direction.Right | Direction.Down),
            Point.FromDirections(Direction.Right | Direction.Up),
            Point.FromDirections(Direction.Left | Direction.Down),
            Point.FromDirections(Direction.Left | Direction.Up),
        ];

        int matches = 0;

        foreach (var direction in directions)
        {
            var current = starting;
            for (int i = 0; i < chars.Length; i++)
            {
                char? currChar = matrix.CharAt(current);

                // No match or out of matrix bounds.
                if (currChar != chars[i])
                    break;

                current += direction;

                // A match made in heaven.
                if (i == chars.Length - 1)
                    matches++;
            }
        }

        return matches;
    }
}
