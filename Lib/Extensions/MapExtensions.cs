using Lib.Datastructures;

namespace Lib.Extensions;

public static class MapExtensions
{
    public static int Count<T>(this Map<T> map, Func<T, bool> predicate) where T : notnull
    {
        var result = 0;
        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (predicate(map[y, x]))
                {
                    result++;
                }
            }
        }

        return result;
    }

    public static long Sum(this Map<int> map)
    {
        var result = 0L;
        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                result += map[y, x];
            }
        }

        return result;
    }
}
