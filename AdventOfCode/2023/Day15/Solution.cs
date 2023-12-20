using System.Collections.Specialized;
using System.Text;

namespace AdventOfCode._2023.Day15;

internal class Solution
{
    private static async Task Method(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var input = (await reader.ReadToEndAsync()).Replace("\n", "").Split(",");

        var boxes = new OrderedDictionary[256];
        foreach (var entry in input)
        {
            var parts = entry.Split(["=", "-"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var label = parts[0];
            var hash = Hash(parts[0]);
            boxes[hash] ??= [];
            if (parts.Length > 1)
            {
                if (boxes[hash].Contains(label))
                {
                    boxes[hash][label] = int.Parse(parts[1]);
                }
                else
                {
                    boxes[hash].Add(label, int.Parse(parts[1]));
                }
            }
            else
            {
                boxes[hash].Remove(label);
            }
        }



        var result = 0;
        for (var i = 0; i < 256; i++)
        {
            if (boxes[i] != null)
            {
                var slot = 1;
                foreach (var value in boxes[i].Values)
                {
                    result += (i + 1) * (int)value * slot;
                    slot++;
                }
            }
        }

        Console.WriteLine(result);
    }

    static int Hash(string input)
    {
        var hash = 0;
        foreach (var c in input)
        {
            hash += c;
            hash *= 17;
            hash %= 256;
        }
        return hash;
    }
}