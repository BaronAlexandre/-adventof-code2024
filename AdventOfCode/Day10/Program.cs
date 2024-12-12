namespace AdventOfCode.Day10;

public class Day10
{
    const string FILEPATH = "Day10\\input.txt";

    public Day10()
    {
        GetStartingHiking(GetMap());
    }

    private static void GetStartingHiking(string[] map)
    {
        int totalScore1 = 0;
        int totalScore2 = 0;

        // Parcours de chaque cellule de la carte
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == '0')
                {
                    totalScore1 += CountNb9FriomStarting(map, i, j);
                    totalScore2 += CountDistinctHiking(map, i, j);
                }
            }
        }

        Console.WriteLine(totalScore1);
        Console.WriteLine(totalScore2);
    }
    private static int CountNb9FriomStarting(string[] map, int x, int y)
    {
        var visited = new bool[map.Length, map[0].Length];

        int score = 0;
        var myqueue = new Queue<(int, int)>();
        myqueue.Enqueue((x, y));
        visited[x, y] = true;

        int[] dx = [-1, 1, 0, 0];
        int[] dy = [0, 0, -1, 1];

        // Exploration en largeur
        while (myqueue.Count > 0)
        {
            (x, y) = myqueue.Dequeue();

            // Si 9 c'est good
            if (map[x][y] == '9')
            {
                score++;
            }
            // On explore
            else
            {
                // Pour chaque direction
                for (var i = 0; i < 4; i++)
                {
                    int voisinX = x + dx[i];
                    int voisinY = y + dy[i];

                    if (voisinX >= 0 && voisinX < map.Length && voisinY >= 0 && voisinY < map[0].Length
                        && !visited[voisinX, voisinY]
                        && map[voisinX][voisinY] == (char)(map[x][y] + 1)) // Si + 1, pas déjà vu et dans la map
                    {
                        visited[voisinX, voisinY] = true;
                        myqueue.Enqueue((voisinX, voisinY));
                    }
                }
            }
        }

        return score;
    }

    private static int CountDistinctHiking(string[] map, int x, int y)
    {
        var distincthikings = new HashSet<string>();

        int[] dx = [-1, 1, 0, 0];
        int[] dy = [0, 0, -1, 1];

        // Queue mais maintenant avec le path unique
        var myqueue = new Queue<(int x, int y, List<(int, int)> path)>();
        var path = new List<(int, int)> { (x, y) };

        myqueue.Enqueue((x, y, path));

        while (myqueue.Count > 0)
        {
            (x, y, path) = myqueue.Dequeue();

            // Vérifier les limites et les cases bloquées
            if (x >= 0 && x < map.Length && y >= 0 && y < map[0].Length)
            {
                if (path.Count == 1 || (1 + map[path[^1].Item1][path[^1].Item2] == map[x][y]))
                {
                    var pathtemp = new List<(int, int)>(path)
                    {
                        (x, y)
                    };

                    if (map[x][y] == '9')
                    {
                        var pathKey = string.Join(",", pathtemp.Select(p => $"({p.Item1},{p.Item2})"));
                        distincthikings.Add(pathKey);
                    }
                    else
                    {
                        // On enqueue les 4 directions
                        for (int dir = 0; dir < 4; dir++)
                        {
                            int newX = x + dx[dir];
                            int newY = y + dy[dir];

                            myqueue.Enqueue((newX, newY, pathtemp));
                        }
                    }
                }
            }
        }

        return distincthikings.Count;
    }


    private static string[] GetMap()
    {
        List<string> map = [];

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                map.Add(line.Trim());
            }

            return map.ToArray();
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}