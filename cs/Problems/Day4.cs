namespace aoc24.Problems;

// https://adventofcode.com/2024/day/4
public sealed class Day4 : IProblem<int>
{
    public int Solve(string input) => CountXmasOccurrencesOptimized(input);

    private static int CountXmasOccurrencesOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[Matrix<char>.SizeFor(input)];
        var matrix = Matrix<char>.CreateFrom(input, inner);

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

    private static int CountXmasOccurrences(string input)
    {
        // The only difference is that this version of "CreateFrom" will allocate a backing array for the matrix,
        // as opposed to the other one which will use the provided and stack allocated structure...
        var matrix = Matrix<char>.CreateFrom(input);

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

    private static int FindWordMatches(Matrix<char> matrix, Point starting, ReadOnlySpan<char> chars)
    {
        if (matrix.ItemAt(starting) != chars[0])
            return 0;

        ReadOnlySpan<Point> directions =
        [
            Point.FromDirection(Direction.Right),
            Point.FromDirection(Direction.Left),
            Point.FromDirection(Direction.Down),
            Point.FromDirection(Direction.Up),
            Point.FromDirection(Direction.Right | Direction.Down),
            Point.FromDirection(Direction.Right | Direction.Up),
            Point.FromDirection(Direction.Left | Direction.Down),
            Point.FromDirection(Direction.Left | Direction.Up),
        ];

        int matches = 0;

        foreach (var direction in directions)
        {
            var current = starting;
            for (int i = 0; i < chars.Length; i++)
            {
                char? currChar = matrix.ItemAt(current);

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
