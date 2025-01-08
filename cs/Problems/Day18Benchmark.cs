namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day18Benchmark
{
    private readonly Day18 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day18_1");

    [Benchmark(Description = "Day18 problem")]
    public void Measure() => problem.Solve(input);
}
