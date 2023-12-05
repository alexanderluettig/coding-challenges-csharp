using System.Text;

namespace AoC._2023.Day5;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        var seedRanges = (await reader.ReadLineAsync())![6..].Trim().Split(" ").Select(long.Parse).ToArray();
        await reader.ReadLineAsync(); // Empty Line

        var ranges = new List<SeedRange>();
        for (var i = 0; i < seedRanges.Length; i += 2)
        {
            var start = seedRanges[i];
            var stepSize = seedRanges[i + 1];

            ranges.Add(new SeedRange(start, start + stepSize - 1));
        }

        var rangeMapper = new RangeMapper();
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            if (line.Contains("map"))
            {
                continue;
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                var newRanges = new List<SeedRange>();

                foreach (var range in ranges)
                {
                    foreach (var mapping in rangeMapper.Ranges)
                    {
                        if (range.From < mapping.From)
                        {
                            newRanges.Add(new SeedRange(range.From, Math.Min(range.To, mapping.From - 1)));
                            range.From = mapping.From;
                            if (range.From > range.To)
                            {
                                break;
                            }
                        }

                        if (range.From <= mapping.To)
                        {
                            newRanges.Add(new SeedRange(range.From + mapping.StepSize, Math.Min(range.To, mapping.To) + mapping.StepSize));
                            range.From = mapping.To + 1;
                            if (range.From > range.To)
                            {
                                break;
                            }
                        }
                    }
                    if (range.From <= range.To)
                    {
                        newRanges.Add(range);
                    }
                }

                ranges = newRanges;
                rangeMapper.Clear();
            }
            else
            {
                var parts = line.Split(" ");
                var end = long.Parse(parts[0]);
                var start = long.Parse(parts[1]);
                var stepSize = long.Parse(parts[2]);
                rangeMapper.AddRange(new Range(end, start, stepSize));
            }
        }

        Console.WriteLine(ranges.Min(x => x.From));
    }
}

class RangeMapper
{
    public List<Range> Ranges { get; internal set; } = new List<Range>();

    public RangeMapper()
    {

    }

    public void AddRange(Range range)
    {
        Ranges.Add(range);
        Ranges = [.. Ranges.OrderBy(x => x.From)];
    }

    public void Clear()
    {
        Ranges.Clear();
    }

    public long Parse(long input)
    {
        foreach (var range in Ranges)
        {
            if (range.CanParse(input))
            {
                return range.Parse(input);
            }
        }

        return input;
    }
}

class SeedRange
{
    public long From;
    public long To;

    public SeedRange(long start, long to)
    {
        From = start;
        To = to;
    }
}

class Range
{
    public long From;
    public long To;
    public long StepSize;

    public Range(long end, long start, long stepSize)
    {
        From = start;
        To = end;
        StepSize = stepSize;
    }

    public long Parse(long input) => Math.Abs(input - From) + To;

    public bool CanParse(long input) => input >= From && input < From + StepSize;
}