namespace aoc24.Problems;

// https://adventofcode.com/2024/day/5
public sealed class Day5 : IProblem<int>
{
    public int Solve(string input) => SumMiddlePageNumbersOptimized(input);

    private readonly Dictionary<int, List<int>> orderRules = []; // ;)

    public int SumMiddlePageNumbersOptimized(ReadOnlySpan<char> input)
    {
        Span<Range> fileSections = stackalloc Range[2];
        input.Split(fileSections, InputReader.NewLine + InputReader.NewLine);

        var orderingSection = input[fileSections[0]];
        var updatesSection = input[fileSections[1]];
        orderRules.Clear();

        var orderingEnumerator = orderingSection.Split(InputReader.NewLine);
        while (orderingEnumerator.MoveNext())
        {
            var orderingRule = orderingSection[orderingEnumerator.Current];
            int separatorInx = orderingRule.IndexOf('|');
            int greater = int.Parse(orderingRule[..separatorInx]);
            int lower = int.Parse(orderingRule[(separatorInx + 1)..]);

            if (!orderRules.ContainsKey(greater))
                orderRules.Add(greater, []);

            orderRules[greater].Add(lower);
        }

        int middleDigitSum = 0;

        Span<int> pageUpdate = stackalloc int[30];
        var updatesEnumerator = updatesSection.Split(InputReader.NewLine);
        while (updatesEnumerator.MoveNext())
        {
            var pageUpdateStr = updatesSection[updatesEnumerator.Current];
            int actualLength = InputParser.ParseNumbers(pageUpdate, pageUpdateStr, ',');
            bool isOrdered = IsOrdered(pageUpdate, actualLength, orderRules);

            if (isOrdered)
                middleDigitSum += pageUpdate[actualLength / 2];
        }

        return middleDigitSum;
    }

    private static int SumMiddlePageNumbers(string input)
    {
        var split = input.Split(InputReader.NewLine + InputReader.NewLine);
        var pageOrdering = split[0].Split(InputReader.NewLine);
        var pageUpdates = split[1].Split(InputReader.NewLine);

        var orderRules = new Dictionary<int, List<int>>();

        foreach (var orderingRuleStr in pageOrdering)
        {
            int separatorInx = orderingRuleStr.IndexOf('|');
            int greater = int.Parse(orderingRuleStr[..separatorInx]);
            int lower = int.Parse(orderingRuleStr[(separatorInx + 1)..]);

            if (!orderRules.ContainsKey(greater))
                orderRules.Add(greater, []);

            orderRules[greater].Add(lower);
        }

        int middleDigitSum = 0;

        foreach (var pageUpdateStr in pageUpdates)
        {
            var pageUpdate = pageUpdateStr.Split(',').Select(int.Parse).ToArray();
            bool isOrdered = IsOrdered(pageUpdate, pageUpdate.Length, orderRules);

            if (isOrdered)
                middleDigitSum += pageUpdate[pageUpdate.Length / 2];
        }

        return middleDigitSum;
    }

    private static bool IsOrdered(ReadOnlySpan<int> pageUpdate, int length, Dictionary<int, List<int>> orderRules)
    {
        for (int i = 0; i < length - 1; i++)
        {
            int current = pageUpdate[i];

            if (!orderRules.ContainsKey(current))
                return false;

            for (int j = i + 1; j < length; j++)
            {
                int against = pageUpdate[j];

                if (!orderRules[current].Contains(against))
                    return false;
            }
        }

        return true;
    }
}
