namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day5Benchmark
{
    private readonly Day5 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day5_1");

    [Benchmark(Description = "Day5 problem")]
    public void Measure() => problem.Solve(input);
}
