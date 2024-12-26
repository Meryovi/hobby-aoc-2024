namespace aoc24.Problems;

public class Day5Test
{
    private readonly Day5 sut = new();

    [Theory, InlineData(143)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day5_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(7307)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day5_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
