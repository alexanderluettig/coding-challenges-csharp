namespace AdventOfCode.Y2023.Day6;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var times = inputLines[0][5..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distances = inputLines[1][9..].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var variations = 1;

        for (var i = 0; i < times.Length; i++)
            variations *= CalculateWinVariations(long.Parse(times[i]), long.Parse(distances[i]));

        return variations;
    }

    public override long SolvePartTwo()
    {
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var time = long.Parse(inputLines[0][5..].Replace(" ", ""));
        var distance = long.Parse(inputLines[1][9..].Replace(" ", ""));

        return CalculateWinVariations(time, distance);
    }

    private static int CalculateWinVariations(long time, long distance)
    {
        double p = -1 * time;
        double q = distance + 0.001;
        var x1 = -(p / 2) + Math.Sqrt(Math.Pow(p / 2, 2) - q);
        var x2 = -(p / 2) - Math.Sqrt(Math.Pow(p / 2, 2) - q);

        var lowerBound = (int)Math.Ceiling(Math.Min(x1, x2));
        var upperBound = (int)Math.Floor(Math.Max(x1, x2));

        return upperBound - lowerBound + 1;
    }
}

record Race(int Time, int Distance);