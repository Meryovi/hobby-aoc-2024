namespace aoc24.Problems;

// https://adventofcode.com/2024/day/15
public sealed class Day15 : IProblem<int>
{
    public int Solve(string input) => SumBoxesGpsCoordsOptimized(input);

    private static int SumBoxesGpsCoordsOptimized(ReadOnlySpan<char> input)
    {
        Span<Range> sections = stackalloc Range[2];
        input.Split(sections, InputReader.NewLine + InputReader.NewLine);

        var map = input[sections[0]];
        var instructions = input[sections[1]];

        Span<char> inner = stackalloc char[CharMatrix.SizeFor(map)];
        var matrix = CharMatrix.CreateFrom(map, inner);
        var robot = matrix.SeekChar('@')!.Value;

        foreach (var instruction in instructions)
        {
            if (char.IsWhiteSpace(instruction))
                continue;

            var direction = InstructionDirectionMap[instruction];
            robot = MoveItemInMatrix(robot, matrix, direction);
        }

        int gpsCoordsSum = 0;

        for (int y = 0; y < matrix.Height; y++)
        for (int x = 0; x < matrix.Width; x++)
            gpsCoordsSum += matrix.CharAt(x, y) == 'O' ? y * 100 + x : 0;

        return gpsCoordsSum;
    }

    private static Point MoveItemInMatrix(Point current, CharMatrix matrix, Point direction)
    {
        var next = current + direction;
        var nextChar = matrix.CharAt(next);

        if (nextChar is null or '#')
            return current;

        if (nextChar == 'O')
        {
            var nextNext = MoveItemInMatrix(next, matrix, direction);

            if (nextNext == next)
                return current;
        }

        SwapElements(matrix, current, next);

        return next;
    }

    private static void SwapElements(CharMatrix matrix, Point pointA, Point pointB)
    {
        var charA = matrix.CharAt(pointA)!.Value;
        var charB = matrix.CharAt(pointB)!.Value;

        matrix.ReplaceCharAt(pointA, charB);
        matrix.ReplaceCharAt(pointB, charA);
    }

    private static readonly Dictionary<char, Point> InstructionDirectionMap =
        new()
        {
            { '>', Point.FromDirection(Direction.Right) },
            { '<', Point.FromDirection(Direction.Left) },
            { '^', Point.FromDirection(Direction.Up) },
            { 'v', Point.FromDirection(Direction.Down) },
        };
}
