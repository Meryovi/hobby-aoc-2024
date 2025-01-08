namespace aoc24.Problems;

public class Day17Test
{
    private readonly Day17 sut = new();

    // [Theory, InlineData("4,6,3,5,6,3,5,2,1,0")]
    // public void TestSet_ShouldYield_Result(string expected)
    // {
    //     var input = InputReader.ReadProblemInput("day17_1");
    //     var result = sut.Solve(input);

    //     Assert.Equal(expected, result);
    // }

    // [Theory, InlineData("4,2,5,6,7,7,7,7,3,1,0")]
    // public void TestSet2_ShouldYield_Result(string expected)
    // {
    //     var input = InputReader.ReadProblemInput("day17_2");
    //     var result = sut.Solve(input);

    //     Assert.Equal(expected, result);
    // }

    [Theory, InlineData("7,6,5,3,6,5,7,0,4")]
    public void FullSet_ShouldYield_Result(string expected)
    {
        var input = InputReader.ReadProblemInput("day17_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
