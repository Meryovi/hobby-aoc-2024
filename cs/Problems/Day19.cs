namespace aoc24.Problems;

using SpanLookup = Dictionary<string, bool>.AlternateLookup<ReadOnlySpan<char>>;

// https://adventofcode.com/2024/day/19
public sealed class Day19 : IProblem<int>
{
    public int Solve(string input) => CountPossibleTowelDesignsOptimized(input);

    private readonly SpanLookup cache = new Dictionary<string, bool>().GetAlternateLookup<ReadOnlySpan<char>>();

    private int CountPossibleTowelDesignsOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine + InputReader.NewLine);

        // No way around allocating some strings.
        // We could stretch it and use an array of string hash codes instead, but that might produce collisions.
        iterator.MoveNext();
        var towels = input[iterator.Current].ToString().Split(", ");

        iterator.MoveNext();
        var designs = input[iterator.Current];
        iterator = designs.Split(InputReader.NewLine);

        int combinations = 0;

        while (iterator.MoveNext())
            if (IsComposedOf(designs[iterator.Current], towels))
                combinations++;

        return combinations;
    }

    private int CountPossibleTowelDesigns(string input)
    {
        if (input.Split(InputReader.NewLine + InputReader.NewLine) is not [var towelsStr, var designsStr])
            return 0;

        var towels = towelsStr.Split(", ");
        var designs = designsStr.Split(InputReader.NewLine);

        int combinations = 0;

        foreach (var design in designs)
            if (IsComposedOf(design, towels))
                combinations++;

        return combinations;
    }

    private bool IsComposedOf(ReadOnlySpan<char> parent, ReadOnlySpan<string> children)
    {
        if (parent.IsEmpty)
            return true;

        // If we've already seen this pattern, bail out.
        if (cache.TryGetValue(parent, out var ret))
            return ret;

        foreach (var child in children)
        {
            if (!parent.StartsWith(child))
                continue;

            bool composedSubset = IsComposedOf(parent[child.Length..], children);
            if (composedSubset)
            {
                cache[parent] = true;
                return true;
            }
        }

        cache[parent] = false;
        return false;
    }
}
