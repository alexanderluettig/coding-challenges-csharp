namespace AdventOfCode.Y2023.Day16;

internal class Solution(string input) : ISolver(input)
{
    private readonly Dictionary<(char c, int d), int> map = new()
    {
        [('.', 0)] = 0,
        [('.', 1)] = 1,
        [('.', 2)] = 2,
        [('.', 3)] = 3,
        [('/', 0)] = 3,
        [('/', 1)] = 2,
        [('/', 2)] = 1,
        [('/', 3)] = 0,
        [('\\', 0)] = 1,
        [('\\', 1)] = 0,
        [('\\', 2)] = 3,
        [('\\', 3)] = 2,
        [('-', 0)] = 0,
        [('-', 1)] = 5,
        [('-', 2)] = 2,
        [('-', 3)] = 5,
        [('|', 0)] = 6,
        [('|', 1)] = 1,
        [('|', 2)] = 6,
        [('|', 3)] = 3,
    };
    public override long SolvePartOne()
    {
        var S = _input.Split(Environment.NewLine).ToList();

        var currentbeams = new Queue<(int tox, int toy, int fromd)>();
        currentbeams.Enqueue((0, 0, 0));


        return GetEnergy(S, currentbeams, map);
    }

    public override long SolvePartTwo()
    {
        var S = _input.Split(Environment.NewLine).ToList();

        long result = 0;

        var currentbeams = new Queue<(int tox, int toy, int fromd)>();
        currentbeams.Enqueue((0, 0, 0));

        for (int i = 0; i < S.Count; i++)
        {
            currentbeams.Enqueue((0, i, 0));
            var a1 = GetEnergy(S, currentbeams, map);
            if (a1 > result) result = a1;

            currentbeams.Enqueue((S.Count - 1, i, 2));
            a1 = GetEnergy(S, currentbeams, map);
            if (a1 > result) result = a1;
        }

        for (int i = 0; i < S[0].Length; i++)
        {
            currentbeams.Enqueue((i, 0, 1));
            var a1 = GetEnergy(S, currentbeams, map);
            if (a1 > result) result = a1;

            currentbeams.Enqueue((i, S[0].Length - 1, 3));
            a1 = GetEnergy(S, currentbeams, map);
            if (a1 > result) result = a1;
        }

        return result;
    }
    static long GetEnergy(List<string> S, Queue<(int tox, int toy, int fromd)> currentbeams, Dictionary<(char c, int d), int> map)
    {
        var visited = new HashSet<(int x, int y, int fromdirection)>();
        var move = new (int dx, int dy)[4] { (1, 0), (0, 1), (-1, 0), (0, -1) };

        while (currentbeams.Count > 0)
        {
            var beam = currentbeams.Dequeue();
            if (!visited.Contains(beam))
            {
                visited.Add(beam);
                var newd = map[(S[beam.toy][beam.tox], beam.fromd)];

                if (newd < 5) Addwork(currentbeams, move, beam, newd, S[0].Length, S.Count);
                else
                {
                    var nd1 = newd == 5 ? 0 : 1;
                    var nd2 = newd == 5 ? 2 : 3;
                    Addwork(currentbeams, move, beam, nd1, S[0].Length, S.Count);
                    Addwork(currentbeams, move, beam, nd2, S[0].Length, S.Count);
                }
            }
        }

        return visited.Select(v => (v.x, v.y)).ToHashSet().Count;

        static void Addwork(Queue<(int tox, int toy, int fromd)> currentbeams, (int dx, int dy)[] move, (int tox, int toy, int fromd) beam, int newd, int maxx, int maxy)
        {
            int newx = beam.tox + move[newd].dx;
            int newy = beam.toy + move[newd].dy;
            if (0 <= newx && newx < maxx && newy >= 0 && newy < maxy)
                currentbeams.Enqueue((newx, newy, newd));
        }
    }
}