namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day12Benchmark
{
    private readonly Day12 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day12_1");

    [Benchmark(Description = "Day12 problem")]
    public void Measure() => problem.Solve(input);
}
