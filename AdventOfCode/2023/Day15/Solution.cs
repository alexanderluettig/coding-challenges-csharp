using System.Collections.Specialized;
using System.Text;

namespace AdventOfCode.Y2023.Day15;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var input = _input.Replace("\n", "").Split(",");
        var result = 0;
        foreach (var entry in input)
        {
            result += Hash(entry);
        }

        return result;
    }

    public override long SolvePartTwo()
    {
        var input = _input.Replace("\n", "").Split(",");

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

        return result;
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