namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day9Benchmark
{
    private readonly Day9 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day9_1");

    [Benchmark(Description = "Day9 problem")]
    public void Measure() => problem.Solve(input);
}
