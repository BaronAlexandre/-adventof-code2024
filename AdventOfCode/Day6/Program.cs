namespace AdventOfCode.Day6;

public class Day6
{
    const string FILEPATH = "Day6\\input.txt";

    public Day6()
    {

        Console.WriteLine(SolvePart1(GetMap()));
        Console.WriteLine(SolvePart2(GetMap()));
    }

    public string SolvePart1(char[][] grid)
    {
        return GetVisited(grid)!.Count.ToString();
    }

    public string SolvePart2(char[][] grid)
    {
        var infiniteLoopCount = 0;

        foreach (var visited in GetVisited(grid)!.Skip(1))
        {
            grid[visited.y][visited.x] = '#';

            var newVisited = GetVisited(grid);

            grid[visited.y][visited.x] = '.';

            if (newVisited is null)
                infiniteLoopCount++;
        }

        return infiniteLoopCount.ToString();
    }

    private static HashSet<(int x, int y)>? GetVisited(char[][] grid)
    {
        var guardPosition = FindGuard(grid);
        var guardDirection = grid[guardPosition.y][guardPosition.x];
        var visitState = new HashSet<(int x, int y, char direction)>();

        while (true)
        {
            if (!visitState.Add((guardPosition.x, guardPosition.y, guardDirection)))
                return null;

            try
            {
                var nextPosition = guardDirection switch
                {
                    '^' => grid[guardPosition.y - 1][guardPosition.x] == '#' ? (guardPosition.x + 1, guardPosition.y, '>') : (guardPosition.x, guardPosition.y - 1, '^'),
                    '>' => grid[guardPosition.y][guardPosition.x + 1] == '#' ? (guardPosition.x, guardPosition.y + 1, 'v') : (guardPosition.x + 1, guardPosition.y, '>'),
                    'v' => grid[guardPosition.y + 1][guardPosition.x] == '#' ? (guardPosition.x - 1, guardPosition.y, '<') : (guardPosition.x, guardPosition.y + 1, 'v'),
                    '<' => grid[guardPosition.y][guardPosition.x - 1] == '#' ? (guardPosition.x, guardPosition.y - 1, '^') : (guardPosition.x - 1, guardPosition.y, '<'),
                };

                guardDirection = nextPosition.Item3;

                if (grid[nextPosition.Item2][nextPosition.Item1] != '#')
                    guardPosition = (nextPosition.Item1, nextPosition.Item2);

            }
            catch (IndexOutOfRangeException)
            {
                break;
            }
        }

        return visitState.Select(x => (x.x, x.y)).ToHashSet();
    }

    private static (int x, int y) FindGuard(char[][] grid)
    {
        for (var i = 0; i < grid.Length; i++)
            for (var j = 0; j < grid[i].Length; j++)
                if (grid[i][j] == '^')
                    return (j, i);

        throw new Exception("Guard not found");
    }


    private static char[][] GetMap()
    {
        List<char[]> map = [];

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                map.Add(line.ToCharArray());
            }

            return map.ToArray();
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}