namespace AdventOfCode.Y2023.Day9;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var result = 0L;
        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(" ").Select(long.Parse).ToArray();
            var list = CalculateNextRows(numbers);

            var temp = 0L;
            foreach (var row in list)
            {
                temp += row.Last();
            }

            result += temp;
        }

        return result;
    }

    public override long SolvePartTwo()
    {
        var result = 0L;
        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(" ").Select(long.Parse).ToArray();
            var list = CalculateNextRows(numbers);

            var temp = 0L;

            foreach (var row in list.Reverse())
            {
                temp = row.First() - temp;
            }

            result += temp;
        }

        return result;
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