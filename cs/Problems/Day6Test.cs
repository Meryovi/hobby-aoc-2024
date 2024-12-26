namespace aoc24.Problems;

public class Day6Test
{
    private readonly Day6 sut = new();

    [Theory, InlineData(41)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day6_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(4973)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day6_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
