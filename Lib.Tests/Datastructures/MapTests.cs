namespace Lib.Tests.Datastructures;

public class MapTests
{
    [Fact]
    public void It_should_create_a_map_with_the_correct_dimensions_and_default_value()
    {
        var map = new Map<int>(10, 20, 0);

        Assert.Equal(10, map.Height);
        Assert.Equal(20, map.Width);

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                Assert.Equal(0, map[y, x]);
            }
        }
    }

    [Fact]
    public void It_should_create_a_map_with_the_correct_dimensions_and_points()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);

        Assert.Equal(2, map.Width);
        Assert.Equal(2, map.Height);

        Assert.Equal(1, map[0, 0]);
        Assert.Equal(2, map[0, 1]);
        Assert.Equal(3, map[1, 0]);
        Assert.Equal(4, map[1, 1]);
    }

    [Fact]
    public void It_should_rotate_map_with_dim_2x2_to_right()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateRight();

        Assert.Equal(2, rotatedMap.Width);
        Assert.Equal(2, rotatedMap.Height);

        Assert.Equal(3, rotatedMap[0, 0]);
        Assert.Equal(1, rotatedMap[0, 1]);
        Assert.Equal(4, rotatedMap[1, 0]);
        Assert.Equal(2, rotatedMap[1, 1]);
    }

    [Fact]
    public void It_should_rotate_map_with_dim_3x3_to_right()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateRight();

        Assert.Equal(3, rotatedMap.Width);
        Assert.Equal(3, rotatedMap.Height);

        Assert.Equal(7, rotatedMap[0, 0]);
        Assert.Equal(4, rotatedMap[0, 1]);
        Assert.Equal(1, rotatedMap[0, 2]);
        Assert.Equal(8, rotatedMap[1, 0]);
        Assert.Equal(5, rotatedMap[1, 1]);
        Assert.Equal(2, rotatedMap[1, 2]);
        Assert.Equal(9, rotatedMap[2, 0]);
        Assert.Equal(6, rotatedMap[2, 1]);
        Assert.Equal(3, rotatedMap[2, 2]);
    }

    [Fact]
    public void It_should_return_same_map_after_4_rotations()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateRight().RotateRight().RotateRight().RotateRight();

        Assert.Equal(map, rotatedMap);
    }

    [Fact]
    public void It_should_rotate_map_with_dim_2x2_to_left()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateLeft();

        Assert.Equal(2, rotatedMap.Width);
        Assert.Equal(2, rotatedMap.Height);

        Assert.Equal(2, rotatedMap[0, 0]);
        Assert.Equal(4, rotatedMap[0, 1]);
        Assert.Equal(1, rotatedMap[1, 0]);
        Assert.Equal(3, rotatedMap[1, 1]);
    }

    [Fact]
    public void It_should_rotate_map_with_dim_3x3_to_left()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateLeft();

        Assert.Equal(3, rotatedMap.Width);
        Assert.Equal(3, rotatedMap.Height);

        Assert.Equal(3, rotatedMap[0, 0]);
        Assert.Equal(6, rotatedMap[0, 1]);
        Assert.Equal(9, rotatedMap[0, 2]);
        Assert.Equal(2, rotatedMap[1, 0]);
        Assert.Equal(5, rotatedMap[1, 1]);
        Assert.Equal(8, rotatedMap[1, 2]);
        Assert.Equal(1, rotatedMap[2, 0]);
        Assert.Equal(4, rotatedMap[2, 1]);
        Assert.Equal(7, rotatedMap[2, 2]);
    }

    [Fact]
    public void It_should_return_same_map_after_4_rotations_to_left()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var rotatedMap = map.RotateLeft().RotateLeft().RotateLeft().RotateLeft();

        Assert.Equal(map, rotatedMap);
    }

    [Fact]
    public void It_should_flip_map_horizontally()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipHorizontal();

        Assert.Equal(2, flippedMap.Width);
        Assert.Equal(2, flippedMap.Height);

        Assert.Equal(2, flippedMap[0, 0]);
        Assert.Equal(1, flippedMap[0, 1]);
        Assert.Equal(4, flippedMap[1, 0]);
        Assert.Equal(3, flippedMap[1, 1]);
    }

    [Fact]
    public void It_should_flip_map_horizontally_with_dim_3x3()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipHorizontal();

        Assert.Equal(3, flippedMap.Width);
        Assert.Equal(3, flippedMap.Height);

        Assert.Equal(3, flippedMap[0, 0]);
        Assert.Equal(2, flippedMap[0, 1]);
        Assert.Equal(1, flippedMap[0, 2]);
        Assert.Equal(6, flippedMap[1, 0]);
        Assert.Equal(5, flippedMap[1, 1]);
        Assert.Equal(4, flippedMap[1, 2]);
        Assert.Equal(9, flippedMap[2, 0]);
        Assert.Equal(8, flippedMap[2, 1]);
        Assert.Equal(7, flippedMap[2, 2]);
    }

    [Fact]
    public void It_should_return_same_map_after_2_flips()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipHorizontal().FlipHorizontal();

        Assert.Equal(map, flippedMap);
    }

    [Fact]
    public void It_should_flip_map_vertically()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipVertical();

        Assert.Equal(2, flippedMap.Width);
        Assert.Equal(2, flippedMap.Height);

        Assert.Equal(3, flippedMap[0, 0]);
        Assert.Equal(4, flippedMap[0, 1]);
        Assert.Equal(1, flippedMap[1, 0]);
        Assert.Equal(2, flippedMap[1, 1]);
    }

    [Fact]
    public void It_should_flip_map_vertically_with_dim_3x3()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipVertical();

        Assert.Equal(3, flippedMap.Width);
        Assert.Equal(3, flippedMap.Height);

        Assert.Equal(7, flippedMap[0, 0]);
        Assert.Equal(8, flippedMap[0, 1]);
        Assert.Equal(9, flippedMap[0, 2]);
        Assert.Equal(4, flippedMap[1, 0]);
        Assert.Equal(5, flippedMap[1, 1]);
        Assert.Equal(6, flippedMap[1, 2]);
        Assert.Equal(1, flippedMap[2, 0]);
        Assert.Equal(2, flippedMap[2, 1]);
        Assert.Equal(3, flippedMap[2, 2]);
    }

    [Fact]
    public void It_should_return_same_map_after_2_vertical_flips()
    {
        var points = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        var map = new Map<int>(0, points);

        var flippedMap = map.FlipVertical().FlipVertical();

        Assert.Equal(map, flippedMap);
    }

    [Fact]
    public void It_should_transpose_map_correctly_same_dimensions()
    {
        var points = new int[2, 2]
        {
            { 1, 2 },
            { 3, 4 }
        };

        var map = new Map<int>(0, points);
        var transposedMap = map.Transpose();

        Assert.Equal(2, transposedMap.Width);
        Assert.Equal(2, transposedMap.Height);

        Assert.Equal(1, transposedMap[0, 0]);
        Assert.Equal(3, transposedMap[0, 1]);
        Assert.Equal(2, transposedMap[1, 0]);
        Assert.Equal(4, transposedMap[1, 1]);
    }

    [Fact]
    public void It_should_transpose_map_correctly_different_dimensions()
    {
        var points = new int[3, 2]
        {
            { 1, 2 },
            { 3, 4 },
            { 5, 6 }
        };

        var map = new Map<int>(0, points);
        var transposedMap = map.Transpose();

        Assert.Equal(2, transposedMap.Height);
        Assert.Equal(3, transposedMap.Width);

        Assert.Equal(1, transposedMap[0, 0]);
        Assert.Equal(3, transposedMap[0, 1]);
        Assert.Equal(5, transposedMap[0, 2]);
        Assert.Equal(2, transposedMap[1, 0]);
        Assert.Equal(4, transposedMap[1, 1]);
        Assert.Equal(6, transposedMap[1, 2]);
    }

    [Fact]
    public void It_should_return_same_map_after_2_transpositions()
    {
        var points = new int[3, 2]
        {
            { 1, 2 },
            { 3, 4 },
            { 5, 6 }
        };

        var map = new Map<int>(0, points);

        var transposedMap = map.Transpose().Transpose();

        Assert.Equal(map, transposedMap);
    }
}
