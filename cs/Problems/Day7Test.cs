namespace aoc24.Problems;

public class Day7Test
{
    private readonly Day7 sut = new();

    [Theory, InlineData(3749)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day7_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(1298103531759)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day7_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
