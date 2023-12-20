using System.Collections;
using Lib.Tests.Datastructures;
using Lib.Values;

namespace Lib.Tests;

public class Point2DTests
{
    [Fact]
    public void It_should_compare_points_correctly()
    {
        var point1 = new Point2D<int>(1, 2, 3);
        var point2 = new Point2D<int>(1, 2, 3);
        var point3 = new Point2D<int>(3, 4, 5);

        Assert.True(point1 == point2);
        Assert.False(point1 == point3);
        Assert.False(point1 != point2);
        Assert.True(point1 != point3);
    }

    [Fact]
    public void It_should_add_vectors_to_points_correctly()
    {
        var point1 = new Point2D<int>(1, 2, 3);
        var point2 = new Point2D<int>(3, 4, 5);
        var vector = new Vector2d(1, 2);

        Assert.Equal(new Point2D<int>(2, 4, 3), point1 + vector);
        Assert.Equal(new Point2D<int>(4, 6, 5), point2 + vector);
    }

    [Fact]
    public void It_should_subtract_vectors_from_points_correctly()
    {
        var point1 = new Point2D<int>(1, 2, 3);
        var point2 = new Point2D<int>(3, 4, 5);
        var vector = new Vector2d(1, 2);

        Assert.Equal(new Point2D<int>(0, 0, 3), point1 - vector);
        Assert.Equal(new Point2D<int>(2, 2, 5), point2 - vector);
    }

    [Fact]
    public void It_should_move_a_point_correctly()
    {
        var point = new Point2D<int>(0, 0, 0);
        point = point.Move([Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST]);

        Assert.Equal(new Point2D<int>(0, 0, 0), point);
    }

    [Theory]
    [ClassData(typeof(DirectionTestData))]
    public void It_should_move_a_point_correctly_into_given_direction(Vector2d direction, decimal steps, decimal endX, decimal endY)
    {
        var point = new Point2D<int>(0, 0, 0);
        point = point.Move(direction, steps);

        Assert.Equal(new Point2D<int>(endX, endY, 0), point);
    }

    [Theory]
    [ClassData(typeof(DirectionTestData))]
    public void It_should_move_a_point_correctly_into_given_direction_with_operator_overload(Vector2d direction, decimal steps, decimal endX, decimal endY)
    {
        var point = new Point2D<int>(0, 0, 0);
        point += direction * steps;

        Assert.Equal(new Point2D<int>(endX, endY, 0), point);
    }

    class DirectionTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [Direction.NORTH, 2, 0, 2],
            [Direction.EAST, 2, 2, 0],
            [Direction.SOUTH, 2, 0, -2],
            [Direction.WEST, 2, -2, 0],
            [Direction.NORTHEAST, 2, 2, 2],
            [Direction.SOUTHEAST, 2, 2, -2],
            [Direction.SOUTHWEST, 2, -2, -2],
            [Direction.NORTHWEST, 2, -2, 2],
            [Direction.Up, 2, 0, 2],
            [Direction.Right, 2, 2, 0],
            [Direction.Down, 2, 0, -2],
            [Direction.Left, 2, -2, 0],
            [Direction.UpRight, 2, 2, 2],
            [Direction.DownRight, 2, 2, -2],
            [Direction.DownLeft, 2, -2, -2],
            [Direction.UpLeft, 2, -2, 2],
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
