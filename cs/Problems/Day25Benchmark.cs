namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day25Benchmark
{
    private readonly Day25 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day25_1");

    [Benchmark(Description = "Day25 problem")]
    public void Measure() => problem.Solve(input);
}
