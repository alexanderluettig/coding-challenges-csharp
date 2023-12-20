using Lib.Datastructures;

namespace Lib;

public static class Point2dExtensions
{
    public static Point2D<T> Move<T>(this Point2D<T> point, Vector2d direction, decimal steps) where T : notnull
        => point + direction * steps;

    public static Point2D<T> Move<T>(this Point2D<T> point, Vector2d[] directions) where T : notnull
        => point + directions.Aggregate((a, b) => a + b);
}
