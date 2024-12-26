namespace aoc24.Problems;

public class Day2Test
{
    private readonly Day2 sut = new();

    [Theory, InlineData(2)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day2_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(486)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day2_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
