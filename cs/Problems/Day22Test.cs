namespace aoc24.Problems;

public class Day22Test
{
    private readonly Day22 sut = new();

    [Theory, InlineData(37327623)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day22_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(20215960478)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day22_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
