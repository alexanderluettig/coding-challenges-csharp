namespace Lib.Values;

public static class Direction
{
    public static readonly Vector2d NORTH = new(0, 1);
    public static readonly Vector2d EAST = new(1, 0);
    public static readonly Vector2d SOUTH = new(0, -1);
    public static readonly Vector2d WEST = new(-1, 0);
    public static readonly Vector2d NORTHEAST = new(1, 1);
    public static readonly Vector2d SOUTHEAST = new(1, -1);
    public static readonly Vector2d SOUTHWEST = new(-1, -1);
    public static readonly Vector2d NORTHWEST = new(-1, 1);
    public static readonly Vector2d Up = new(0, 1);
    public static readonly Vector2d Down = new(0, -1);
    public static readonly Vector2d Left = new(-1, 0);
    public static readonly Vector2d Right = new(1, 0);
    public static readonly Vector2d UpLeft = new(-1, 1);
    public static readonly Vector2d UpRight = new(1, 1);
    public static readonly Vector2d DownLeft = new(-1, -1);
    public static readonly Vector2d DownRight = new(1, -1);
}
