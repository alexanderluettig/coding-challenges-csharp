using System.Text;

namespace AoC._2023.Day10;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "input.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] map = [];

        var row = 0;
        (int col, int row) start = (-1, -1);
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            map = [.. map, line.Select(c => c.ToString()).ToArray()];

            if (line.Contains('S'))
            {
                var col = line.IndexOf('S');
                start = (col, row);
            }

            row++;
        }

        var steps = 0;
        (int col, int row) previousPosition = (-1, -1);
        for (var currentPosition = start; currentPosition != start || steps == 0; steps++)
        {
            var tempPos = GetNextPosition(map, currentPosition, previousPosition);
            previousPosition = currentPosition;
            currentPosition = tempPos;
        }

        Console.WriteLine(steps / 2);
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
    private static (int col, int row) GetNextPosition(string[][] map, (int col, int row) currentPosition, (int col, int row) previousPosition)
    {
        var (col, row) = currentPosition;
        var direction = GetDirection(currentPosition, previousPosition);

        switch (map[row][col])
        {
            case "S":
                if (col > 0 && map[row][col - 1] is "-" or "L" or "F")
                    return (col - 1, row);
                if (col < map[row].Length && map[row][col + 1] is "-" or "J" or "7")
                    return (col + 1, row);
                if (row > 0 && map[row - 1][col] is "|" or "7" or "F")
                    return (col, row - 1);
                if (row < map.Length && map[row + 1][col] is "|" or "L" or "J")
                    return (col, row + 1);
                throw new Exception("No valid next position");
            case "|":
                if (direction == Direction.Up)
                    return (col, row - 1);
                if (direction == Direction.Down)
                    return (col, row + 1);
                throw new Exception("No valid next position");
            case "-":
                if (direction == Direction.Left)
                    return (col - 1, row);
                if (direction == Direction.Right)
                    return (col + 1, row);
                throw new Exception("No valid next position");
            case "L":
                if (direction == Direction.Down)
                    return (col + 1, row);
                if (direction == Direction.Left)
                    return (col, row - 1);
                throw new Exception("No valid next position");
            case "J":
                if (direction == Direction.Down)
                    return (col - 1, row);
                if (direction == Direction.Right)
                    return (col, row - 1);
                throw new Exception("No valid next position");
            case "7":
                if (direction == Direction.Up)
                    return (col - 1, row);
                if (direction == Direction.Right)
                    return (col, row + 1);
                throw new Exception("No valid next position");
            case "F":
                if (direction == Direction.Up)
                    return (col + 1, row);
                if (direction == Direction.Left)
                    return (col, row + 1);
                throw new Exception("No valid next position");
            default:
                throw new ArgumentOutOfRangeException("Unknown Character", nameof(map));
        }
    }

    private static Direction GetDirection((int col, int row) currentPosition, (int col, int row) previousPosition)
    {
        var (col, row) = currentPosition;
        var (previousCol, previousRow) = previousPosition;

        if (col == previousCol)
        {
            return row > previousRow ? Direction.Down : Direction.Up;
        }

        return col > previousCol ? Direction.Right : Direction.Left;
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}