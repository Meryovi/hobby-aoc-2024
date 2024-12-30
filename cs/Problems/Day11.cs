using System.Buffers;

namespace aoc24.Problems;

public sealed class Day11 : IProblem<int>
{
    public int Solve(string input) => BlinkAndCountStonesOptimized(input);

    private static readonly ArrayPool<long> bank = ArrayPool<long>.Shared;

    public static int BlinkAndCountStonesOptimized(ReadOnlySpan<char> input)
    {
        // We need a huge chunk of longs in order to be able to process this exponentially growing array.
        // For that end, we use an array 'rented' from an array pool.

        int elements = input.Count(' ') + 1;
        long[] numbers = bank.Rent(elements * 30_000);

        try
        {
            int length = InputParser.ParseNumbers(numbers, input, ' ');

            for (int i = 0; i < 25; i++)
            {
                int count = length;
                for (int x = 0; x < count; x++)
                {
                    var (updated, created) = ApplyBlinkingRules(numbers[x]);
                    numbers[x] = updated;

                    if (created is not null)
                        numbers[length++] = created.Value;
                }
            }

            return length;
        }
        finally
        {
            bank.Return(numbers);
        }
    }

    public static int BlinkAndCountStones(string input)
    {
        var numbers = input.Split(' ').Select(long.Parse).ToList();

        for (int i = 0; i < 25; i++)
        {
            int count = numbers.Count;
            for (int x = 0; x < count; x++)
            {
                var (updated, created) = ApplyBlinkingRules(numbers[x]);
                numbers[x] = updated;

                if (created is not null)
                    numbers.Add(created.Value);
            }
        }

        return numbers.Count;
    }

    private static (long, long?) ApplyBlinkingRules(long number)
    {
        if (number == 0)
            return (1, null);

        int length = GetNumberLength(number);

        if (length % 2 != 0)
            return (number * 2024, null);

        int powerHalf = (int)Math.Pow(10, length / 2);
        long firstHalf = number / powerHalf;
        long secondHalf = number % powerHalf;

        return (firstHalf, secondHalf);
    }

    private static int GetNumberLength(long number) => number == 0 ? 1 : (int)Math.Floor(Math.Log10(number)) + 1;
}
