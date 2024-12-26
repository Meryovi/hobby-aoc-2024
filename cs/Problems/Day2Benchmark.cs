namespace aoc24.Problems;

[MemoryDiagnoser, ShortRunJob]
public class Day2Benchmark
{
    private readonly Day2 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day2_1");

    [Benchmark(Description = "Day2 problem")]
    public void Measure() => problem.Solve(input);
}
