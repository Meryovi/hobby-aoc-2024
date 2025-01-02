namespace aoc24.Problems;

// https://adventofcode.com/2024/day/13
public sealed class Day13 : IProblem<int>
{
    public int Solve(string input) => SmallestNumberOfTokensOptimized(input);

    private readonly PriorityQueue<Game, int> queue = new();

    private readonly HashSet<Game> history = [];

    private int SmallestNumberOfTokensOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine + InputReader.NewLine);

        int totalTokens = 0;

        while (iterator.MoveNext())
        {
            var problem = input[iterator.Current];
            int nlInx1 = problem.IndexOf(InputReader.NewLine);
            int nlInx2 = problem.LastIndexOf(InputReader.NewLine);

            var a = problem[..nlInx1];
            var b = problem[(nlInx1 + 1)..nlInx2];
            var p = problem[(nlInx2 + 1)..];

            var aMove = ParsePointFromInstruction(a);
            var bMove = ParsePointFromInstruction(b);
            var price = ParsePointFromInstruction(p);

            totalTokens += CalculateMinTokenCount(aMove, bMove, price);
        }

        return totalTokens;
    }

    private int SmallestNumberOfTokens(string input)
    {
        var problems = input.Split(InputReader.NewLine + InputReader.NewLine);

        int totalTokens = 0;

        foreach (var problem in problems)
        {
            if (problem.Split(InputReader.NewLine) is not [var a, var b, var p])
                continue;

            var aMove = ParsePointFromInstruction(a);
            var bMove = ParsePointFromInstruction(b);
            var price = ParsePointFromInstruction(p);

            totalTokens += CalculateMinTokenCount(aMove, bMove, price);
        }

        return totalTokens;
    }

    private int CalculateMinTokenCount(Point aMove, Point bMove, Point prize)
    {
        // I initially thought this was more a math problem than a coding one.
        // In the end, I decided to do a math optimization while still using well known algorithms to solve it.
        int gcdX = GreatestCommonDenominator(aMove.X, bMove.X);
        int gcdY = GreatestCommonDenominator(aMove.Y, bMove.Y);

        // Instead of trying all possible values, calculate the GCD for x and y, and based on that either
        // bail out if the goal is unreachable or reduce the number of combinations to test.
        if (prize.X % gcdX != 0 || prize.Y % gcdY != 0)
            return 0;

        // Scale down based on the calculated GCDs...
        aMove = new(aMove.X / gcdX, aMove.Y / gcdY);
        bMove = new(bMove.X / gcdX, bMove.Y / gcdY);
        prize = new(prize.X / gcdX, prize.Y / gcdY);

        history.Clear();
        queue.Clear();
        queue.Enqueue(new(1, 1), 0);

        Span<Game> nextSteps = [new(0, 1), new(1, 0)];

        while (queue.TryDequeue(out var game, out var _))
        {
            var reach = aMove * game.ACount + bMove * game.BCount;

            if (reach == prize)
                return game.TokenCost;

            if (reach > prize)
                continue;

            foreach (var step in nextSteps)
            {
                var nextGame = new Game(game.ACount + step.ACount, game.BCount + step.BCount);
                if (nextGame.ACount <= 100 && nextGame.BCount <= 100 && !history.Contains(nextGame))
                {
                    queue.Enqueue(nextGame, nextGame.TokenCost + getHeuristicCost(nextGame));
                    history.Add(nextGame);
                }
            }
        }

        // This function helps prioritize combinations more likely to get us to the prize.
        // A feeble last attempt to get the execution time for the full set under 500ms.
        // Thanks ChatGPT for the formula.
        int getHeuristicCost(Game game) =>
            Math.Abs(prize.X - (aMove.X * game.ACount + bMove.X * game.BCount))
            + Math.Abs(prize.Y - (aMove.Y * game.ACount + bMove.Y * game.BCount));

        return 0;
    }

    private static int GreatestCommonDenominator(int a, int b) => b == 0 ? a : GreatestCommonDenominator(b, a % b);

    private static Point ParsePointFromInstruction(ReadOnlySpan<char> value)
    {
        int xInx = value.IndexOfAny(['+', '=']) + 1;
        int xSep = value.IndexOf(',');
        int yInx = value.LastIndexOfAny(['+', '=']) + 1;

        int x = int.Parse(value[xInx..xSep]);
        int y = int.Parse(value[yInx..]);
        return new(x, y);
    }

    private readonly record struct Game(int ACount, int BCount)
    {
        public readonly int TokenCost => ACount * 3 + BCount;
    }
}
