namespace aoc24.Problems;

public class Day20Test
{
    private readonly Day20 sut = new();

    [Theory, InlineData(0)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day20_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    // [Theory, InlineData(1399)] // Too computationally expensive, disable.
    private void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day20_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
