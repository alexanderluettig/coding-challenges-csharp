using System.Text;
using System.Text.RegularExpressions;

namespace AoC._2023.Day4;

internal class Program
{
    private const int NUMBEROFSCRATCHCARDS = 212;
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var currentScratchCard = 1;
        var scratchCardsAmount = new Dictionary<int, int>();

        for (var i = 1; i <= NUMBEROFSCRATCHCARDS; i++)
        {
            scratchCardsAmount.Add(i, 1);
        }

        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            line = Regex.Replace(line, @"Card\s+\d+:", "");
            line = Regex.Replace(line, @"\s+", " ");

            var scratchCard = line.Split('|')
                .ToArray();

            var winningNumbers = scratchCard[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToDictionary(x => x, _ => true);

            var myNumbers = scratchCard[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var cardResult = 0;

            foreach (var number in myNumbers)
            {
                if (winningNumbers.ContainsKey(number))
                {
                    cardResult += 1;
                }
            }

            for (var i = currentScratchCard + 1;
                i <= currentScratchCard + cardResult && i <= NUMBEROFSCRATCHCARDS;
                i++)
            {
                scratchCardsAmount[i] += scratchCardsAmount[currentScratchCard];
            }

            currentScratchCard++;
        }

        Console.WriteLine(scratchCardsAmount.Values.Sum());
    }
}
