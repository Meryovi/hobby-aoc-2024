namespace aoc24.Problems;

// https://adventofcode.com/2024/day/1
public sealed class Day1 : IProblem<int>
{
    public int Solve(string input) => DistanceBetweenListsOptimized(input);

    private static int DistanceBetweenListsOptimized(ReadOnlySpan<char> input)
    {
        int lineCount = input.Count(InputReader.NewLine) + 1;

        Span<int> left = stackalloc int[lineCount];
        Span<int> right = stackalloc int[lineCount];
        Span<Range> ranges = stackalloc Range[lineCount];

        input.Split(ranges, InputReader.NewLine);

        for (int i = 0; i < lineCount; i++)
        {
            var line = input[ranges[i]];
            int inx = line.IndexOf("   ");

            left[i] = int.Parse(line[..inx]);
            right[i] = int.Parse(line[(inx + 3)..]);
        }

        MemoryExtensions.Sort(left);
        MemoryExtensions.Sort(right);

        int distance = 0;

        for (int i = 0; i < lineCount; i++)
            distance += Math.Abs(left[i] - right[i]);

        return distance;
    }
}
