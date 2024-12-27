namespace aoc24.Problems;

public class Day9Test
{
    private readonly Day9 sut = new();

    [Theory, InlineData(1928)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day9_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(6242766523059)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day9_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
