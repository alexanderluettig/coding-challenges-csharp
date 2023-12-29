namespace AdventOfCode.Y2015.Day6;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var map = new Map<int>(0, 1000, 1000);

        foreach (var line in _input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = parts[^4] switch
            {
                "on" => 1,
                "off" => 0,
                "toggle" => 2,
                _ => throw new Exception("Unknown command")
            };

            var start = parts[^3].Split(',');
            var end = parts[^1].Split(',');

            var startX = int.Parse(start[0]);
            var startY = int.Parse(start[1]);
            var endX = int.Parse(end[0]);
            var endY = int.Parse(end[1]);

            for (var y = startY; y <= endY; y++)
            {
                for (var x = startX; x <= endX; x++)
                {
                    map[y, x] = command switch
                    {
                        0 => 0,
                        1 => 1,
                        2 => map[y, x] == 0 ? 1 : 0,
                        _ => throw new Exception("Unknown command")
                    };
                }
            }
        }

        return map.Count(c => c == 1);
    }

    public override long SolvePartTwo()
    {
        var map = new Map<int>(0, 1000, 1000);

        foreach (var line in _input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = parts[^4] switch
            {
                "on" => 1,
                "off" => 0,
                "toggle" => 2,
                _ => throw new Exception("Unknown command")
            };

            var start = parts[^3].Split(',');
            var end = parts[^1].Split(',');

            var startX = int.Parse(start[0]);
            var startY = int.Parse(start[1]);
            var endX = int.Parse(end[0]);
            var endY = int.Parse(end[1]);

            for (var y = startY; y <= endY; y++)
            {
                for (var x = startX; x <= endX; x++)
                {
                    var val = map[y, x];
                    map[y, x] = command switch
                    {
                        0 => val > 0 ? val - 1 : 0,
                        1 => val + 1,
                        2 => val + 2,
                        _ => throw new Exception("Unknown command")
                    };
                }
            }
        }

        return map.Sum();
    }
}