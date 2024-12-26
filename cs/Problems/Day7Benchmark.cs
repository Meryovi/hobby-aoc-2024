namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day7Benchmark
{
    private readonly Day7 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day7_1");

    [Benchmark(Description = "Day7 problem")]
    public void Measure() => problem.Solve(input);
}
