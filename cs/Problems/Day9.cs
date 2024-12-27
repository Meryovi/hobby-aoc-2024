namespace aoc24.Problems;

public sealed class Day9 : IProblem<long>
{
    public long Solve(string input) => CalculateDiskChecksumOptimized(input);

    public static long CalculateDiskChecksumOptimized(ReadOnlySpan<char> input)
    {
        // Expand file volume... O.O
        Span<int> expanded = stackalloc int[95_000];
        int size = 0;

        for (int i = 0; i < input.Length; i++)
        {
            int positions = input[i] - '0';
            int fileId = i % 2 == 0 ? i / 2 : -1;

            for (int s = 0; s < positions; s++, size++)
                expanded[size] = fileId;
        }

        // Calculate checksum...
        // In theory, we should reorganize free space first, but I figured I could do this in one go.
        long checksum = 0;

        for (int head = 0, tail = size; head < tail; head++)
        {
            int value = expanded[head];

            if (IsEmptySpace(value))
            {
                while (--tail > head)
                {
                    value = expanded[tail];

                    if (!IsEmptySpace(value))
                        break;
                }
            }

            checksum += value * head;
        }

        return checksum;
    }

    public static long CalculateDiskChecksum(string input)
    {
        var expanded = new List<int>();

        for (int i = 0; i < input.Length; i += 2)
        {
            int files = input[i] - '0';
            int space = i < input.Length - 1 ? input[i + 1] - '0' : 0;
            int fileId = i / 2;

            expanded.AddRange(Enumerable.Repeat(fileId, files));
            expanded.AddRange(Enumerable.Repeat(-1, space));
        }

        long checksum = 0;

        for (int head = 0, tail = expanded.Count; head < tail; head++)
        {
            int value = expanded[head];

            if (IsEmptySpace(value))
            {
                while (--tail > head)
                {
                    value = expanded[tail];

                    if (!IsEmptySpace(value))
                        break;
                }
            }

            checksum += value * head;
        }

        return checksum;
    }

    static bool IsEmptySpace(int number) => number < 0;
}