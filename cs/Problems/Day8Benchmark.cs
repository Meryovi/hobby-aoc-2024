namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day8Benchmark
{
    private readonly Day8 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day8_1");

    [Benchmark(Description = "Day8 problem")]
    public void Measure() => problem.Solve(input);
}
