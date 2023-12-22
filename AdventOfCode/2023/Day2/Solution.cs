using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day2;

internal class Solution(string input) : ISolver(input)
{
    private static readonly int MAXREDS = 12;
    private static readonly int MAXGREENS = 13;
    private static readonly int MAXBLUES = 14;

    public override long SolvePartOne()
    {
        var result = 0;
        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var extractedGameIdString = Regex.Replace(line, @"Game (\d{1,3})", "$1");
            var game = extractedGameIdString.Split(":");
            var gameId = int.Parse(game[0]);
            var rounds = game[1].Split(";");

            var gameIsPossible = true;

            foreach (var round in rounds)
            {
                var colors = round.Split(",");
                foreach (var color in colors)
                {
                    if (color.Contains("red"))
                    {
                        var red = int.Parse(color.Replace("red", "").Trim());
                        if (red > MAXREDS)
                        {
                            gameIsPossible = false;
                            break;
                        }
                    }
                    else if (color.Contains("green"))
                    {
                        var green = int.Parse(color.Replace("green", "").Trim());
                        if (green > MAXGREENS)
                        {
                            gameIsPossible = false;
                            break;
                        }
                    }
                    else if (color.Contains("blue"))
                    {
                        var blue = int.Parse(color.Replace("blue", "").Trim());
                        if (blue > MAXBLUES)
                        {
                            gameIsPossible = false;
                            break;
                        }
                    }
                }
            }

            if (gameIsPossible)
            {
                result += gameId;
            }
        }

        return result;
    }

    public override long SolvePartTwo()
    {
        var result = 0;
        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var game = line.Split(":");
            var rounds = game[1].Split(";");

            var roundMaxReds = 0;
            var roundMaxGreens = 0;
            var roundMaxBlues = 0;
            foreach (var round in rounds)
            {
                var colors = round.Split(",");

                var reds = 0;
                var greens = 0;
                var blues = 0;

                foreach (var color in colors)
                {
                    if (color.Contains("red"))
                    {
                        reds += int.Parse(color.Replace("red", "").Trim());
                    }
                    else if (color.Contains("green"))
                    {
                        greens += int.Parse(color.Replace("green", "").Trim());
                    }
                    else if (color.Contains("blue"))
                    {
                        blues += int.Parse(color.Replace("blue", "").Trim());
                    }
                }

                roundMaxReds = Math.Max(roundMaxReds, reds);
                roundMaxGreens = Math.Max(roundMaxGreens, greens);
                roundMaxBlues = Math.Max(roundMaxBlues, blues);
            }

            result += roundMaxReds * roundMaxGreens * roundMaxBlues;
        }

        return result;
    }
}