namespace aoc24.Problems;

public class Day21Test
{
    private readonly Day21 sut = new();

    [Theory, InlineData(126384)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day21_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(238078)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day21_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
