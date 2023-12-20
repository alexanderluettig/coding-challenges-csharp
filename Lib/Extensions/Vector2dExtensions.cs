namespace Lib;

public static class Vector2dExtensions
{
    public static decimal ManhattanDistance(this Vector2d vector1, Vector2d vector2)
        => Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y);

    public static decimal EuclideanDistance(this Vector2d vector1, Vector2d vector2)
        => (decimal)Math.Sqrt(Math.Pow((double)vector1.X - (double)vector2.X, 2) + Math.Pow((double)vector1.Y - (double)vector2.Y, 2));
}
