namespace Lib.Extensions;

public static class ArrayExtensions
{
    public static T[][] Transpose<T>(this T[][] array)
    {
        var result = new T[array[0].Length][];
        for (var i = 0; i < array[0].Length; i++)
        {
            result[i] = new T[array.Length];
            for (var j = 0; j < array.Length; j++)
            {
                result[i][j] = array[j][i];
            }
        }

        return result;
    }
}
