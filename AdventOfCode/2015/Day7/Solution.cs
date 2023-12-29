namespace AdventOfCode.Y2015.Day7;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        IDictionary<string, ushort> mem = new SortedDictionary<string, ushort>();
        mem = Wire(mem, _input);

        return mem.TryGetValue("a", out var result) ? result : -1;
    }

    public override long SolvePartTwo()
    {
        IDictionary<string, ushort> mem = new SortedDictionary<string, ushort>();
        // Solution is basically the same as Part 1, 
        // but we need to override the value of b when assigned in input (line 90)
        // Solution of Part 1 is 956
        var input = _input.Replace("14146 -> b", "956 -> b");
        mem = Wire(mem, input);

        return mem.TryGetValue("a", out var result) ? result : -1;
    }

    private static IDictionary<string, ushort> Wire(IDictionary<string, ushort> mem, string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var queue = new Queue<string>(lines);

        while (queue.Count > 0)
        {
            var line = queue.Dequeue();
            var parts = line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var instruction = parts[0];
            var dest = parts[1];

            var tokens = instruction.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 1)
            {
                if (ushort.TryParse(tokens[0], out var value))
                {
                    mem[dest] = value;
                    continue;
                }

                if (!mem.ContainsKey(tokens[0]))
                {
                    queue.Enqueue(line);
                    continue;
                }

                mem[dest] = mem[tokens[0]];
            }
            else if (tokens.Length == 2)
            {
                var src = tokens[1];
                var op = tokens[0];

                if (!mem.ContainsKey(src))
                {
                    queue.Enqueue(line);
                    continue;
                }

                mem[dest] = op switch
                {
                    "NOT" => (ushort)~mem[src],
                    _ => throw new Exception($"Unknown op: {op}")
                };
            }
            else if (tokens.Length == 3)
            {
                var src1 = tokens[0];
                var src2 = tokens[2];
                var op = tokens[1];

                var src1Value = ushort.TryParse(src1, out var src1ParsingResult) ? src1ParsingResult : mem.TryGetValue(src1, out var src1MemValue) ? src1MemValue : -1;
                var src2Value = ushort.TryParse(src2, out var src2ParsingResult) ? src2ParsingResult : mem.TryGetValue(src2, out var src2MemValue) ? src2MemValue : -1;

                if (src1Value == -1 || src2Value == -1)
                {
                    queue.Enqueue(line);
                    continue;
                }

                mem[dest] = op switch
                {
                    "AND" => (ushort)(src1Value & src2Value),
                    "OR" => (ushort)(src1Value | src2Value),
                    "LSHIFT" => (ushort)(src1Value << src2Value),
                    "RSHIFT" => (ushort)(src1Value >> src2Value),
                    _ => throw new Exception($"Unknown op: {op}")
                };

            }
        }

        return mem;
    }
}