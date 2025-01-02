namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day13Benchmark
{
    private readonly Day13 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day13_1");

    [Benchmark(Description = "Day13 problem")]
    public void Measure() => problem.Solve(input);
}
