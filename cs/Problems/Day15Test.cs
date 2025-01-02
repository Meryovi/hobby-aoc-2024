namespace aoc24.Problems;

public class Day15Test
{
    private readonly Day15 sut = new();

    [Theory, InlineData(2028)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day15_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(10092)]
    public void TestSet2_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day15_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(1_490_942)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day15_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
