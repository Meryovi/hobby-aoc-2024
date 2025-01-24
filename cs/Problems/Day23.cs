using System.Buffers;

namespace aoc24.Problems;

// https://adventofcode.com/2024/day/23
public sealed class Day23 : IProblem<int>
{
    public int Solve(string input) => CountInterconnectedSetsOptimized(input);

    private readonly HashSet<NodeNetwork> networks = [];

    private readonly NodeGraph<Node> connections = new();

    private int CountInterconnectedSetsOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine);

        connections.Clear();
        networks.Clear();

        while (iterator.MoveNext())
        {
            var (n1, n2) = Node.ParsePair(input[iterator.Current], 't');
            connections.Add(n1, n2);
        }

        foreach (var (n1, _) in connections.Graph)
        {
            foreach (var n2 in connections[n1])
            {
                foreach (var n3 in connections[n2])
                {
                    if (!connections[n3].Contains(n1)) // If n3 doesn't connect back to n1, move on.
                        continue;

                    if (!n1.IsTarget && !n2.IsTarget && !n3.IsTarget) // No t, move on.
                        continue;

                    var network = NodeNetwork.Create(n1, n2, n3);
                    networks.Add(network);
                }
            }
        }

        return networks.Count;
    }

    private int CountInterconnectedSets(string input)
    {
        // Build a map of all the connections between nodes.
        var connections = input
            .Split(InputReader.NewLine)
            .Aggregate(
                new NodeGraph<Node>(),
                (graph, line) =>
                {
                    var (n1, n2) = Node.ParsePair(line, 't');
                    graph.Add(n1, n2);
                    return graph;
                }
            );

        networks.Clear();

        foreach (var (n1, _) in connections.Graph)
        {
            foreach (var n2 in connections[n1])
            {
                foreach (var n3 in connections[n2])
                {
                    if (!connections[n3].Contains(n1)) // If n3 doesn't connect back to n1, move on.
                        continue;

                    if (!n1.IsTarget && !n2.IsTarget && !n3.IsTarget) // No t, move on.
                        continue;

                    var network = NodeNetwork.Create(n1, n2, n3);
                    networks.Add(network);
                }
            }
        }

        return networks.Count;
    }

    // Ensures the order of nodes in the network to prevent duplicates.
    public readonly record struct NodeNetwork(int N1, int N2, int N3)
    {
        public static NodeNetwork Create(Node n1, Node n2, Node n3) => Create(n1.Hash, n2.Hash, n3.Hash);

        private static NodeNetwork Create(int n1, int n2, int n3)
        {
            int highest = Math.Max(n1, Math.Max(n2, n3));
            int lowest = Math.Min(n1, Math.Min(n2, n3));
            int middle = n1 + n2 + n3 - highest - lowest;

            return new(lowest, middle, highest);
        }
    }

    // I decided to build a network of the hash codes instead to avoid string handling.
    // Since we're guaranteed 2 char strings, there's no collision risk.
    public readonly record struct Node(int Hash, bool IsTarget)
    {
        public static Node Parse(ReadOnlySpan<char> nodeString, char startsWith) =>
            new(string.GetHashCode(nodeString), nodeString.StartsWith(startsWith));

        public static (Node, Node) ParsePair(ReadOnlySpan<char> nodeString, char startsWith)
        {
            int sepInx = nodeString.IndexOf('-');
            var n1 = Parse(nodeString[..sepInx], startsWith);
            var n2 = Parse(nodeString[(sepInx + 1)..], startsWith);

            return (n1, n2);
        }
    }

    // This is a more memory optimized implementation of a node graph on top of a Dictionary
    public sealed class NodeGraph<T>
        where T : notnull
    {
        private readonly Dictionary<T, (T[] Leaves, int Count)> graph = [];

        public T[] this[T node] => Graph.TryGetValue(node, out var values) ? values.Leaves : [];

        public Dictionary<T, (T[] Leaves, int Count)> Graph => graph;

        public void Add(T n1, T n2)
        {
            AddRelationship(n1, n2);
            AddRelationship(n2, n1);
        }

        private void AddRelationship(T n1, T n2)
        {
            if (!graph.TryGetValue(n1, out var nodes))
                nodes = (ArrayPool<T>.Shared.Rent(8), 0);

            if (nodes.Count == nodes.Leaves.Length)
            {
                var newLeaves = ArrayPool<T>.Shared.Rent((int)(nodes.Count * 1.5));
                Array.Copy(nodes.Leaves, newLeaves, nodes.Count);
                ArrayPool<T>.Shared.Return(nodes.Leaves);
                nodes.Leaves = newLeaves;
            }

            nodes.Leaves[nodes.Count] = n2;
            nodes.Count++;

            graph[n1] = nodes;
        }

        public void Clear() => graph.Clear();
    }
}
