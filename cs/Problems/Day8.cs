namespace aoc24.Problems;

public sealed class Day8 : IProblem<int>
{
    public int Solve(string input) => CalculateAntiNodesOptimized(input);

    private readonly Dictionary<char, List<Point>> antennaGroups = [];

    private readonly HashSet<Point> antiNodes = [];

    public int CalculateAntiNodesOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[CharMatrix.SizeFor(input)];
        var matrix = CharMatrix.CreateFrom(input, inner);

        antennaGroups.Clear();
        antiNodes.Clear();

        // Group antennas by character.
        for (int y = 0; y < matrix.Height; y++)
        {
            for (int x = 0; x < matrix.Width; x++)
            {
                var location = new Point(x, y);

                if (matrix.CharAt(location) is char val and not '.')
                {
                    if (!antennaGroups.ContainsKey(val))
                        antennaGroups[val] = [];

                    antennaGroups[val].Add(location);
                }
            }
        }

        // Compare antennas and calculate the anti-nodes in each group.
        foreach (var (_, antennas) in antennaGroups)
        {
            for (int i = 0; i < antennas.Count - 1; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var (antennaA, antennaB) = (antennas[i], antennas[j]);
                    var distance = antennaA - antennaB;

                    var antiNodeA = antennaA + distance;
                    var antiNodeB = antennaB - distance;

                    if (matrix.CharAt(antiNodeA) is not null)
                        antiNodes.Add(antiNodeA);

                    if (matrix.CharAt(antiNodeB) is not null)
                        antiNodes.Add(antiNodeB);
                }
            }
        }

        return antiNodes.Count;
    }
}
