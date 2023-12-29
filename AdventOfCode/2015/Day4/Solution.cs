using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015.Day4;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        return ParallelFind(_input, "00000");
    }

    public override long SolvePartTwo()
    {
        return ParallelFind(_input, "000000");
    }

    private static int ParallelFind(string input, string prefix)
    {
        var queue = new ConcurrentQueue<int>();

        Parallel.ForEach(
            Numbers(),
            (i, state) =>
            {
                var hash = string.Join("", MD5.HashData(Encoding.UTF8.GetBytes($"{input}{i}")).Select(b => b.ToString("x2")));

                if (hash.StartsWith(prefix))
                {
                    queue.Enqueue(i);
                    state.Stop();
                }
            }
        );

        return queue.Min();
    }

    private static IEnumerable<int> Numbers()
    {
        int i = 0;
        while (true)
            yield return i++;
    }
}