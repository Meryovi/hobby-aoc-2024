namespace aoc24.Problems;

public class Day8Test
{
    private readonly Day8 sut = new();

    [Theory, InlineData(14)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day8_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(228)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day8_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
