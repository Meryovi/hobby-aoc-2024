namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day10Benchmark
{
    private readonly Day10 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day10_1");

    [Benchmark(Description = "Day10 problem")]
    public void Measure() => problem.Solve(input);
}
