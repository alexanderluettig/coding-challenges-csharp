using System.Text;
namespace AdventOfCode._2023.Day13;

internal class Solution
{
    private static async Task Method(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] input = [];
        var part1 = 0;
        var part2 = 0;
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            if (line == "")
            {
                part1 += CalculatePart1(input);
                part2 += CalculatePart2(input);
                input = [];
                continue;
            }
            else
            {
                input = [.. input, line.ToCharArray().Select(c => c.ToString()).ToArray()];
            }
        }

        part1 += CalculatePart1(input);
        part2 += CalculatePart2(input);

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
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