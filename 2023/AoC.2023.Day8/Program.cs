using System.Text;

namespace AoC._2023.Day8;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var directions = (await reader.ReadLineAsync())!.ToCharArray().Select(c => c.ToString()).ToArray();
        await reader.ReadLineAsync();

        var map = new Dictionary<string, (string left, string right)>();
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            line = line.Replace(" ", "").Replace("(", "").Replace(")", "");
            var split = line.Split("=");
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
                if (c.EndsWith("Z"))
                {
                    distances[index] = numberOfSteps;
                }
            }
        }

        Console.WriteLine(FindLeastCommonMultiple(distances));
    }

    private static long FindLeastCommonMultiple(IEnumerable<long> numbers) =>
        numbers.Aggregate((long)1, (current, number) => current / GreatestCommonDivisor(current, number) * number);

    private static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            a %= b;
            (a, b) = (b, a);
        }
        return a;
    }
}