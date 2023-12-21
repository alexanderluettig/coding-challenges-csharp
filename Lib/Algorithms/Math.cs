namespace Lib.Algorithms;

public static class MathAlg
{
    public static long FindLeastCommonMultiple(IEnumerable<long> numbers) =>
        numbers.Aggregate((long)1, (current, number) => current / GreatestCommonDivisor(current, number) * number);

    public static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            a %= b;
            (a, b) = (b, a);
        }
        return a;
    }
}
