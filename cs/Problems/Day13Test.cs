namespace aoc24.Problems;

public class Day13Test
{
    private readonly Day13 sut = new();

    [Theory, InlineData(480)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day13_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(31761)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day13_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
