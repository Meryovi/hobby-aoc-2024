namespace aoc24;

public static class InputParser
{
    public static int ParseNumbers<T>(Span<T> numbers, ReadOnlySpan<char> input, params ReadOnlySpan<char> separator)
        where T : INumber<T>
    {
        Span<Range> ranges = stackalloc Range[numbers.Length];

        int splitSize = input.Split(ranges, separator);
        int actualSize = 0;

        for (int i = 0; i < splitSize; i++)
        {
            var value = input[ranges[i]];

            if (!value.IsEmpty)
            {
                numbers[actualSize] = T.Parse(value, null);
                actualSize++;
            }
        }

        return actualSize;
    }

    public static ParsedNumbers<T> ParseNumbers<T>(
        ref Span<T> numbers,
        ReadOnlySpan<char> input,
        int maxSize,
        ReadOnlySpan<char> separator
    )
        where T : INumber<T>
    {
        Span<Range> ranges = stackalloc Range[numbers.Length];

        int splitSize = input.Split(ranges, separator);
        int actualSize = 0;

        for (int i = 0; i < splitSize; i++)
        {
            var value = input[ranges[i]];

            if (!value.IsEmpty)
            {
                numbers[actualSize] = T.Parse(value, null);
                actualSize++;
            }
        }

        return new ParsedNumbers<T>(ref numbers, actualSize);
    }
}

public readonly ref struct ParsedNumbers<T>(ref Span<T> numbers, int actualLength)
    where T : INumber<T>
{
    public ReadOnlySpan<T> Numbers { get; } = numbers;

    public int Length { get; } = actualLength;
}
