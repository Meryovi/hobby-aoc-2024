namespace aoc24;

public static class InputParser
{
    public static int ParseNumbers<T>(Span<T> numbers, ReadOnlySpan<char> input, params ReadOnlySpan<char> separator)
        where T : INumber<T>
    {
        var iterator = input.Split(separator);
        int size = 0;

        while (iterator.MoveNext())
        {
            ReadOnlySpan<char> value = input[iterator.Current];

            if (!value.IsEmpty)
            {
                numbers[size] = T.Parse(value, null);
                size++;
            }
        }

        return size;
    }

    public static int ParseNumbers<T>(T[] numbers, ReadOnlySpan<char> input, params ReadOnlySpan<char> separator)
        where T : INumber<T> => ParseNumbers(numbers.AsSpan(), input, separator);
}
