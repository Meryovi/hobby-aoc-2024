namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day24Benchmark
{
    private readonly Day24 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day24_1");

    [Benchmark(Description = "Day24 problem")]
    public void Measure() => problem.Solve(input);
}
