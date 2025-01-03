namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day16Benchmark
{
    private readonly Day16 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day16_1");

    [Benchmark(Description = "Day16 problem")]
    public void Measure() => problem.Solve(input);
}
