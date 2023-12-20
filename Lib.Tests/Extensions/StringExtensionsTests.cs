namespace Lib.Tests.Extensions;

public class StringExtensionsTests
{
    public class NumberOfDifferences
    {
        [Theory]
        [InlineData("abc", "abc", 0)]
        [InlineData("abc", "abd", 1)]
        public void It_should_return_the_correct_number_of_differences(string a, string b, int expected)
        {
            var result = a.NumberOfDifferences(b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void It_should_throw_an_exception_when_the_strings_are_of_different_lengths()
        {
            var a = "abc";
            var b = "abcd";
            Assert.Throws<ArgumentException>(() => a.NumberOfDifferences(b));
        }
    }
}
