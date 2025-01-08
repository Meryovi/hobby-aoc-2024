namespace aoc24.Problems;

// https://adventofcode.com/2024/day/8
public sealed class Day8 : IProblem<int>
{
    public int Solve(string input) => CalculateAntiNodesOptimized(input);

    private readonly Dictionary<char, List<Point>> antennaGroups = [];

    private readonly HashSet<Point> antiNodes = [];

    public int CalculateAntiNodesOptimized(ReadOnlySpan<char> input)
    {
        Span<char> inner = stackalloc char[Matrix<char>.SizeFor(input)];
        var matrix = Matrix<char>.CreateFrom(input, inner);

        antennaGroups.Clear();
        antiNodes.Clear();

        // Group antennas by character.
        for (int y = 0; y < matrix.Height; y++)
        {
            for (int x = 0; x < matrix.Width; x++)
            {
                var location = new Point(x, y);

                if (matrix.ItemAt(location) is char val and not '.')
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

                    if (matrix.ItemAt(antiNodeA) is not null)
                        antiNodes.Add(antiNodeA);

                    if (matrix.ItemAt(antiNodeB) is not null)
                        antiNodes.Add(antiNodeB);
                }
            }
        }

        return antiNodes.Count;
    }
}
