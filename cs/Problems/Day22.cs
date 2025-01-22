namespace aoc24.Problems;

// https://adventofcode.com/2024/day/22
public sealed class Day22 : IProblem<long>
{
    public long Solve(string input) => CalculateSecretNumbersOptimized(input);

    private static long CalculateSecretNumbersOptimized(ReadOnlySpan<char> input)
    {
        var iterator = input.Split(InputReader.NewLine);

        long secretSum = 0;

        while (iterator.MoveNext())
        {
            int secret = int.Parse(input[iterator.Current]);
            secretSum += CalculateSecretNumber(secret);
        }

        return secretSum;
    }

    private static long CalculateSecretNumber(int secret, int iterations = 2000)
    {
        long seaCrab = secret;

        for (int i = 0; i < iterations; i++)
        {
            seaCrab = MixAndPruneSecret(seaCrab, seaCrab * 64);
            seaCrab = MixAndPruneSecret(seaCrab, seaCrab / 32);
            seaCrab = MixAndPruneSecret(seaCrab, seaCrab * 2048);
        }

        static long MixAndPruneSecret(long secret, long value) => (secret ^ value) % 16777216;

        return seaCrab;
    }
}
