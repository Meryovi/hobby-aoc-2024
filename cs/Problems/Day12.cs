namespace aoc24.Problems;

// https://adventofcode.com/2024/day/12
public sealed class Day12 : IProblem<int>
{
    public int Solve(string input) => CalculateFencingPriceOptimized(input);

    private static int CalculateFencingPriceOptimized(ReadOnlySpan<char> input)
    {
        Span<char> innerMatrix = stackalloc char[CharMatrix.SizeFor(input)];
        Span<char> innerHistory = stackalloc char[innerMatrix.Length];

        var matrix = CharMatrix.CreateFrom(input, innerMatrix);
        var history = CharMatrix.CreateEmpty(matrix.Width, matrix.Height, innerHistory, '0');

        int totalPrice = 0;

        for (int y = 0; y < matrix.Height; y++)
        for (int x = 0; x < matrix.Width; x++)
            totalPrice += ScanGarden(matrix, history, new(x, y)).FencingPrice;

        return totalPrice;
    }

    private static int CalculateFencingPrice(string input)
    {
        // The only difference is the backing memory store for the char matrix.
        // In this case we are using arrays, so there is some allocation involved.
        var matrix = CharMatrix.CreateFrom(input);
        var history = CharMatrix.CreateEmpty(matrix.Width, matrix.Height, '0');

        int totalPrice = 0;

        for (int y = 0; y < matrix.Height; y++)
        for (int x = 0; x < matrix.Width; x++)
            totalPrice += ScanGarden(matrix, history, new(x, y)).FencingPrice;

        return totalPrice;
    }

    private static Garden ScanGarden(CharMatrix matrix, CharMatrix history, Point location)
    {
        var type = matrix.CharAt(location)!.Value;

        if (history.CharAt(location) == '1') // Already visited. Short-circuit.
            return new(type);

        var region = new Garden(type);

        ScanGardenPerimeter(matrix, history, location);

        void ScanGardenPerimeter(CharMatrix matrix, CharMatrix history, Point location)
        {
            var type = matrix.CharAt(location);
            bool wasVisited = history.CharAt(location) == '1';

            if (type != region.Type)
            {
                region.Perimeter++;
                return;
            }

            if (wasVisited || type is null)
                return;

            history.ReplaceCharAt(location, '1');
            region.Area++;

            foreach (var direction in NavigableDirections)
                ScanGardenPerimeter(matrix, history, location + Point.FromDirection(direction));
        }

        return region;
    }

    private record struct Garden(char Type, int Area = 0, int Perimeter = 0)
    {
        public readonly int FencingPrice => Perimeter * Area;
    }

    private static readonly Direction[] NavigableDirections = [Direction.Left, Direction.Down, Direction.Right, Direction.Up];
}
