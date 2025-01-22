namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day22Benchmark
{
    private readonly Day22 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day22_1");

    [Benchmark(Description = "Day22 problem")]
    public void Measure() => problem.Solve(input);
}
