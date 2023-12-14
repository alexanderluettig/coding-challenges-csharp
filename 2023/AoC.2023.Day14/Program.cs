using System.Text;
using Lib.Extensions;

namespace AoC._2023.Day14;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] map = [];
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            map = [.. map, line.Select(c => c.ToString()).ToArray()];
        }

        var history = new List<string>();
        for (var i = 1000000000 - 1; i > 0; i--)
        {
            map = Cycle(map);
            var mapString = string.Join("\n", map.Select(row => string.Join("", row)));
            var index = history.IndexOf(mapString);
            if (index < 0)
            {
                history.Add(mapString);
            }
            else
            {
                var cycleLength = history.Count - index;
                var cycleIndex = i % cycleLength;
                map = history[index + cycleIndex].Split("\n").Select(row => row.ToCharArray().Select(c => c.ToString()).ToArray()).ToArray();
                break;
            }
        }

        var result = map.Select((row, y) =>
        {
            var numberOfRelevantRocks = row.Count(c => c == "O");
            return numberOfRelevantRocks * (map.Length - y);
        }).Sum();
        Console.WriteLine(result);
    }

    public static string[][] Cycle(string[][] map)
    {
        map = RollInDirection(map, Direction.North);
        map = RollInDirection(map, Direction.West);
        map = RollInDirection(map, Direction.South);
        map = RollInDirection(map, Direction.East);

        return map;
    }

    private static string[][] RollInDirection(string[][] map, Direction dir)
    {
        map = map.Transpose();

        map = map.Select(row =>
        {
            var currentRow = string.Join("", row)
                .Split("#")
                .Select(s =>
                {
                    var charArray = s.ToCharArray();
                    Array.Sort(charArray);
                    if (dir == Direction.North || dir == Direction.West)
                    {
                        charArray = charArray.Reverse().ToArray();
                    }
                    return new string(charArray);
                }).ToArray();

            return string.Join("#", currentRow).ToCharArray().Select(c => c.ToString()).ToArray();
        }).ToArray();

        return map;
    }

    private static int Comparer(string a, string b)
    {
        if (a == "#" || b == "#")
        {
            return 0;
        }

        return b.CompareTo(a);
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }
}