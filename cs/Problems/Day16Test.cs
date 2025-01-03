namespace aoc24.Problems;

public class Day16Test
{
    private readonly Day16 sut = new();

    [Theory, InlineData(7036)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day16_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(107512)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day16_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
