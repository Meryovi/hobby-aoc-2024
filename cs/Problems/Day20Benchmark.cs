namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day20Benchmark
{
    private readonly Day20 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day20_1");

    [Benchmark(Description = "Day20 problem")]
    public void Measure() => problem.Solve(input);
}
