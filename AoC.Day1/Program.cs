using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Day1;
internal class Program
{
    private static readonly string[] numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
    .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        int counter = 0;

        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {

            //This is for part two
            var newLine = "";
            for (int i = 0; i < line.Length; i++)
            {
                string current = line[i].ToString();

                if (int.TryParse(current, out _))
                {
                    newLine += current;
                }
                else
                {
                    var subString = parseSubstring(line, i);

                    if (int.TryParse(subString, out _))
                    {
                        newLine += subString;
                    }
                }

            }

            line = newLine;

            //This is for part one
            //line = Regex.Replace(line, @"\D", ""); ;

            var numberString = line[0].ToString() + line[line.Length - 1].ToString();
            counter += int.Parse(numberString);
        }

        Console.WriteLine(counter);
    }

    private static string parseSubstring(string line, int index)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (index + numbers[i].Length > line.Length)
            {
                continue;
            }

            if (line.Substring(index, numbers[i].Length) == numbers[i])
            {
                return (i + 1).ToString();
            }
        }

        return "NaN";
    }
}