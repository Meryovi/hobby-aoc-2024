namespace aoc24.Problems;

public class Day10Test
{
    private readonly Day10 sut = new();

    [Theory, InlineData(36)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day10_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(822)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day10_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
