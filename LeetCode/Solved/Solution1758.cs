namespace LeetCode;

public class Solution1758
{
    [Test]
    public static void TestCase1()
    {
        var input = "0100";
        MinOperations(input).Should().Be(1);
    }

    [Test]
    public static void TestCase2()
    {
        var input = "10";
        MinOperations(input).Should().Be(0);
    }

    [Test]
    public static void TestCase3()
    {
        var input = "1111";
        MinOperations(input).Should().Be(2);
    }

    public static int MinOperations(string s)
    {
        var ones = 0;
        var zeros = 0;
        var currentStartingOne = '0';
        var currentStartingZero = '1';

        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] != currentStartingOne)
            {
                ones++;
            }

            if (s[i] != currentStartingZero)
            {
                zeros++;
            }

            currentStartingZero = currentStartingZero == '0' ? '1' : '0';
            currentStartingOne = currentStartingOne == '0' ? '1' : '0';
        }

        return Math.Min(ones, zeros);
    }
}
