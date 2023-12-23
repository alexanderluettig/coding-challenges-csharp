using System.Diagnostics;

namespace LeetCode;

public class TestRunner
{
    public static void Run<T>()
    {
        var testMethods = typeof(T)
            .GetMethods()
            .Where(m => m.GetCustomAttributes(typeof(TestAttribute), false).Length > 0);

        var instance = Activator.CreateInstance<T>();

        Stopwatch sw = new();
        sw.Start();

        foreach (var testMethod in testMethods)
        {
            try
            {
                testMethod.Invoke(instance, null);
                Console.WriteLine($"{testMethod.Name} passed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{testMethod.Name} failed: {e.InnerException?.Message}");
            }
        }

        sw.Stop();
        Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds}ms");
    }
}
