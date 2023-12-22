namespace LeetCode.Solved;

// https://leetcode.com/problems/maximum-score-after-splitting-a-string
public class Solution1422
{
    [Test]
    public static void TestCase1()
    {
        var input = "011101";
        MaxScore(input).Should().Be(5);
    }

    [Test]
    public static void TestCase2()
    {
        var input = "00111";
        MaxScore(input).Should().Be(5);
    }

    [Test]
    public static void TestCase3()
    {
        var input = "1111";
        MaxScore(input).Should().Be(3);
    }

    public static int MaxScore(string s)
    {
        var rightScore = s.Count(c => c == '1');
        var leftScore = 0;
        var maxScore = 0;
        for (var i = 0; i < s.Length - 1; i++)
        {
            _ = s[i] switch
            {
                '0' => leftScore++,
                '1' => rightScore--,
                _ => throw new Exception("Invalid input.")
            };

            maxScore = Math.Max(maxScore, leftScore + rightScore);
        }

        return maxScore;
    }
}
