namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day17Benchmark
{
    private readonly Day17 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day17_1");

    [Benchmark(Description = "Day17 problem")]
    public void Measure() => problem.Solve(input);
}
