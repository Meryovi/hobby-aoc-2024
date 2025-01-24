namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day23Benchmark
{
    private readonly Day23 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day23_1");

    [Benchmark(Description = "Day23 problem")]
    public void Measure() => problem.Solve(input);
}
