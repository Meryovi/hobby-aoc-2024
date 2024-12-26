namespace aoc24;

public interface IProblem<T>
{
    T Solve(string input);
}
