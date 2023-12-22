namespace AdventOfCode.Y2023.Day13;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        string[][] map = [];
        var result = 0;
        foreach (var line in _input.Split(Environment.NewLine))
        {
            if (line == "")
            {
                result += CalculatePart1(map);
                map = [];
                continue;
            }
            else
            {
                map = [.. map, line.ToCharArray().Select(c => c.ToString()).ToArray()];
            }
        }

        result += CalculatePart2(map);

        return result;
    }

    public override long SolvePartTwo()
    {
        string[][] map = [];
        var result = 0;
        foreach (var line in _input.Split(Environment.NewLine))
        {
            if (line == "")
            {
                result += CalculatePart2(map);
                map = [];
                continue;
            }
            else
            {
                map = [.. map, line.ToCharArray().Select(c => c.ToString()).ToArray()];
            }
        }

        result += CalculatePart2(map);

        return result;
    }

    private static int CalculatePart1(string[][] input)
    {
        if (HasMirror(input, out var row))
        {
            return row * 100;
        }

        return HasMirror(input.Transpose(), out var column) ? column : 0;
    }

    private static int CalculatePart2(string[][] input)
    {
        if (HasMirrorWithSmudges(input, out var row))
        {
            return row * 100;
        }

        return HasMirrorWithSmudges(input.Transpose(), out var column) ? column : 0;
    }

    private static bool HasMirrorWithSmudges(string[][] input, out int row)
    {
        row = -1;

        for (var i = 0; i < input.Length; i++)
        {
            var upperLine = i;
            var bottomLine = i + 1;

            var numberOfSmudges = 0;
            while (upperLine >= 0 && bottomLine < input.Length)
            {
                var topLine = string.Join("", input[upperLine]);
                var botLine = string.Join("", input[bottomLine]);

                numberOfSmudges += topLine.NumberOfDifferences(botLine);
                upperLine--;
                bottomLine++;
            }

            if (numberOfSmudges == 1)
            {
                row = i + 1;
                return true;
            }
        }

        return false;
    }

    private static bool HasMirror(string[][] input, out int row)
    {
        row = -1;

        for (var i = 0; i < input.Length; i++)
        {
            var upperLine = i;
            var bottomLine = i + 1;

            var isMirror = false;
            while (upperLine >= 0 && bottomLine < input.Length)
            {
                var topLine = string.Join("", input[upperLine]);
                var botLine = string.Join("", input[bottomLine]);
                if (topLine.NumberOfDifferences(botLine) > 0)
                {
                    isMirror = false;
                    break;
                }
                else
                {
                    isMirror = true;
                }

                upperLine--;
                bottomLine++;
            }

            if (isMirror)
            {
                row = i + 1;
                return true;
            }
        }

        return false;
    }
}