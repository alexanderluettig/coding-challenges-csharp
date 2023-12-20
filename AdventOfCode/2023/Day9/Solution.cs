using System.Text;

namespace AdventOfCode._2023.Day9;

internal class Program
{
    private static async Task Method(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var result = 0L;
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            var numbers = line.Split(" ").Select(long.Parse).ToArray();
            var list = CalculateNextRows(numbers);

            var temp = 0L;

            //Part 1
            // foreach (var row in list)
            // {
            //     temp += row.Last();
            // }

            //Part 2
            foreach (var row in list.Reverse())
            {
                temp = row.First() - temp;
            }

            result += temp;
        }

        Console.WriteLine(result);
    }

    private static long[][] CalculateNextRows(long[] numbers)
    {
        var list = new List<long[]>
            {
                numbers
            };

        var currentNumbers = numbers;
        long[] nextNumbers;
        do
        {
            nextNumbers = new long[currentNumbers.Length - 1];
            for (var i = 0; i < currentNumbers.Length - 1; i++)
            {
                nextNumbers[i] = currentNumbers[i + 1] - currentNumbers[i];
            }

            list.Add(nextNumbers);

            currentNumbers = nextNumbers;
        } while (nextNumbers.Any(x => x != 0) && nextNumbers.Length > 1);

        return [.. list];
    }
}