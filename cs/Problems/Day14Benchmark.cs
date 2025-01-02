namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day14Benchmark
{
    private readonly Day14 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day14_1");

    [Benchmark(Description = "Day14 problem")]
    public void Measure() => problem.Solve(input);
}
