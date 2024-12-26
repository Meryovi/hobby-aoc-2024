namespace aoc24.Problems;

public sealed class Day7 : IProblem<long>
{
    public long Solve(string input) => CalculateCalibrationValuesOptimized(input);

    public static long CalculateCalibrationValuesOptimized(ReadOnlySpan<char> input)
    {
        long totalCalibration = 0;

        Span<long> numbers = stackalloc long[15];
        var lineEnumerator = input.Split(InputReader.NewLine);

        while (lineEnumerator.MoveNext())
        {
            var line = input[lineEnumerator.Current];
            int splitInx = line.IndexOf(':');
            long testValue = long.Parse(line[..splitInx]);
            int length = InputParser.ParseNumbers(numbers, line[(splitInx + 1)..], ' ');

            if (OperatorsEqualTestValue(testValue, numbers[0], numbers, length, 1))
                totalCalibration += testValue;
        }

        return totalCalibration;
    }

    public static long CalculateCalibrationValues(string input)
    {
        long totalCalibration = 0;

        foreach (var line in input.Split(InputReader.NewLine))
        {
            int splitInx = line.IndexOf(':');
            long testValue = long.Parse(line[..splitInx]);
            var numbers = line[(splitInx + 2)..].Split(" ").Select(long.Parse).ToArray();

            if (OperatorsEqualTestValue(testValue, numbers[0], numbers, numbers.Length, 1))
                totalCalibration += testValue;
        }

        return totalCalibration;
    }

    public static bool OperatorsEqualTestValue(long testValue, long currentValue, ReadOnlySpan<long> numbers, int length, int inx)
    {
        if (currentValue > testValue) // If current exceeds test value, it will never match.
            return false;

        if (inx == length) // In the last iteration, compare the values.
            return testValue == currentValue;

        long addValue = currentValue + numbers[inx];
        long multValue = currentValue * numbers[inx];

        return OperatorsEqualTestValue(testValue, addValue, numbers, length, inx + 1)
            || OperatorsEqualTestValue(testValue, multValue, numbers, length, inx + 1);
    }
}
