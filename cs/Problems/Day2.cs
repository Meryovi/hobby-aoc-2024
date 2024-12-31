namespace aoc24.Problems;

// https://adventofcode.com/2024/day/2
public sealed class Day2 : IProblem<int>
{
    public int Solve(string input) => ComputeSafeReactorReportsOptimized(input);

    private static int ComputeSafeReactorReportsOptimized(ReadOnlySpan<char> input)
    {
        int safeReports = 0;
        Span<int> levels = stackalloc int[8];

        var lineEnumerator = input.Split(InputReader.NewLine).GetEnumerator();
        while (lineEnumerator.MoveNext())
        {
            var line = input[lineEnumerator.Current];
            int numOfLevels = InputParser.ParseNumbers(levels, line, ' ');
            bool increasing = levels[0] < levels[1];

            safeReports++;

            for (int i = 1; i < numOfLevels; i++)
            {
                var (prev, current) = (levels[i - 1], levels[i]);
                int diff = increasing ? current - prev : prev - current;

                if (diff is < 1 or > 3)
                {
                    safeReports--;
                    break;
                }
            }
        }

        return safeReports;
    }

    private static int ComputeSafeReactorReports(string input)
    {
        int safeReports = 0;

        foreach (var line in input.Split(InputReader.NewLine))
        {
            var levels = line.Split(" ").Select(int.Parse).ToArray();
            bool increasing = levels[0] < levels[1];

            safeReports++;

            for (int i = 1; i < levels.Length; i++)
            {
                var (prev, current) = (levels[i - 1], levels[i]);
                int diff = increasing ? current - prev : prev - current;

                if (diff is < 1 or > 3)
                {
                    safeReports--;
                    break;
                }
            }
        }

        return safeReports;
    }
}
