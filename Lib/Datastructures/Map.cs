using System.Text;

namespace Lib.Datastructures;

public class Map<T> where T : notnull
{
    public int Width { get; }
    public int Height { get; }
    private T[,] Data { get; init; }
    private readonly T _defaultValue;

    public Map(int height, int width, T defaultValue)
    {
        Width = width;
        Height = height;
        _defaultValue = defaultValue;


        Data = new T[height, width];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Data[y, x] = _defaultValue;
            }
        }
    }

    public Map(T defaultValue, T[,] data)
    {
        Width = data.GetLength(1);
        Height = data.GetLength(0);
        _defaultValue = defaultValue;

        Data = data;
    }

    public Map<T> RotateRight()
    {
        var rotatedMap = new Map<T>(Height, Width, _defaultValue);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                rotatedMap[y, x] = Data[Width - x - 1, y];
            }
        }

        return rotatedMap;
    }

    public Map<T> RotateLeft()
    {
        var rotatedMap = new Map<T>(Height, Width, _defaultValue);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                rotatedMap[y, x] = Data[x, Height - y - 1];
            }
        }

        return rotatedMap;
    }

    public Map<T> FlipHorizontal()
    {
        var flippedMap = new Map<T>(Width, Height, _defaultValue);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                flippedMap[y, x] = Data[y, Width - x - 1];
            }
        }

        return flippedMap;
    }

    public Map<T> FlipVertical()
    {
        var flippedMap = new Map<T>(Width, Height, _defaultValue);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                flippedMap[y, x] = Data[Height - y - 1, x];
            }
        }

        return flippedMap;
    }

    public Map<T> Transpose()
    {
        var transposedMap = new Map<T>(Width, Height, _defaultValue);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                transposedMap[x, y] = Data[y, x];
            }
        }

        return transposedMap;
    }

    #region Operator Overloads
    public T this[int y, int x]
    {
        get => Data[y, x];
        set => Data[y, x] = value;
    }

    public T[] this[int index]
    {
        get => Enumerable.Range(0, Width).Select(x => Data[index, x]).ToArray();
        set => Enumerable.Range(0, Width).ToList().ForEach(x => Data[index, x] = value[x]);
    }

    public static bool operator ==(Map<T> left, Map<T> right)
    {
        if (left.Width != right.Width || left.Height != right.Height)
        {
            return false;
        }

        for (var y = 0; y < left.Height; y++)
        {
            for (var x = 0; x < left.Width; x++)
            {
                if (!left[y, x].Equals(right[y, x]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !=(Map<T> left, Map<T> right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Map<T> map)
        {
            return this == map;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height, Data, _defaultValue);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Height; y++)
        {
            sb.AppendLine(string.Join(" ", this[y]));
        }

        return sb.ToString();
    }
    #endregion
}