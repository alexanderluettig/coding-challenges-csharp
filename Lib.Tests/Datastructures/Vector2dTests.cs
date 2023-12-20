namespace Lib.Tests;

public class Vector2dTests
{
    [Fact]
    public void It_should_compare_vectors_correctly()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(1, 2);
        var vector3 = new Vector2d(3, 4);

        Assert.True(vector1 == vector2);
        Assert.False(vector1 == vector3);
        Assert.False(vector1 != vector2);
        Assert.True(vector1 != vector3);
    }

    [Fact]
    public void It_should_add_vectors_correctly()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(3, 4);
        var expected = new Vector2d(4, 6);

        var actual = vector1 + vector2;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_substract_vectors_correctly()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(3, 4);
        var expected = new Vector2d(-2, -2);

        var actual = vector1 - vector2;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_multiply_vectors_correctly_using_scalar_product()
    {
        var vector = new Vector2d(1, 2);
        var scalar = 3;
        var expected = new Vector2d(3, 6);

        var actual = vector * scalar;
        var actual2 = scalar * vector;

        Assert.Equal(expected, actual);
        Assert.Equal(expected, actual2);
    }

    [Fact]
    public void It_should_multiply_vectors_correctly_using_dot_product()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(3, 4);
        var expected = 11;

        var actual = vector1 * vector2;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_calculate_the_manhattan_distance_correctly()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(3, 4);
        var expected = 4;

        var actual = vector1.ManhattanDistance(vector2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void It_should_calculate_the_euclidean_distance_correctly()
    {
        var vector1 = new Vector2d(1, 2);
        var vector2 = new Vector2d(3, 4);
        var expected = (decimal)Math.Sqrt(8);

        var actual = vector1.EuclideanDistance(vector2);

        Assert.Equal(expected, actual);
    }
}
