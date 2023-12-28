namespace AdventOfCode.Y2015.Day2;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        return _input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('x', StringSplitOptions.RemoveEmptyEntries))
            .Select(dimensions => dimensions.Select(int.Parse).ToArray())
            .Select(dimensions => new Box
            {
                Length = dimensions[0],
                Width = dimensions[1],
                Height = dimensions[2]
            })
            .Select(box => box.SurfaceArea + box.SmallestSideArea)
            .Sum();

    }

    public override long SolvePartTwo()
    {
        return _input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('x', StringSplitOptions.RemoveEmptyEntries))
            .Select(dimensions => dimensions.Select(int.Parse).ToArray())
            .Select(dimensions => new Box
            {
                Length = dimensions[0],
                Width = dimensions[1],
                Height = dimensions[2]
            })
            .Select(box => box.SmallestPerimeter + box.Volume)
            .Sum();
    }

    class Box
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int SurfaceArea => 2 * Length * Width + 2 * Width * Height + 2 * Height * Length;
        public int SmallestSideArea => Math.Min(Length * Width, Math.Min(Width * Height, Height * Length));
        public int Volume => Length * Width * Height;
        public int SmallestPerimeter => Math.Min(2 * Length + 2 * Width, Math.Min(2 * Width + 2 * Height, 2 * Height + 2 * Length));
    }
}