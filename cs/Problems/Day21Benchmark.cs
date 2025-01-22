namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day21Benchmark
{
    private readonly Day21 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day21_1");

    [Benchmark(Description = "Day21 problem")]
    public void Measure() => problem.Solve(input);
}
