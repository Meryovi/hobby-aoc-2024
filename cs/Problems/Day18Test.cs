namespace aoc24.Problems;

public class Day18Test
{
    private readonly Day18 sut = new();

    [Theory, InlineData(22)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day18_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(298)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day18_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
