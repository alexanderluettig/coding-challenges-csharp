using Lib;

namespace AdventOfCode.Y2015.Day3;

internal sealed class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var visited = new HashSet<Vector2d>();
        var current = new Vector2d(0, 0);

        visited.Add(current);

        for (int i = 0; i < _input.Length; i++)
        {
            switch (_input[i])
            {
                case '^':
                    current.Y++;
                    break;
                case 'v':
                    current.Y--;
                    break;
                case '>':
                    current.X++;
                    break;
                case '<':
                    current.X--;
                    break;
            }

            visited.Add(current);
        }

        return visited.Count;
    }

    public override long SolvePartTwo()
    {
        var visited = new HashSet<Vector2d>();
        var santa = new Vector2d(0, 0);
        var roboSanta = new Vector2d(0, 0);

        visited.Add(santa.Copy());

        for (int i = 0; i < _input.Length; i++)
        {
            switch (_input[i])
            {
                case '^':
                    if (i % 2 == 0)
                        santa.Y++;
                    else
                        roboSanta.Y++;
                    break;
                case 'v':
                    if (i % 2 == 0)
                        santa.Y--;
                    else
                        roboSanta.Y--;
                    break;
                case '>':
                    if (i % 2 == 0)
                        santa.X++;
                    else
                        roboSanta.X++;
                    break;
                case '<':
                    if (i % 2 == 0)
                        santa.X--;
                    else
                        roboSanta.X--;
                    break;
            }

            visited.Add(santa.Copy());
            visited.Add(roboSanta.Copy());
        }

        return visited.Count;
    }
}