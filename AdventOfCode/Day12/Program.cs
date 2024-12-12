namespace AdventOfCode.Day12;

public class Day12
{
    const string FILEPATH = "Day12\\input.txt";
    public Day12()
    {
        Console.WriteLine(GetFences(GetMap()).Item1);
        Console.WriteLine(GetFences(GetMap()).Item2);
    }
    private static (int, int) GetFences(string[] map)
    {
        var visited = new bool[map.Length, map[0].Length];

        int totalPrice = 0;
        int totalPrice2 = 0;

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (!visited[i, j])
                {
                    var region = GetPlantsAndPerimeter(i, j, map, visited);
                    totalPrice += region.Item1 * region.Item2;
                    region.Item3.Sort();
                    totalPrice2 += region.Item1 * GetUniqueSidesCount(region.Item3);
                }
            }
        }
        return (totalPrice, totalPrice2);
    }

    static (int, int, List<string>) GetPlantsAndPerimeter(int x, int y, string[] map, bool[,] visited)
    {
        var result = (area: 0, perimeter: 0, sides: new List<string>());

        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            x = current.x;
            y = current.y;

            if (visited[x, y]) continue;

            visited[x, y] = true;

            // Gauche
            if (y - 1 < 0 || map[x][y - 1] != map[x][y])
            {
                result.perimeter += 1;
                result.sides.Add($"l{y},{x}");
            }
            else if (!visited[x, y - 1])
            {
                queue.Enqueue((x, y - 1));
            }

            // Bas
            if (x + 1 >= map.Length || map[x + 1][y] != map[x][y])
            {
                result.perimeter += 1;
                result.sides.Add($"d{x},{y}");
            }
            else if (!visited[x + 1, y])
            {
                queue.Enqueue((x + 1, y));
            }

            // Droite
            if (y + 1 >= map[x].Length || map[x][y + 1] != map[x][y])
            {
                result.perimeter += 1;
                result.sides.Add($"r{y},{x}");
            }
            else if (!visited[x, y + 1])
            {
                queue.Enqueue((x, y + 1));
            }

            // Haut
            if (x - 1 < 0 || map[x - 1][y] != map[x][y])
            {
                result.perimeter += 1;
                result.sides.Add($"u{x},{y}");
            }
            else if (!visited[x - 1, y])
            {
                queue.Enqueue((x - 1, y));
            }

            result.area += 1;
        }

        return result;
    }

    static int GetUniqueSidesCount(List<string> sides)
    {
        var count = 1;
        var prevElement = sides[0];
        for (int i = 1; i < sides.Count; i++)
        {
            var prevParts = prevElement.Split(",");
            var currParts = sides[i].Split(",");
            prevElement = sides[i];
            if (currParts[0] == prevParts[0] && int.Parse(currParts[1]) - int.Parse(prevParts[1]) == 1) continue;
            count++;
        }
        return count;
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