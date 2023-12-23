using Lib.Datastructures;
using Lib.Values;

namespace Lib.Algorithms;

public static class SpaceExploration
{
    private static readonly Vector2d[] Directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];
    public static Map<T> FloodFromOutSide<T>(this Map<T> map, T wallValue, T fillValue) where T : notnull
    {
        var result = map.Copy();
        var visited = new Dictionary<Vector2d, bool>();
        var queue = new Queue<Vector2d>();

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (map[y, x].Equals(wallValue))
                {
                    continue;
                }

                if (x == 0 || x == map.Width - 1 || y == 0 || y == map.Height - 1)
                {
                    queue.Enqueue(new Vector2d(x, y));
                }
            }
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result[current] = fillValue;

            foreach (var direction in Directions)
            {
                var next = current + direction;
                if (map.TryGet(next, out var point) && !point.Value.Equals(wallValue) && !visited.ContainsKey(next) && !queue.Contains(next))
                {
                    queue.Enqueue(next);
                }
            }

            visited[current] = true;
        }


        return result;
    }
}
