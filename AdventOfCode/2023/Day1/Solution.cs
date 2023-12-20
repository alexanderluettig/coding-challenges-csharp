using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day1;
internal class Solution(string input) : ISolver(input)
{
    private static readonly string[] numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

    public override long SolvePartOne()
    {
        int counter = 0;

        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var lineWithoutCharacters = Regex.Replace(line, @"\D", "");
            var numberString = lineWithoutCharacters[0].ToString() + lineWithoutCharacters[^1].ToString();
            counter += int.Parse(numberString);
        }

        return counter;
    }

    public override long SolvePartTwo()
    {
        long counter = 0;

        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var newLine = "";
            for (int i = 0; i < line.Length; i++)
            {
                string current = line[i].ToString();

                if (int.TryParse(current, out _))
                {
                    newLine += current;
                }
                else
                {
                    var subString = ParseSubstring(line, i);

                    if (int.TryParse(subString, out _))
                    {
                        newLine += subString;
                    }
                }

            }

            var numberString = newLine[0].ToString() + newLine[^1].ToString();
            counter += int.Parse(numberString);
        }

        return counter;
    }

    private static string ParseSubstring(string line, int index)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (index + numbers[i].Length > line.Length)
            {
                continue;
            }

            if (line.Substring(index, numbers[i].Length) == numbers[i])
            {
                return (i + 1).ToString();
            }
        }

        return "NaN";
    }
}