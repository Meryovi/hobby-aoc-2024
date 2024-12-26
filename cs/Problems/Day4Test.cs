namespace aoc24.Problems;

public sealed class Day4Test
{
    private readonly Day4 sut = new();

    [Theory, InlineData(18)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day4_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(2575)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day4_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
