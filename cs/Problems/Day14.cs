namespace aoc24.Problems;

using QuadrantBounds = (Point Start, Point End);

// https://adventofcode.com/2024/day/14
public sealed class Day14 : IProblem<int>
{
    public int Solve(string input) => DetermineRobotSafetyFactorOptimized(input);

    private static int DetermineRobotSafetyFactorOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine);
        iterator.MoveNext();

        var sizeStr = input[iterator.Current];
        int sepInx = sizeStr.IndexOf(',');
        int width = int.Parse(sizeStr[..sepInx]);
        int height = int.Parse(sizeStr[(sepInx + 1)..]);

        var (q1, q2, q3, q4) = BuildQuadrantMap(width, height);
        Span<QuadrantBounds> quadrants = [q1, q2, q3, q4];
        Span<int> quadrantRobotCounts = [0, 0, 0, 0];

        while (iterator.MoveNext())
        {
            // Parse and move robot.
            var robot = Robot.FromString(input[iterator.Current]);
            robot.MoveWithin(width, height, steps: 100);

            // Which quadrant is the robot in?
            for (int i = 0; i < quadrants.Length; i++)
            {
                var quadrant = quadrants[i];
                if (robot.Position.IsBetween(quadrant.Start, quadrant.End))
                {
                    quadrantRobotCounts[i]++;
                    break;
                }
            }
        }

        int safetyFactor = 1;

        foreach (var robotCount in quadrantRobotCounts)
            safetyFactor *= robotCount;

        return safetyFactor;
    }

    private static (QuadrantBounds, QuadrantBounds, QuadrantBounds, QuadrantBounds) BuildQuadrantMap(int width, int height)
    {
        // These quadrants are kinda confusing... but we have to skip the middle row and column.
        QuadrantBounds q1 = (new(0, 0), new(width / 2 - 1, height / 2 - 1));
        QuadrantBounds q2 = (new(width / 2 + 1, 0), new(width, height / 2 - 1));
        QuadrantBounds q3 = (new(0, height / 2 + 1), new(width / 2 - 1, height));
        QuadrantBounds q4 = (new(width / 2 + 1, height / 2 + 1), new(width, height));

        return (q1, q2, q3, q4);
    }

    private record struct Robot(Point Position, Point Velocity)
    {
        public static Robot FromString(ReadOnlySpan<char> robotString)
        {
            int pInx = robotString.IndexOf('=') + 1;
            int vInx = robotString.LastIndexOf('=') + 1;

            var position = Point.FromString(robotString[pInx..(vInx - 3)]);
            var velocity = Point.FromString(robotString[vInx..]);
            return new Robot(position, velocity);
        }

        public void MoveWithin(int width, int height, int steps)
        {
            var next = Position + Velocity * steps;

            int nx = next.X % width;
            int ny = next.Y % height;
            nx = nx < 0 ? width + nx : nx;
            ny = ny < 0 ? height + ny : ny;

            if (next.X != nx || next.Y != ny)
                next = new(nx, ny);

            Position = next;
        }
    }
}
