namespace aoc24.Problems;

using Keypad = Dictionary<Point, char>;

// https://adventofcode.com/2024/day/21
public sealed class Day21 : IProblem<int>
{
    public int Solve(string input) => CountRobotButtonPressScoresOptimized(input);

    readonly Dictionary<(char currKey, char nextKey, int depth), int> cache = [];

    readonly Keypad[] keypads =
    [
        // Pad 1
        new()
        {
            { new(0, 0), '7' },
            { new(1, 0), '8' },
            { new(2, 0), '9' },
            { new(0, -1), '4' },
            { new(1, -1), '5' },
            { new(2, -1), '6' },
            { new(0, -2), '1' },
            { new(1, -2), '2' },
            { new(2, -2), '3' },
            { new(0, -3), ' ' },
            { new(1, -3), '0' },
            { new(2, -3), 'A' }
        },
        // Pad 2
        new()
        {
            { new(0, 0), ' ' },
            { new(1, 0), '^' },
            { new(2, 0), 'A' },
            { new(0, -1), '<' },
            { new(1, -1), 'v' },
            { new(2, -1), '>' },
        },
        // Pad 3
        new()
        {
            { new(0, 0), ' ' },
            { new(1, 0), '^' },
            { new(2, 0), 'A' },
            { new(0, -1), '<' },
            { new(1, -1), 'v' },
            { new(2, -1), '>' },
        }
    ];

    private int CountRobotButtonPressScoresOptimized(ReadOnlySpan<char> input)
    {
        cache.Clear();
        int res = 0;

        var iterator = input.Split(InputReader.NewLine);
        while (iterator.MoveNext())
        {
            var line = input[iterator.Current];
            var value = int.Parse(line[..^1]);
            res += value * CalculateKeysCost(line, keypads);
        }

        return res;
    }

    private int CalculateKeysCost(ReadOnlySpan<char> keys, ReadOnlySpan<Keypad> keypads)
    {
        if (keypads.Length == 0)
            return keys.Length;

        char currentKey = 'A'; // Always start at 'A'
        int cost = 0;

        foreach (var key in keys)
        {
            cost += CalculateKeyCost(currentKey, key, keypads);
            currentKey = key;
        }

        return cost;
    }

    private int CalculateKeyCost(char currentKey, char nextKey, ReadOnlySpan<Keypad> keypads)
    {
        if (cache.TryGetValue((currentKey, nextKey, keypads.Length), out int cached))
            return cached;

        var currKeyPad = keypads[0];

        (Point currPos, Point nextPos) = (Point.Zero, Point.Zero);
        foreach (var key in currKeyPad)
        {
            currPos = key.Value == currentKey ? key.Key : currPos;
            nextPos = key.Value == nextKey ? key.Key : nextPos;

            if (currPos != Point.Zero && nextPos != Point.Zero)
                break;
        }

        int dy = nextPos.Y - currPos.Y;
        int dx = nextPos.X - currPos.X;

        // Operate on a stack allocated char array to eliminate allocs. Works the same as a string in this use case.
        Span<char> nextKeys = stackalloc char[Math.Abs(dy) + Math.Abs(dx) + 1];
        nextKeys[^1] = 'A';

        int cost = int.MaxValue;

        if (currKeyPad[new(currPos.X, nextPos.Y)] != ' ')
        {
            nextKeys[..Math.Abs(dy)].Fill(dy < 0 ? 'v' : '^');
            nextKeys[Math.Abs(dy)..^1].Fill(dx < 0 ? '<' : '>');
            cost = Math.Min(cost, CalculateKeysCost(nextKeys, keypads[1..]));
        }

        if (currKeyPad[new(nextPos.X, currPos.Y)] != ' ')
        {
            nextKeys[..Math.Abs(dx)].Fill(dx < 0 ? '<' : '>');
            nextKeys[Math.Abs(dx)..^1].Fill(dy < 0 ? 'v' : '^');
            cost = Math.Min(cost, CalculateKeysCost(nextKeys, keypads[1..]));
        }

        cache.Add((currentKey, nextKey, keypads.Length), cost);
        return cost;
    }
}
