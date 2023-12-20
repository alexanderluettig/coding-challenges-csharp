using System.Text;
using AdventOfCode.Y2023.Day4;

namespace AdventOfCode;
internal class Program
{
    public static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
            .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var input = await reader.ReadToEndAsync();

        if (string.IsNullOrEmpty(input))
            throw new Exception("Input is empty");

        var solution = new Solution(input);
        Console.WriteLine($"Part1: {solution.SolvePartOne()}");
        Console.WriteLine($"Part2: {solution.SolvePartTwo()}");
    }
}