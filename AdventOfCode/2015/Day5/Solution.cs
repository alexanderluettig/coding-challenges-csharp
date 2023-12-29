namespace AdventOfCode.Y2015.Day5;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var result = 0;

        foreach (var line in _input.Split(Environment.NewLine))
        {
            if (IsNicePartOne(line))
                result++;
        }

        return result;
    }

    public override long SolvePartTwo()
    {
        var result = 0;

        foreach (var line in _input.Split(Environment.NewLine))
        {
            if (IsNicePartTwo(line))
                result++;
        }

        return result;
    }

    private static bool IsNicePartOne(string line)
    {
        var vowels = line.Count("aeiou".Contains);

        var doubleLetter = false;
        for (var i = 0; i < line.Length - 1; i++)
        {
            if (line[i] == line[i + 1])
            {
                doubleLetter = true;
                break;
            }
        }

        var badStrings = new[] { "ab", "cd", "pq", "xy" };
        var badString = badStrings.Any(line.Contains);

        return vowels >= 3 && doubleLetter && !badString;
    }

    private static bool IsNicePartTwo(string line)
    {

        var doublePair = false;
        var pairWithLetterBetween = false;
        for (var i = 0; i < line.Length - 1; i++)
        {
            var pair = line.Substring(i, 2);
            if (line.IndexOf(pair, i + 2) != -1)
                doublePair = true;

            if (i < line.Length - 2 && line[i] == line[i + 2])
                pairWithLetterBetween = true;
        }

        return doublePair && pairWithLetterBetween;
    }
}