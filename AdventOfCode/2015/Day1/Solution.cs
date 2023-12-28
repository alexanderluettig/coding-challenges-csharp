using AdventOfCode;

namespace AdventOfCode.Y2015.Day1;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        return _input.Select(c => c switch
        {
            '(' => 1,
            ')' => -1,
            _ => throw new Exception("Invalid input")
        }).Sum();
    }

    public override long SolvePartTwo()
    {
        int floor = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            floor += _input[i] switch
            {
                '(' => 1,
                ')' => -1,
                _ => throw new Exception("Invalid input")
            };

            if (floor == -1)
                return i + 1;
        }

        return -1;
    }
}