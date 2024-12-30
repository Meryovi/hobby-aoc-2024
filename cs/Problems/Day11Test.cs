namespace aoc24.Problems;

public class Day11Test
{
    private readonly Day11 sut = new();

    [Theory, InlineData(55_312)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day11_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(200_446)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day11_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
