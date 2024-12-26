namespace aoc24.Problems;

public class Day3Test
{
    private readonly Day3 sut = new();

    [Theory, InlineData(161)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day3_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(188741603)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day3_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
