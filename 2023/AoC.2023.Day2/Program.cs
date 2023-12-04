using System.Text;

namespace AoC._2023.Day2;

internal class Program
{
    // These constants are only relevant for the first part of the puzzle
    // private static readonly int MAXREDS = 12;
    // private static readonly int MAXGREENS = 13;
    // private static readonly int MAXBLUES = 14;

    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
            .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var result = 0;
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            // For the first part of the puzzle replace the following line extracts the game id
            // Regex.Replace(line, @"Game (\d{1,3})", "$1")
            var game = line.Split(":");

            // var gameId = int.Parse(game[0]);
            var rounds = game[1].Split(";");

            var roundMaxReds = 0;
            var roundMaxGreens = 0;
            var roundMaxBlues = 0;
            foreach (var round in rounds)
            {
                var colors = round.Split(",");

                var reds = 0;
                var greens = 0;
                var blues = 0;

                foreach (var color in colors)
                {
                    if (color.Contains("red"))
                    {
                        reds += int.Parse(color.Replace("red", "").Trim());
                    }
                    else if (color.Contains("green"))
                    {
                        greens += int.Parse(color.Replace("green", "").Trim());
                    }
                    else if (color.Contains("blue"))
                    {
                        blues += int.Parse(color.Replace("blue", "").Trim());
                    }
                }

                roundMaxReds = Math.Max(roundMaxReds, reds);
                roundMaxGreens = Math.Max(roundMaxGreens, greens);
                roundMaxBlues = Math.Max(roundMaxBlues, blues);
            }

            result += roundMaxReds * roundMaxGreens * roundMaxBlues;
        }

        Console.WriteLine(result);
    }
}