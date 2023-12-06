using System.Text;

namespace AoC._2023.Day06;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var time = long.Parse((await reader.ReadLineAsync())![5..].Replace(" ", ""));
        var distance = long.Parse((await reader.ReadLineAsync())![9..].Replace(" ", ""));

        var result = 1;

        double p = -1 * time;
        double q = distance + 0.001;
        var x1 = -(p / 2) + Math.Sqrt(Math.Pow(p / 2, 2) - q);
        var x2 = -(p / 2) - Math.Sqrt(Math.Pow(p / 2, 2) - q);

        var lowerBound = (int)Math.Ceiling(Math.Min(x1, x2));
        var upperBound = (int)Math.Floor(Math.Max(x1, x2));

        result *= upperBound - lowerBound + 1;

        Console.WriteLine(result);
    }
}

record Race(int Time, int Distance);