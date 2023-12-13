using System.Text;
using Lib;
using Lib.Extensions;

namespace AoC._2023.Day13;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] input = [];
        var result = 0;
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            if (line == "")
            {
                result += CalculatePart1(input);
                input = [];
                continue;
            }
            else
            {
                input = [.. input, line.ToCharArray().Select(c => c.ToString()).ToArray()];
            }
        }

        result += CalculatePart1(input);

        Console.WriteLine($"Result: {result}");
    }

    private static int CalculatePart1(string[][] input)
    {
        if (HasMirror(input, out var row))
        {
            return row * 100;
        }

        return HasMirror(input.Transpose(), out var column) ? column : 0;
    }

    private static bool HasMirror(string[][] input, out int row)
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
}