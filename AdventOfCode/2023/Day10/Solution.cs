using Lib;

namespace AdventOfCode.Y2023.Day10;

internal class Solution(string input) : ISolver(input)
{
    public override long SolvePartOne()
    {
        var map = new Map<string>("#", _input.Split(Environment.NewLine).Select(x => x.ToCharArray().Select(y => y.ToString())));
        var result = 0;

        var current = map.Find("S").First();
        var direction = Direction.Up;
        do
        {
            (direction, current) = GetNextDirection(current, direction, map);
            result++;
        } while (current.Value != "S");

        return result / 2;
    }

    public override long SolvePartTwo()
    {
        var map = new Map<string>("#", _input.Split(Environment.NewLine).Select(x => x.ToCharArray().Select(y => y.ToString())));
        var walls = new Map<string>(".", map.Height, map.Width);

        var current = map.Find("S").First();
        var direction = Direction.Up;
        do
        {
            walls[current] = map[current];
            (direction, current) = GetNextDirection(current, direction, map);
        } while (current.Value != "S");

        Console.WriteLine(walls);

        return -1;
    }

    private static (Vector2d direction, Point2D<string> next) GetNextDirection(Vector2d current, Vector2d prevDirection, Map<string> map)
    {
        if (map[current] == "S")
        {
            if (map.TryGet(current + Direction.Up, out var up) && (up.Value is "|" or "7" or "F"))
                return (Direction.Up, up);

            if (map.TryGet(current + Direction.Down, out var down) && (down.Value is "|" or "L" or "J"))
                return (Direction.Down, down);

            if (map.TryGet(current + Direction.Left, out var left) && (left.Value is "-" or "L" or "F"))
                return (Direction.Left, left);

            if (map.TryGet(current + Direction.Right, out var right) && (right.Value is "-" or "J" or "7"))
                return (Direction.Right, right);
        }
        else if (map[current] == "|")
        {
            if (prevDirection == Direction.Up)
            {
                return (Direction.Up, map.TryGet(current + Direction.Up, out var up) ? up : throw new Exception("No Up"));
            }
            else if (prevDirection == Direction.Down)
            {
                return (Direction.Down, map.TryGet(current + Direction.Down, out var down) ? down : throw new Exception("No Down"));
            }
        }
        else if (map[current] == "-")
        {
            if (prevDirection == Direction.Left)
            {
                return (Direction.Left, map.TryGet(current + Direction.Left, out var left) ? left : throw new Exception("No Left"));
            }
            else if (prevDirection == Direction.Right)
            {
                return (Direction.Right, map.TryGet(current + Direction.Right, out var right) ? right : throw new Exception("No Right"));
            }
        }
        else if (map[current] == "L")
        {
            if (prevDirection == Direction.Down)
            {
                return (Direction.Right, map.TryGet(current + Direction.Right, out var right) ? right : throw new Exception("No Right"));
            }
            else if (prevDirection == Direction.Left)
            {
                return (Direction.Up, map.TryGet(current + Direction.Up, out var up) ? up : throw new Exception("No Up"));
            }
        }
        else if (map[current] == "J")
        {
            if (prevDirection == Direction.Down)
            {
                return (Direction.Left, map.TryGet(current + Direction.Left, out var left) ? left : throw new Exception("No Left"));
            }
            else if (prevDirection == Direction.Right)
            {
                return (Direction.Up, map.TryGet(current + Direction.Up, out var up) ? up : throw new Exception("No Up"));
            }
        }
        else if (map[current] == "7")
        {
            if (prevDirection == Direction.Up)
            {
                return (Direction.Left, map.TryGet(current + Direction.Left, out var left) ? left : throw new Exception("No Left"));
            }
            else if (prevDirection == Direction.Right)
            {
                return (Direction.Down, map.TryGet(current + Direction.Down, out var down) ? down : throw new Exception("No Down"));
            }
        }
        else if (map[current] == "F")
        {
            if (prevDirection == Direction.Up)
            {
                return (Direction.Right, map.TryGet(current + Direction.Right, out var right) ? right : throw new Exception("No Right"));
            }
            else if (prevDirection == Direction.Left)
            {
                return (Direction.Down, map.TryGet(current + Direction.Down, out var down) ? down : throw new Exception("No Down"));
            }
        }

        throw new Exception("No Direction");
    }


    /*
    | is a vertical pipe connecting north and south. Up and Down
    - is a horizontal pipe connecting east and west. Left and Right
    L is a 90-degree bend connecting north and east. Up and Right
    J is a 90-degree bend connecting north and west. Up and Left
    7 is a 90-degree bend connecting south and west. Down and Left
    F is a 90-degree bend connecting south and east. Down and Right
    . is ground; there is no pipe in this tile.
    S is the starting position. Next Position can only be connected pipes based on direction
    S Left: -, L, F
    S Right: -, J, 7
    S Up: |, 7, F
    S Down: |, L, J
    For S direction is not relevant
    */
}