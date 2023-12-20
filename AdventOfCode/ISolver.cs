namespace AdventOfCode;

public abstract class ISolver(string input)
{
    protected readonly string input = input;
    public abstract long SolvePartOne();
    public abstract long SolvePartTwo();
}
