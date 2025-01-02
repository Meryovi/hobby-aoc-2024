namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day15Benchmark
{
    private readonly Day15 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day15_1");

    [Benchmark(Description = "Day15 problem")]
    public void Measure() => problem.Solve(input);
}
