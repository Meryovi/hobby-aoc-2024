namespace aoc24.Problems;

public class Day23Test
{
    private readonly Day23 sut = new();

    [Theory, InlineData(7)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day23_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    // [Theory, InlineData(1330)]
    // public void FullSet_ShouldYield_Result(int expected)
    // {
    //     var input = InputReader.ReadProblemInput("day23_2");
    //     var result = sut.Solve(input);

    //     Assert.Equal(expected, result);
    // }
}
