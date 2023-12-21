int[] test = Enumerable.Range(0, 10).ToArray();

foreach (int[] combination in Combinations.GetAllCombinations(test, 3))
{
    Console.WriteLine(string.Join(", ", combination));
}