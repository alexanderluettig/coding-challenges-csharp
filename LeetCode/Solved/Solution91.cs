namespace LeetCode;

public class Solution91
{
    private static readonly Dictionary<string, int> _cache = [];

    [Test]
    public static void TestCase1()
    {
        var input = "12";
        NumDecodings(input).Should().Be(2);
    }

    [Test]
    public static void TestCase2()
    {
        var input = "226";
        NumDecodings(input).Should().Be(3);
    }

    [Test]
    public static void TestCase3()
    {
        var input = "06";
        NumDecodings(input).Should().Be(0);
    }

    public static int NumDecodings(string s)
    {
        if (s.Length == 0)
        {
            return 0;
        }

        if (s[0] == '0')
        {
            return 0;
        }

        if (s.Length == 1)
        {
            return 1;
        }

        if (s.Length == 2)
        {
            var num = int.Parse(s);
            if (num <= 26)
            {
                return num % 10 == 0 ? 1 : 2;
            }

            return s[1] == '0' ? 0 : 1;
        }

        if (_cache.TryGetValue(s, out int value))
        {
            return value;
        }

        var result = 0;
        var second = int.Parse(s[..2]);
        if (second <= 26)
        {
            result += NumDecodings(s[2..]);
        }

        if (s[1] != '0')
        {
            result += NumDecodings(s[1..]);
        }

        _cache[s] = result;
        return result;
    }
}
