namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day6Benchmark
{
    private readonly Day6 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day6_1");

    [Benchmark(Description = "Day6 problem")]
    public void Measure() => problem.Solve(input);
}
