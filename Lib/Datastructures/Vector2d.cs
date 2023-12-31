﻿namespace Lib;

public struct Vector2d(decimal x, decimal y)
{
    public decimal X { get; set; } = x;
    public decimal Y { get; set; } = y;

    public decimal Length => (decimal)Math.Sqrt((double)(X * X + Y * Y));

    #region Operator Overloading
    public static Vector2d operator +(Vector2d vector1, Vector2d vector2)
    {
        return new Vector2d(vector1.X + vector2.X, vector1.Y + vector2.Y);
    }

    public static Vector2d operator -(Vector2d vector1, Vector2d vector2)
    {
        return new Vector2d(vector1.X - vector2.X, vector1.Y - vector2.Y);
    }

    public static Vector2d operator *(Vector2d vector, decimal scalar)
    {
        return new Vector2d(vector.X * scalar, vector.Y * scalar);
    }

    public static Vector2d operator *(decimal scalar, Vector2d vector)
    {
        return vector * scalar;
    }

    public static decimal operator *(Vector2d vector1, Vector2d vector2)
    {
        return vector1.X * vector2.X + vector1.Y * vector2.Y;
    }

    public static bool operator ==(Vector2d vector1, Vector2d vector2)
    {
        return vector1.X == vector2.X && vector1.Y == vector2.Y;
    }

    public static bool operator !=(Vector2d vector1, Vector2d vector2)
    {
        return vector1.X != vector2.X || vector1.Y != vector2.Y;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is Vector2d vector && vector.X == X && vector.Y == Y;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    #endregion
}
