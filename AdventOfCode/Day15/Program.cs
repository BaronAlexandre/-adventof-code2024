namespace AdventOfCode.Day15;

public class Day15
{
	const string FILEPATH = "Day15\\temp2.txt";

	public Day15()
	{
		//Console.WriteLine(Travelling1(GetData()));
		Console.WriteLine(Travelling2(GetData2()));
	}

	private static long Travelling1((string[] map, string moves) data)
	{
		// Pousse les blocs
		void PushBlocks(ref (int xS, int yS) start, string[] map, int x, int y, int dx, int dy)
		{
			int nextX = 0;
			int nextY = 0;

			List<(int x, int y)> boxes = [];

			int currX = x;
			int currY = y;
			while (currX >= 0 && currX < map[0].Length && currY >= 0 && currY < map.Length && map[currY][currX] == 'O')
			{
				boxes.Add((currX, currY));
				currX += dx;
				currY += dy;

				nextX = currX;
				nextY = currY;
			}

			if (map[nextY][nextX] == '.')
			{
				foreach (var box in boxes)
				{
					map[box.y] = map[box.y].Remove(box.x, 1).Insert(box.x, ".");
				}

				foreach (var box in boxes)
				{
					map[box.y + dy] = map[box.y + dy].Remove(box.x + dx, 1).Insert(box.x + dx, "O");
				}

				map[start.yS] = map[start.yS].Remove(start.xS, 1).Insert(start.xS, ".");
				map[y] = map[y].Remove(x, 1).Insert(x, "@");
				start.xS = x;
				start.yS = y;
			}
		}

		// Déplace
		void Move(ref (int xS, int yS) start, string[] map, int newX, int newY, int dx, int dy)
		{
			if (newX >= 0 && newX < map[0].Length && newY >= 0 && newY < map.Length)
			{
				char nextCell = map[newY][newX];

				if (nextCell == '.')
				{
					map[start.yS] = map[start.yS].Remove(start.xS, 1).Insert(start.xS, ".");
					map[newY] = map[newY].Remove(newX, 1).Insert(newX, "@");
					start.xS = newX;
					start.yS = newY;
				}
				else if (nextCell == 'O')
				{
					PushBlocks(ref start, map, newX, newY, dx, dy);
				}
			}
		}

		var map = data.map;

		(int xS, int yS) start = new();

		for (int i = 0; i < map.Length; i++)
		{
			for (int j = 0; j < map[i].Length; j++)
			{
				if (map[i][j] == '@')
				{
					start.xS = j;
					start.yS = i;
					break;
				}
			}
		}

		foreach (var move in data.moves)
		{
			switch (move)
			{
				case '<':
					Move(ref start, data.map, start.xS - 1, start.yS, -1, 0);
					break;
				case '>':
					Move(ref start, data.map, start.xS + 1, start.yS, 1, 0);
					break;
				case '^':
					Move(ref start, data.map, start.xS, start.yS - 1, 0, -1);
					break;
				case 'v':
					Move(ref start, data.map, start.xS, start.yS + 1, 0, 1);
					break;
			}
		}

		PrintMap(data.map, 'o');

		long gpsSum = 0;

		for (int i = 0; i < data.map.Length; i++)
		{
			for (int j = 0; j < data.map[i].Length; j++)
			{
				if (data.map[i][j] == 'O')
				{
					gpsSum += (100 * i) + j;
				}
			}
		}

		return gpsSum;
	}

	private static long Travelling2((string[] map, string moves) data)
	{
		void PushWideBoxes(ref (int xS, int yS) start, string[] map, int x, int y, int dx, int dy)
		{
			int nextX = 0;
			int nextY = 0;

			List<(int x, int y)> boxes = new List<(int, int)>();

			int currX = x;
			int currY = y;
			while (currX >= 0 && currX < map[0].Length && currY >= 0 && currY < map.Length && map[currY][currX] == '[')
			{
				boxes.Add((currX, currY));
				currX += dx;
				currY += dy;

				nextX = currX + 1;
				nextY = currY;
			}

			if (map[nextY][nextX] == '.')
			{
				foreach (var box in boxes)
				{
					map[box.y] = map[box.y].Remove(box.x, 2).Insert(box.x, "..");
				}

				foreach (var box in boxes)
				{
					map[box.y + dy] = map[box.y + dy].Remove(box.x + dx, 2).Insert(box.x + dx, "[]");
				}

				map[start.yS] = map[start.yS].Remove(start.xS, 1).Insert(start.xS, ".");
				map[y] = map[y].Remove(x, 1).Insert(x, "@");
				start.xS = x;
				start.yS = y;
			}
		}
		
		void Move(ref (int xS, int yS) start, string[] map, int newX, int newY, int dx, int dy)
		{
			if (newX >= 0 && newX < map[0].Length && newY >= 0 && newY < map.Length)
			{
				char nextCell = map[newY][newX];

				if (nextCell == '.')
				{
					map[start.yS] = map[start.yS].Remove(start.xS, 1).Insert(start.xS, ".");
					map[newY] = map[newY].Remove(newX, 1).Insert(newX, "@");
					start.xS = newX;
					start.yS = newY;
				}
				else if (nextCell == '[')
				{
					PushWideBoxes(ref start, map, newX, newY, dx, dy);
				}
				else if (nextCell == ']')
				{
					PushWideBoxes(ref start, map, newX, newY, dx, dy);
				}
			}
		}

		var map = data.map;

		(int xS, int yS) start = new();

		for (int i = 0; i < map.Length; i++)
		{
			for (int j = 0; j < map[i].Length; j++)
			{
				if (map[i][j] == '@')
				{
					start.xS = j;
					start.yS = i;
					break;
				}
			}
		}
		PrintMap(data.map, 'b');

		foreach (var move in data.moves)
		{
			switch (move)
			{
				case '<':
					Move(ref start, data.map, start.xS - 1, start.yS, -1, 0);
					break;
				case '>':
					Move(ref start, data.map, start.xS + 1, start.yS, 1, 0);
					break;
				case '^':
					Move(ref start, data.map, start.xS, start.yS - 1, 0, -1);
					break;
				case 'v':
					Move(ref start, data.map, start.xS, start.yS + 1, 0, 1);
					break;
			}
			PrintMap(data.map, move);
		}

		long gpsSum = 0;

		for (int i = 0; i < data.map.Length; i++)
		{
			for (int j = 0; j < data.map[i].Length; j++)
			{
				if (data.map[i][j] == '[')
				{
					gpsSum += (100 * i) + j;
				}
			}
		}

		return gpsSum;
	}


	private static void PrintMap(string[] map, char move)
	{
		Console.WriteLine(move);
		for (int i = 0; i < map.Length; i++)
		{
			for (int j = 0; j < map[0].Length; j++)
			{
				Console.Write(map[i][j]);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	private static (string[], string) GetData()
	{
		string moves = "";
		bool ismap = true;
		List<string> list = new List<string>();

		if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
		{
			foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
			{
				if (line == "")
				{
					ismap = false;
				}
				if (!ismap)
				{
					moves += line;
				}
				else
				{
					list.Add(line);
				}
			}

			return (list.ToArray(), moves);
		}
		else
		{
			throw new Exception("Le fichier n'existe pas.");
		}
	}

	private static (string[], string) GetData2()
	{
		string moves = "";
		bool ismap = true;
		List<string> list = new List<string>();

		if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
		{
			foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
			{
				if (line == "")
				{
					ismap = false;
				}
				if (!ismap)
				{
					moves += line;
				}
				else
				{
					list.Add(line.Replace("O", "[]")
						.Replace("#", "##")
						.Replace(".", "..")
						.Replace("@", "@."));
				}
			}

			return (list.ToArray(), moves);
		}
		else
		{
			throw new Exception("Le fichier n'existe pas.");
		}
	}
}
