using System.Text;
using System.Text.RegularExpressions;

namespace AoC._2023.Day3;

internal class Program
{
    // Got this idea for part two from https://github.com/encse/adventofcode/blob/master/2023/Day03/Solution.cs
    private readonly static Regex gearRegex = new Regex(@"\*");
    private readonly static Regex numberRegex = new Regex(@"\d+");

    private static async Task Main(string[] args)
    {
        // await Part1();
        await Part2();
    }

    private static async Task Part2()
    {
        await using var stream = typeof(Program).Assembly
                .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var gears = new List<Part>();
        var numbers = new List<Part>();

        int row = 0;
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            gears.AddRange(new Regex(@"\*").Matches(line).Select(g => new Part(g.Value, row, g.Index)));
            numbers.AddRange(new Regex(@"\d+").Matches(line).Select(g => new Part(g.Value, row, g.Index)));

            row++;
        }

        int sum = 0;
        foreach (var gear in gears)
        {
            var neighbors = numbers.Where(num =>
                Math.Abs(num.Row - gear.Row) <= 1 &&
                num.Column <= gear.Column + gear.Text.Length &&
                gear.Column <= num.Column + num.Text.Length
            );

            if (neighbors.Count() == 2)
            {
                sum += neighbors.First().ParseToNumber * neighbors.Last().ParseToNumber;
            }
        }

        Console.WriteLine(sum);
    }

    private record Part(string Text, int Row, int Column)
    {
        public int ParseToNumber => int.Parse(Text);
    }

    private static async Task Part1()
    {
        await using var stream = typeof(Program).Assembly
                .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var schematic = Array.Empty<string[]>();

        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            var split = line.ToCharArray().Select(c => c.ToString()).ToArray();
            schematic = [.. schematic, split];
        }

        var result = 0;

        for (var i = 0; i < schematic.Length; i++)
        {
            var row = schematic[i];
            var currentNumber = "";
            var isPartNumber = false;
            for (var j = 0; j < row.Length; j++)
            {
                var symbol = row[j];
                if (int.TryParse(symbol, out var _))
                {
                    if (IsPartNumber(schematic, i, j))
                    {
                        isPartNumber = true;
                    }

                    currentNumber += symbol;
                }
                else
                {
                    if (isPartNumber)
                    {
                        result += int.Parse(currentNumber);
                    }

                    currentNumber = "";
                    isPartNumber = false;
                }
            }

            if (isPartNumber)
            {
                result += int.Parse(currentNumber);
            }
        }

        Console.WriteLine(result);
    }

    private static bool IsPartNumber(string[][] schematic, int y1, int x1)
    {
        var adjacentCells = new[]
        {
            (y1 - 1, x1 - 1), (y1 - 1, x1), (y1 - 1, x1 + 1),
            (y1, x1 - 1), (y1, x1 + 1),
            (y1 + 1, x1 - 1), (y1 + 1, x1), (y1 + 1, x1 + 1)
        };

        foreach (var (y2, x2) in adjacentCells)
        {
            if (y2 < 0 || y2 >= schematic.Length)
            {
                continue;
            }

            if (x2 < 0 || x2 >= schematic[y2].Length)
            {
                continue;
            }

            var symbol = schematic[y2][x2];
            if (symbol == "." || int.TryParse(symbol, out var _))
            {
                continue;
            }

            return true;
        }

        return false;
    }
}
