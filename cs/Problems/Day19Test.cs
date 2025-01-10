namespace aoc24.Problems;

public class Day19Test
{
    private readonly Day19 sut = new();

    [Theory, InlineData(6)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day19_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(302)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day19_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
