namespace aoc24.Problems;

// https://adventofcode.com/2024/day/20
public sealed class Day20 : IProblem<int>
{
    public int Solve(string input) => CountCheatOptimizedRoutesOptimized(input);

    private readonly HashSet<RaceStep> history = [];

    private readonly Dictionary<int, int> goalHistory = [];

    private readonly PriorityQueue<RaceStep, int> queue = new();

    private int CountCheatOptimizedRoutesOptimized(string input)
    {
        Span<char> inner = stackalloc char[Matrix<char>.SizeFor(input)];
        var matrix = Matrix<char>.CreateFrom(input);

        var start = matrix.SeekItem('S')!.Value;
        var end = matrix.SeekItem('E')!.Value;

        history.Clear();
        queue.Clear();
        goalHistory.Clear();

        foreach (var direction in NavigableDirections)
            queue.Enqueue(new RaceStep(new(direction, start)), 0);

        int baseSteps = int.MaxValue;

        while (queue.TryDequeue(out var step, out var priority))
        {
            if (step.Location.Position == end)
            {
                goalHistory.TryAdd(priority, 0);
                goalHistory[priority]++;

                if (step.CheatStart is null) // Goal with no cheats is the baseline.
                    baseSteps = priority;

                continue;
            }

            foreach (var direction in NavigableDirections)
            {
                var location = step.Location.Rotate(direction).MoveForward();

                if (matrix.ItemAt(location.Position) is null)
                    continue;

                var cheatStart = step.CheatStart;
                var cheatEnd = step.CheatEnd;

                if (matrix.ItemAt(location.Position) == '#') // If it's a wall, start cheat.
                {
                    if (cheatStart is not null)
                        continue;

                    cheatStart = location.Position;
                }

                if (cheatStart is not null && cheatEnd is null) // If the cheat was started, end it on the next step.
                    cheatEnd = location.Position;

                var nextStep = new RaceStep(location, cheatStart, cheatEnd);

                if (history.Contains(nextStep) || priority + 1 > baseSteps)
                    continue;

                queue.Enqueue(nextStep, priority + 1);
                history.Add(nextStep);
            }
        }

        int totalOptimized = 0;

        foreach (var (steps, count) in goalHistory)
            if (baseSteps - steps >= 100)
                totalOptimized += count;

        return totalOptimized;
    }

    private readonly record struct RaceStep(FacingPoint Location, Point? CheatStart = null, Point? CheatEnd = null);

    private static readonly Direction[] NavigableDirections = [Direction.Right, Direction.Left, Direction.Up];
}
