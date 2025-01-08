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

        Span<char> inner = stackalloc char[Matrix<char>.SizeFor(map)];
        var matrix = Matrix<char>.CreateFrom(map, inner);

        var robot = matrix.SeekItem('@')!.Value;

        foreach (var instruction in instructions)
        {
            if (char.IsWhiteSpace(instruction))
                continue;

            var direction = InstructionDirectionMap[instruction];
            robot = MoveRobotInMatrix(robot, matrix, direction);
        }

        int gpsCoordsSum = 0;

        for (int y = 0; y < matrix.Height; y++)
        for (int x = 0; x < matrix.Width; x++)
            gpsCoordsSum += matrix.ItemAt(x, y) == 'O' ? y * 100 + x : 0;

        return gpsCoordsSum;
    }

    private static Point MoveRobotInMatrix(Point robot, Matrix<char> matrix, Point direction)
    {
        var next = robot + direction;
        var nextChar = matrix.ItemAt(next);

        if (nextChar is null or '#')
            return robot;

        if (nextChar == 'O')
        {
            var nextNext = MoveRobotInMatrix(next, matrix, direction);

            if (nextNext == next)
                return robot;
        }

        SwapElements(matrix, robot, next);

        return next;
    }

    private static void SwapElements(Matrix<char> matrix, Point pointA, Point pointB)
    {
        var charA = matrix.ItemAt(pointA)!.Value;
        var charB = matrix.ItemAt(pointB)!.Value;

        matrix.ReplaceAt(pointA, charB);
        matrix.ReplaceAt(pointB, charA);
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
