using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Day4;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var result = 0;
        foreach (var line in inputLines)
        {
            var ticketline = Regex.Replace(line, @"Card\s+\d+:", "");
            ticketline = Regex.Replace(ticketline, @"\s+", " ");

            var scratchCard = ticketline.Split('|')
                .ToArray();

            var winningNumbers = scratchCard[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var myNumbers = scratchCard[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var currentResult = 0;
            foreach (var number in myNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    if (currentResult == 0)
                    {
                        currentResult = 1;
                    }
                    else
                    {
                        currentResult *= 2;
                    }
                }
            }
            result += currentResult;
        }

        return result;
    }

    public override long SolvePartTwo()
    {
        var currentScratchCard = 1;
        var inputLines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var numberOfScratchCards = inputLines.Length;
        var scratchCardsAmount = new Dictionary<int, int>();

        for (var i = 1; i <= numberOfScratchCards; i++)
        {
            scratchCardsAmount.Add(i, 1);
        }

        foreach (var line in inputLines)
        {
            var ticketline = Regex.Replace(line, @"Card\s+\d+:", "");
            ticketline = Regex.Replace(ticketline, @"\s+", " ");

            var scratchCard = ticketline.Split('|')
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
                i <= currentScratchCard + cardResult && i <= numberOfScratchCards;
                i++)
            {
                scratchCardsAmount[i] += scratchCardsAmount[currentScratchCard];
            }

            currentScratchCard++;
        }

        return scratchCardsAmount.Values.Sum();
    }
}
