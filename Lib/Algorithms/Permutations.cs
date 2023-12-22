namespace Lib.Algorithms;

public static class Permutations
{
    /// <summary>
    /// Heap's algorithm to find all pmermutations. Non recursive, more efficient.
    /// </summary>
    /// <param name="items">Items to permute in each possible ways</param>
    /// <param name="funcExecuteAndTellIfShouldStop"></param>
    /// <returns>Return true if cancelled</returns> 
    public static bool ForAllPermutation<T>(T[] items, Func<T[], bool> funcExecuteAndTellIfShouldStop)
    {
        int countOfItem = items.Length;

        if (countOfItem <= 1)
        {
            return funcExecuteAndTellIfShouldStop(items);
        }

        var indexes = new int[countOfItem];

        if (funcExecuteAndTellIfShouldStop(items))
        {
            return true;
        }

        for (int i = 1; i < countOfItem;)
        {
            if (indexes[i] < i)
            {
                if ((i & 1) == 1)
                {
                    Swap(ref items[i], ref items[indexes[i]]);
                }
                else
                {
                    Swap(ref items[i], ref items[0]);
                }

                if (funcExecuteAndTellIfShouldStop(items))
                {
                    return true;
                }

                indexes[i]++;
                i = 1;
            }
            else
            {
                indexes[i++] = 0;
            }
        }

        return false;
    }

    /// <summary>
    /// This function is to show a linq way but is far less efficient
    /// From: StackOverflow user: Pengyang : http://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    static void Swap<T>(ref T a, ref T b)
    {
        (b, a) = (a, b);
    }
}
