namespace aoc24.Problems;

public class Day25Test
{
    private readonly Day25 sut = new();

    [Theory, InlineData(3)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day25_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(3116)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day25_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
