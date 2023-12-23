namespace LeetCode.Solved;

public class Solution32
{
    [Test]
    public static void TestCase1()
    {
        var input = "(()";
        LongestValidParentheses(input).Should().Be(2);
    }

    [Test]
    public static void TestCase2()
    {
        var input = ")()())";
        LongestValidParentheses(input).Should().Be(4);
    }

    [Test]
    public static void TestCase3()
    {
        var input = "";
        LongestValidParentheses(input).Should().Be(0);
    }

    [Test]
    public static void TestCase4()
    {
        var input = "()(()";
        LongestValidParentheses(input).Should().Be(2);
    }

    [Test]
    public static void TestCase5()
    {
        var input = "()(())";
        LongestValidParentheses(input).Should().Be(6);
    }

    [Test]
    public static void TestCase6()
    {
        var input = "(()(((()";
        LongestValidParentheses(input).Should().Be(2);
    }

    public static int LongestValidParentheses(string s)
    {
        var stack = new Stack<int>();
        var max = 0;
        var last = -1;

        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                if (stack.Count == 0)
                {
                    last = i;
                }
                else
                {
                    stack.Pop();
                    if (stack.Count == 0)
                    {
                        max = Math.Max(max, i - last);
                    }
                    else
                    {
                        max = Math.Max(max, i - stack.Peek());
                    }
                }
            }
        }

        return max;
    }
}
