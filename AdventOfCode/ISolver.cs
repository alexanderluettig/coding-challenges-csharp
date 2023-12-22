namespace AdventOfCode;

public abstract class ISolver(string input)
{
    protected readonly string _input = input;
    public abstract long SolvePartOne();
    public abstract long SolvePartTwo();
}
