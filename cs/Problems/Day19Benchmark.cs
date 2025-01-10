namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day19Benchmark
{
    private readonly Day19 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day19_1");

    [Benchmark(Description = "Day19 problem")]
    public void Measure() => problem.Solve(input);
}
