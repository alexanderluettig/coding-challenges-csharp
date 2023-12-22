namespace AdventOfCode.Y2023.Day11;

internal class Solution(string input) : ISolver(input)
{

    public override long SolvePartOne()
    {
        return Calculate(2);
    }

    public override long SolvePartTwo()
    {
        return Calculate(1000000);
    }
    private long Calculate(int expansionSize)
    {
        string[][] map = [];
        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var currentLine = line.ToCharArray().Select(c => c.ToString()).ToArray();

            if (currentLine.All(c => c == "."))
            {
                map = [.. map, line.Replace(".", expansionSize.ToString()).ToCharArray().Select(c => c.ToString()).ToArray()];
            }
            else
            {
                map = [.. map, currentLine];
            }
        }

        int[] indexes = [];
        for (var i = 0; i < map[0].Length; i++)
        {
            var onlyDots = map.All(x => x[i] != "#");
            if (onlyDots)
            {
                indexes = [.. indexes, i];
            }
        }

        foreach (var i in indexes)
        {
            map = ReplaceColumn(map, i, expansionSize);
        }

        Coordinate[] coords = [];
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == "#")
                {
                    coords = [.. coords, new(i, j)];
                }
            }
        }

        var combinations = Combinations.GetAllCombinations(coords, 2);
        return combinations.Select(x => CalculateManhattanDistance((x[0], x[1]), map, expansionSize)).Sum();
    }

    private static string[][] ReplaceColumn(string[][] input, int v, int expansionSize)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i][v] = expansionSize.ToString();
        }

        return input;
    }

    private static long CalculateManhattanDistance((Coordinate start, Coordinate end) combination, string[][] input, int expansionSize)
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

        return distanceWithOutExtraRowsAndColumns + ((extraRows + extraColumns) * (expansionSize - 1));
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