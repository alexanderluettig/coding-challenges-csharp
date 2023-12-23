namespace LeetCode.Solved;

public class Solution1496
{
    [Test]
    public static void TestCase1()
    {
        var input = "NES";
        IsPathCrossing(input).Should().BeFalse();
    }

    [Test]
    public static void TestCase2()
    {
        var input = "NESWW";
        IsPathCrossing(input).Should().BeTrue();
    }

    public static bool IsPathCrossing(string path)
    {
        var visited = new HashSet<(int, int)>();
        var current = (0, 0);
        visited.Add(current);

        foreach (var direction in path)
        {
            current = direction switch
            {
                'N' => (current.Item1, current.Item2 + 1),
                'E' => (current.Item1 + 1, current.Item2),
                'S' => (current.Item1, current.Item2 - 1),
                'W' => (current.Item1 - 1, current.Item2),
                _ => throw new Exception("Invalid input.")
            };

            if (visited.Contains(current))
            {
                return true;
            }

            visited.Add(current);
        }

        return false;
    }
}
