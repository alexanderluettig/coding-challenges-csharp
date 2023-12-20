namespace Lib.Tests.Extensions;

public class ArrayExtensionsTests
{
    public class ArrayTransposition
    {
        [Fact]
        public void It_should_transpose_the_array()
        {
            int[][] array = [
                [ 1, 2, 3 ],
                [ 4, 5, 6 ]
            ];
            int[][] expected = [
                [1, 4],
                [2, 5],
                [3, 6]
            ];
            var result = array.Transpose();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void It_should_result_in_same_array_when_transposed_twice()
        {
            int[][] array = [
                [ 1, 2, 3 ],
                [ 4, 5, 6 ]
            ];
            var result = array.Transpose().Transpose();
            Assert.Equal(array, result);
        }
    }
}
