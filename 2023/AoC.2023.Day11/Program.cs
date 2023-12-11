using System.Text;

namespace AoC._2023.Day11;

internal class Program
{
    private const int ExpansionSize = 1000000;
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] input = [];
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            var currentLine = line.ToCharArray().Select(c => c.ToString()).ToArray();

            if (currentLine.All(c => c == "."))
            {
                input = [.. input, line.Replace(".", ExpansionSize.ToString()).ToCharArray().Select(c => c.ToString()).ToArray()];
            }
            else
            {
                input = [.. input, currentLine];
            }
        }

        int[] indexes = [];
        for (var i = 0; i < input[0].Length; i++)
        {
            var onlyDots = input.All(x => x[i] != "#");
            if (onlyDots)
            {
                indexes = [.. indexes, i];
            }
        }

        foreach (var i in indexes)
        {
            input = ReplaceColumn(input, i);
        }

        Coordinate[] coords = [];
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == "#")
                {
                    coords = [.. coords, new(i, j)];
                }
            }
        }

        var combinations = GetCoordinateCombinations(coords);
        Console.WriteLine(combinations.Select(x => CalculateManhattanDistance(x, input)).Sum());
    }

    private static string[][] ReplaceColumn(string[][] input, int v)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i][v] = ExpansionSize.ToString();
        }

        return input;
    }

    private static long CalculateManhattanDistance((Coordinate start, Coordinate end) combination, string[][] input)
    {
        var (start, end) = combination;
        var distanceWithOutExtraRowsAndColumns = Math.Abs(start.Row - end.Row) + Math.Abs(start.Column - end.Column);

        var extraRows = 0;
        for (var i = start.Row; i < end.Row; i++)
        {
            var current = input[i][start.Column];
            if (current != "." && current != "#")
            {
                extraRows++;
            }
        }

        var extraColumns = 0;
        var leftGalaxy = Math.Min(start.Column, end.Column);
        var rightGalaxy = Math.Max(start.Column, end.Column);
        for (var i = leftGalaxy; i < rightGalaxy; i++)
        {
            var current = input[start.Row][i];
            if (current != "." && current != "#")
            {
                extraColumns++;
            }
        }

        return distanceWithOutExtraRowsAndColumns + ((extraRows + extraColumns) * (ExpansionSize - 1));
    }

    private static (Coordinate start, Coordinate end)[] GetCoordinateCombinations(Coordinate[] coordinates)
    {
        (Coordinate start, Coordinate end)[] combinations = [];

        for (var i = 0; i < coordinates.Length - 1; i++)
        {
            for (var j = i + 1; j < coordinates.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                combinations = [.. combinations, (coordinates[i], coordinates[j])];
            }
        }

        return combinations;
    }

    private record Coordinate(int Row, int Column);
}