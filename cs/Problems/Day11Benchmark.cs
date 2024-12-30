namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day11Benchmark
{
    private readonly Day11 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day11_1");

    [Benchmark(Description = "Day11 problem")]
    public void Measure() => problem.Solve(input);
}
