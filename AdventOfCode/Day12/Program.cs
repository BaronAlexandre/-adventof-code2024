namespace AdventOfCode.Day12;

public class Day12
{
    const string FILEPATH = "Day12\\input.txt";
	static int rows, cols;
	static char[,] map;
	static bool[,] visited;

	public Day12()
    {

        Console.WriteLine(GetFences(GetMap()));
    }

    private static decimal GetFences(string[] input)
    {
		rows = input.Length;
		cols = input[0].Length;

		map = new char[rows, cols];
		visited = new bool[rows, cols];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				map[i, j] = input[i][j];
			}
		}

		int totalPrice = 0;

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (!visited[i, j])
				{
					var region = FloodFill(i, j);
					int area = region.Item1;
					int perimeter = region.Item2;
					totalPrice += area * perimeter;
				}
			}
		}
		return totalPrice;
    }
	static Tuple<int, int> FloodFill(int x, int y)
	{
		int[] dx = [-1, 1, 0, 0];
		int[] dy = [0, 0, -1, 1];

		char plantType = map[x, y];
		int area = 0;
		int perimeter = 0;

		Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
		stack.Push(Tuple.Create(x, y));
		visited[x, y] = true;

		while (stack.Count > 0)
		{
			var current = stack.Pop();
			int cx = current.Item1, cy = current.Item2;
			area++;

			for (int i = 0; i < 4; i++)
			{
				int nx = cx + dx[i], ny = cy + dy[i];

				if (nx < 0 || ny < 0 || nx >= rows || ny >= cols || map[nx, ny] != plantType)
				{
					perimeter++;
				}
				else if (!visited[nx, ny]) 
				{
					visited[nx, ny] = true;
					stack.Push(Tuple.Create(nx, ny));
				}
			}
		}

		return Tuple.Create(area, perimeter);
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