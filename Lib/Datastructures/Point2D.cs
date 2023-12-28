namespace Lib.Datastructures;

public sealed class Point2D<T>(decimal x, decimal y, T value) where T : notnull
{
    public Vector2d Position { get; set; } = new(x, y);
    public decimal X => Position.X;
    public decimal Y => Position.Y;
    public T Value { get; set; } = value;

    #region Operator Overloads
    public static Point2D<T> operator +(Point2D<T> point1, Vector2d vector2D)
        => new(point1.X + vector2D.X, point1.Y + vector2D.Y, point1.Value);

    public static Point2D<T> operator -(Point2D<T> point1, Vector2d vector2D)
        => new(point1.X - vector2D.X, point1.Y - vector2D.Y, point1.Value);

    public static bool operator ==(Point2D<T> point1, Point2D<T> point2)
        => point1.X == point2.X && point1.Y == point2.Y && point1.Value.Equals(point2.Value);

    public static bool operator !=(Point2D<T> point1, Point2D<T> point2)
        => !(point1 == point2);

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) && obj is Point2D<T> point && point.Value.Equals(Value);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Value);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, value = {Value})";
    }
    #endregion
}
