namespace aoc24.Problems;

public class Day1Test
{
    private readonly Day1 sut = new();

    [Theory, InlineData(11)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day1_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(1110981)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day1_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
