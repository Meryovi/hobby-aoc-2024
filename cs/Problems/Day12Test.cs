namespace aoc24.Problems;

public class Day12Test
{
    private readonly Day12 sut = new();

    [Theory, InlineData(140)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(772)]
    public void TestSet2_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(1930)]
    public void TestSet3_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(1_396_562)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_4");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
