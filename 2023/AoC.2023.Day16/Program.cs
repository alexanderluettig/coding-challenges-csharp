using System.Text;

namespace AoC._2023.Day16;

internal class Program
{
    private const int FPS = 30;
    private const int FrameTime = 1000 / FPS;
    private static async Task Main(string[] args)
    {
        await using var stream = typeof(Program).Assembly
        .GetManifestResourceStream(typeof(Program), "test.txt");
        using var reader = new StreamReader(stream!, Encoding.UTF8, leaveOpen: true);

        string[][] map = [];
        for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
        {
            map = [.. map, line.ToCharArray().Select(c => c.ToString()).ToArray()];
        }

        int[][] lights = new int[map.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            lights[i] = new int[map[i].Length];
        }

        TraverseMap(map, lights, (0, 0), (1, 0));

    }

    private static void TraverseMap(string[][] map, int[][] lights, (int x, int y) position, (int x, int y) direction)
    {
        if (position.x < 0 || position.x >= map.Length || position.y < 0 || position.y >= map[0].Length || lights[position.x][position.y] == 1)
        {
            return;
        }

        lights[position.x][position.y] = 1;
        // Console.Clear();
        // lights.ToList().ForEach(l => Console.WriteLine(string.Join("", l.Select(i => i == 1 ? "#" : "."))));
        // Thread.Sleep(FrameTime);

        (int nextX, int nextY) = (position.x + direction.x, position.y + direction.y);

        if (map[position.y][position.x] == "." || map[position.y][position.x] == "-" && direction.x != 0 || map[position.y][position.x] == "|" && direction.y != 0)
        {
            TraverseMap(map, lights, (nextX, nextY), direction);
        }
        else if (map[position.y][position.x] == "/" && direction.x == 1)
        {
            TraverseMap(map, lights, (nextX, nextY), (0, -1));
        }
        else if (map[position.y][position.x] == "/" && direction.x == -1)
        {
            TraverseMap(map, lights, (nextX, nextY), (0, 1));
        }
        else if (map[position.y][position.x] == "/" && direction.y == 1)
        {
            TraverseMap(map, lights, (nextX, nextY), (-1, 0));
        }
        else if (map[position.y][position.x] == "/" && direction.y == -1)
        {
            TraverseMap(map, lights, (nextX, nextY), (1, 0));
        }
        else if (map[position.y][position.x] == "\\" && direction.x == 1)
        {
            TraverseMap(map, lights, (nextX, nextY), (0, 1));
        }
        else if (map[position.y][position.x] == "\\" && direction.x == -1)
        {
            TraverseMap(map, lights, (nextX, nextY), (0, -1));
        }
        else if (map[position.y][position.x] == "\\" && direction.y == 1)
        {
            TraverseMap(map, lights, (nextX, nextY), (1, 0));
        }
        else if (map[position.y][position.x] == "\\" && direction.y == -1)
        {
            TraverseMap(map, lights, (nextX, nextY), (-1, 0));
        }
        else if (map[position.y][position.x] == "|" && direction.x != 0)
        {
            TraverseMap(map, lights, (nextX, nextY), (0, -1));
            TraverseMap(map, lights, (nextX, nextY), (0, 1));
        }
        else if (map[position.y][position.x] == "-" && direction.y != 0)
        {
            TraverseMap(map, lights, (nextX, nextY), (1, 0));
            TraverseMap(map, lights, (nextX, nextY), (-1, 0));
        }

        return;
    }
}