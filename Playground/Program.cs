string[] test = ["e", "r", "a", "d", "i", "o"];

var permutations = test.Select(x => x).GetPermutations(test.Length).ToList();

foreach (var permutation in permutations)
{
    Console.WriteLine(string.Join("", permutation));
}