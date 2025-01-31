namespace aoc24.Problems;

// https://adventofcode.com/2024/day/25
public sealed class Day25 : IProblem<int>
{
    public int Solve(string input) => CountFittingLockKeyPairsOptimized(input);

    const int ROW_SIZE = 5;

    private static int CountFittingLockKeyPairsOptimized(ReadOnlySpan<char> input)
    {
        // Welcome to day 25! This is the last problem!
        // I could have gotten lazy on this one and allowed some allocations here and there... but no! I refuse.
        // I got creative and decided to use the 'Matrix' primitive as a stack-allocated array of arrays, which allowed me to reduce
        // the number of allocations down to zero. I had to do some Span slicing to make this work, but the end result isn’t too bad... I think.
        // Anyway, the original function without these gymnastics is down below.
        // Hope to see you next year. :)
        const int MAX_ITEMS = 250;
        Span<int> innerLocks = stackalloc int[ROW_SIZE * MAX_ITEMS];
        Span<int> innerKeys = stackalloc int[ROW_SIZE * MAX_ITEMS];

        var locks = Matrix<int>.CreateEmpty(ROW_SIZE, MAX_ITEMS, innerLocks, 0);
        var keys = Matrix<int>.CreateEmpty(ROW_SIZE, MAX_ITEMS, innerKeys, 0);

        int locksSize = 0;
        int keysSize = 0;

        Span<char> inner = stackalloc char[ROW_SIZE * 7]; // Fixed key/lock size.

        var iterator = input.Split(InputReader.NewLine + InputReader.NewLine);

        while (iterator.MoveNext())
        {
            var schematic = input[iterator.Current];
            var matrix = Matrix<char>.CreateFrom(schematic, inner);

            if (schematic[0] == '#')
            {
                var store = innerLocks.Slice(locksSize * ROW_SIZE, ROW_SIZE);
                FillLockFromSchematic(matrix, store);
                locksSize++;
            }

            if (schematic[0] == '.')
            {
                var store = innerKeys.Slice(keysSize * ROW_SIZE, ROW_SIZE);
                FillKeyFromSchematic(matrix, store);
                keysSize++;
            }
        }

        int fitting = 0;

        for (int l = 0; l < locksSize; l++)
        for (int k = 0; k < keysSize; k++)
            fitting += AreLockAndKeyCompatible(locks.Row(l), keys.Row(k), ROW_SIZE) ? 1 : 0;

        return fitting;
    }

    private static int CountFittingLockKeyPairs(string input)
    {
        var schematics = input.Split(InputReader.NewLine + InputReader.NewLine);

        var locks = new List<int[]>();
        var keys = new List<int[]>();

        foreach (var schematic in schematics)
        {
            var matrix = Matrix<char>.CreateFrom(schematic);
            var store = new int[matrix.Width];

            if (schematic[0] == '#')
            {
                FillLockFromSchematic(matrix, store);
                locks.Add(store);
            }

            if (schematic[0] == '.')
            {
                FillKeyFromSchematic(matrix, store);
                keys.Add(store);
            }
        }

        int fitting = 0;

        foreach (var @lock in locks)
        foreach (var key in keys)
            fitting += AreLockAndKeyCompatible(@lock, key, ROW_SIZE) ? 1 : 0;

        return fitting;
    }

    private static void FillLockFromSchematic(Matrix<char> schematic, Span<int> store)
    {
        for (int i = 0; i < schematic.Width; i++)
        {
            store[i] = schematic.Height - 2;
            for (int j = 1; j < schematic.Height - 1; j++)
            {
                if (schematic.ItemAt(i, j) != '#')
                {
                    store[i] = j - 1;
                    break;
                }
            }
        }
    }

    private static void FillKeyFromSchematic(Matrix<char> schematic, Span<int> store)
    {
        for (int i = 0; i < schematic.Width; i++)
        {
            store[i] = schematic.Height - 2;
            for (int j = schematic.Height - 1; j > 0; j--)
            {
                if (schematic.ItemAt(i, j) != '#')
                {
                    store[i] = store[i] - j;
                    break;
                }
            }
        }
    }

    private static bool AreLockAndKeyCompatible(ReadOnlySpan<int> @lock, ReadOnlySpan<int> key, int rowSize)
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (@lock[i] + key[i] > rowSize)
                return false;
        }

        return true;
    }
}
