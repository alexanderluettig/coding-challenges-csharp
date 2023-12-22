using System.Text;

namespace AdventOfCode.Y2023.Day5;

// Solution by https://github.com/dmitry-shechtman/aoc2023/blob/main/aoc2023/day05/README.m
internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var ss = _input.Split("\n\n");
        var (seeds, maps) = Parse(ss);
        return Part1(seeds, maps);
    }

    public override long SolvePartTwo()
    {
        var ss = _input.Split("\n\n");
        var (seeds, maps) = Parse(ss);
        return Part2(seeds, maps);
    }

    record Entry(LongRange Source, LongRange Dest)
    {
        public static Entry Parse(string s) =>
            new(ParseInt64s(s));

        private Entry(long[] v) : this(
            Source: LongRange.FromMinLength(v[1], v[2]),
            Dest: LongRange.FromMinLength(v[0], v[2]))
        {
        }

        public long Transform(long value) =>
            value + Dest.Min - Source.Min;

        public LongRange Transform(LongRange range) =>
            Dest.Intersect((Transform(range.Min), Transform(range.Max)));
    }

    record Map(Entry[] Entries)
    {
        public static Map Parse(string s) =>
            new(s.Trim().Split('\n')[1..].Select(Entry.Parse).ToArray());

        public IEnumerable<long> Transform(long value) =>
            Entries.Where(m => m.Source.IsMatch(value)).Select(m => m.Transform(value));

        public IEnumerable<LongRange> Transform(LongRange range) =>
            Entries.Where(m => m.Source.IsMatch(range)).Select(m => m.Transform(range));
    }

    private static long Part1(long[] seeds, Map[] maps) =>
        seeds.Min(seed => Min(seed, maps));

    private static long Part2(long[] seeds, Map[] maps) =>
        maps.Aggregate(
                seeds.Chunk(2).Select(LongRange.FromMinLength), Transform)
            .Min(range => range.Min);

    private static long Min(long seed, Map[] maps) =>
        maps.Aggregate(seed, Min);

    private static long Min(long value, Map map) =>
        map.Transform(value).Any() ? map.Transform(value).Min() : value;

    private static IEnumerable<LongRange> Transform(IEnumerable<LongRange> ranges, Map map) =>
        ranges.SelectMany(range => Transform(range, map));

    private static IEnumerable<LongRange> Transform(LongRange range, Map map) =>
        map.Transform(range).Any() ? map.Transform(range) : new[] { range };

    private static (long[], Map[]) Parse(string[] ss) =>
        (ParseInt64s(ss[0].Split(": ")[1]), ss[1..].Select(Map.Parse).ToArray());

    private static long[] ParseInt64s(string s) =>
        s.Split(' ').Select(long.Parse).ToArray();
}