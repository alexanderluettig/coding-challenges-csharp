namespace Lib;

public static class StringExtensions
{
    public static int NumberOfDifferences(this string s1, string s2)
    {
        if (s1.Length != s2.Length)
        {
            throw new ArgumentException("Strings must be of equal length");
        }

        var differences = 0;
        for (var i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                differences++;
            }
        }

        return differences;
    }
}
