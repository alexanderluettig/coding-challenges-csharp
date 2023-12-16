using System.Text;

namespace AoC._2023.Day16;

internal class Program
{
    private const int FPS = 10;
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

        Console.WriteLine(lights.ToList().Select(l => l.Sum()).Sum());
    }

    private static void TraverseMap(string[][] map, int[][] lights, (int x, int y) position, (int x, int y) direction)
    {

        ((int x, int y), (int x, int y))[] todo = [(position, direction)];

        while (todo.Length > 0)
        {
            var (pos, dir) = todo[0];
            todo = todo[1..];

            if (pos.x < 0 || pos.x >= map.Length || pos.y < 0 || pos.y >= map[0].Length)
            {
                continue;
            }

            lights[pos.x][pos.y] = 1;
            todo = [.. todo, .. GetNext(map, lights, pos, dir)];

            Console.Clear();
            lights.ToList().ForEach(l => Console.WriteLine(string.Join("", l.Select(i => i == 1 ? "#" : "."))));
            Thread.Sleep(FrameTime);

        }
    }

    private static ((int nextX, int nextY), (int dx, int dy))[] GetNext(string[][] map, int[][] lights, (int x, int y) position, (int x, int y) direction)
    {
        if (map[position.y][position.x] == "." || map[position.y][position.x] == "-" && direction.x != 0 || map[position.y][position.x] == "|" && direction.y != 0)
        {
            var nextPos = (position.x + direction.x, position.y + direction.y);
            return [(nextPos, direction)];
        }
        else if (map[position.y][position.x] == "/" && direction.x == 1)
        {
            var nextPos = (position.x, position.y - 1);
            var nextDir = (0, -1);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "/" && direction.x == -1)
        {
            var nextPos = (position.x, position.y + 1);
            var nextDir = (0, 1);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "/" && direction.y == 1)
        {
            var nextPos = (position.x - 1, position.y);
            var nextDir = (-1, 0);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "/" && direction.y == -1)
        {
            var nextPos = (position.x + 1, position.y);
            var nextDir = (1, 0);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "\\" && direction.x == 1)
        {
            var nextPos = (position.x, position.y + 1);
            var nextDir = (0, 1);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "\\" && direction.x == -1)
        {
            var nextPos = (position.x, position.y - 1);
            var nextDir = (0, -1);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "\\" && direction.y == 1)
        {
            var nextPos = (position.x + 1, position.y);
            var nextDir = (1, 0);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "\\" && direction.y == -1)
        {
            var nextPos = (position.x - 1, position.y);
            var nextDir = (-1, 0);
            return [(nextPos, nextDir)];
        }
        else if (map[position.y][position.x] == "|" && direction.x != 0)
        {
            if (lights[position.y][position.x + 1] == 1 && lights[position.y][position.x - 1] == 1)
            {
                return [];
            }

            var nextPos1 = (position.x, position.y - 1);
            var nextDir1 = (0, -1);
            var nextPos2 = (position.x, position.y + 1);
            var nextDir2 = (0, 1);
            return [(nextPos1, nextDir1), (nextPos2, nextDir2)];
        }
        else if (map[position.y][position.x] == "-" && direction.y != 0)
        {
            if (lights[position.y + 1][position.x] == 1 && lights[position.y - 1][position.x] == 1)
            {
                return [];
            }

            TraverseMap(map, lights, (position.x + 1, position.y), (1, 0));
            TraverseMap(map, lights, (position.x - 1, position.y), (-1, 0));
            var nextPos1 = (position.x + 1, position.y);
            var nextDir1 = (1, 0);
            var nextPos2 = (position.x - 1, position.y);
            var nextDir2 = (-1, 0);
            return [(nextPos1, nextDir1), (nextPos2, nextDir2)];
        }

        return [];
    }
}