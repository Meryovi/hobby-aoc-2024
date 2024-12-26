using System.Text.RegularExpressions;

namespace aoc24.Problems;

public sealed partial class Day3 : IProblem<int>
{
    public int Solve(string input) => SumCorruptedCalculationOptimized(input);

    public static int SumCorruptedCalculationOptimized(ReadOnlySpan<char> input)
    {
        int sum = 0;

        var iter = input.Split("mul(");
        while (iter.MoveNext())
        {
            var slice = input[iter.Current];

            int commaInx = slice.IndexOf(',');
            int rightParInx = slice.IndexOf(')');

            if (commaInx == -1 || rightParInx < commaInx)
                continue;

            int.TryParse(slice[..commaInx], out int left);
            int.TryParse(slice[(commaInx + 1)..rightParInx], out int right);
            sum += left * right;
        }

        return sum;
    }

    public static int SumCorruptedCalculation(string input)
    {
        var matches = MultExpressionRegex.Matches(input);
        int sum = 0;

        for (int i = 0; i < matches.Count; i++)
        {
            int left = int.Parse(matches[i].Groups[1].Value);
            int right = int.Parse(matches[i].Groups[2].Value);
            sum += left * right;
        }

        return sum;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    public static partial Regex MultExpressionRegex { get; }
}
