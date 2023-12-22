namespace AdventOfCode.Y2023.Day8;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var directions = inputLines[0].ToCharArray().Select(c => c.ToString()).ToArray();

        var map = new Dictionary<string, (string left, string right)>();
        foreach (var line in inputLines[1..])
        {
            var mapping = line.Replace(" ", "").Replace("(", "").Replace(")", "");
            var split = mapping.Split("=");
            var next = split[1].Split(",");

            map.Add(split[0], (next[0], next[1]));
        }

        var current = "AAA";
        var steps = 0;
        while (current != "ZZZ")
        {
            current = directions[steps % directions.Length] switch
            {
                "L" => map[current].left,
                "R" => map[current].right,
                _ => throw new Exception()
            };
            steps++;
        }

        return steps;
    }

    public override long SolvePartTwo()
    {
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var directions = inputLines[0].ToCharArray().Select(c => c.ToString()).ToArray();

        var map = new Dictionary<string, (string left, string right)>();
        foreach (var line in inputLines[1..])
        {
            var mapping = line.Replace(" ", "").Replace("(", "").Replace(")", "");
            var split = mapping.Split("=");
            var next = split[1].Split(",");

            map.Add(split[0], (next[0], next[1]));
        }

        var numberOfSteps = 0;
        var current = map.Keys.Where(k => k.EndsWith('A')).ToArray();
        var currentDirection = 0;

        var distances = new long[current.Length];

        while (distances.Any(x => x == 0))
        {
            current = current.Select(x => directions[currentDirection] switch
            {
                "L" => map[x].left,
                "R" => map[x].right,
                _ => throw new Exception()
            }).ToArray();

            numberOfSteps++;
            currentDirection = (currentDirection + 1) % directions.Length;

            foreach (var c in current)
            {
                var index = Array.IndexOf(current, c);
                if (c.EndsWith('Z'))
                {
                    distances[index] = numberOfSteps;
                }
            }
        }

        return MathAlg.FindLeastCommonMultiple(distances);
    }
}