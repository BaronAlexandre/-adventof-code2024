namespace AdventOfCode.Day6;

public class Day6
{
    const string FILEPATH = "Day6\\input.txt";

    public Day6()
    {
		Console.WriteLine(GetPositionsVisited(GetMap()));
	}

	public static int GetPositionsVisited(List<string> map)
	{
		var directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };

		var x = -1;
		var y = -1;
		var direction = -1;
		for (int i = 0; i < map.Count; i++)
		{
			for (int j = 0; j < map[0].Length; j++)
			{
				if (map[i][j] == '^')
				{
					x = i;
					y = j;
					direction = 0;
				}
				else if (map[i][j] == '>')
				{
					x = i;
					y = j;
					direction = 1;
				}
				else if (map[i][j] == 'v')
				{
					x = i;
					y = j;
					direction = 2;
				}
				else if (map[i][j] == '<')
				{
					x = i;
					y = j;
					direction = 3;
				}
			}
		}

		HashSet<(int, int)> visited = [ new(x, y) ];

		var end = false;
		while (!end)
		{
			int xTemp = x + directions[direction].Item1;
			int yTemp = y + directions[direction].Item2;

			if (xTemp < 0 || yTemp < 0 || xTemp >= map.Count || yTemp >= map[0].Length)
			{
				end = true;
			}
			else
			{
				if (map[xTemp][yTemp] == '#')
					direction = (direction + 1) % 4;
				else
				{
					x = xTemp;
					y = yTemp;
					visited.Add((x, y));
				}
			}
		}

		return visited.Count;
	}
	private static List<string> GetMap()
    {
		List<string> map = [];

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
			var text = File.ReadAllLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH));

			foreach (var t in text)
            {
				map.Add(t);
            }

            return map;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}