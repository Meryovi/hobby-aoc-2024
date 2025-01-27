namespace aoc24.Problems;

public class Day24Test
{
    private readonly Day24 sut = new();

    [Theory, InlineData(4)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day24_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(2024)]
    public void TestSet2_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day24_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(48063513640678)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day24_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
