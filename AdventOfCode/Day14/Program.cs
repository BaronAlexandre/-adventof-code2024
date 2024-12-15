namespace AdventOfCode.Day14;

public class Day14
{
	const string FILEPATH = "Day14\\input.txt";

	private const int Width = 101;
	private const int Height = 103;
	static int Loop = 100;
	static int LoopSaping = 0;

	public Day14()
	{
		Console.WriteLine(Part1(GetData()));
		Console.WriteLine(Part2(GetData()));
	}

	private static long Part2(List<((int x, int y) p, (int x, int y) v)> list)
	{
		var map = list;
		var endmap = map.Select(d => d.p).ToList();

		while (true)
		{
			for (int j = 0; j < map.Count; j++)
			{
				var item = map[j];

				item.p.x = (map[j].p.x + map[j].v.x + Width) % Width;
				item.p.y = (map[j].p.y + map[j].v.y + Height) % Height;

				map[j] = item;
			}
			endmap = map.Select(d => d.p).ToList();
			LoopSaping++;
			if (endmap.ToHashSet().Count == map.Count)
			{
				PrintMap(endmap);
				break;
			}
		}

		return LoopSaping;
	}

	static int Part1(List<((int x, int y) p, (int x, int y) v)> list)
	{
		var map = list;
		var endmap = map.Select(d => d.p).ToList();

		for (int i = 0; i < Loop; i++)
		{
			for (int j = 0; j < map.Count; j++)
			{
				var item = map[j];

				item.p.x = (map[j].p.x + map[j].v.x + Width) % Width;
				item.p.y = (map[j].p.y + map[j].v.y + Height) % Height;

				map[j] = item;
			}
		}

		var ul = 0;
		var ur = 0;
		var bl = 0;
		var br = 0;

		int midX = Width / 2;
		int midY = Height / 2;

		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				if (x == midX || y == midY) continue;

				if (x < midX && y < midY) ul += endmap.Count(i => i.x == x && i.y == y);
				if (x >= midX && y < midY) ur += endmap.Count(i => i.x == x && i.y == y);
				if (x < midX && y >= midY) bl += endmap.Count(i => i.x == x && i.y == y);
				if (x >= midX && y >= midY) br += endmap.Count(i => i.x == x && i.y == y);

			}
		}

		int result = ul * ur * bl * br;

		return result;
	}

	private static List<((int, int), (int, int))> GetData()
	{
		var pl = new List<((int, int), (int, int))>();

		if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
		{
			foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
			{
				var l = line.Split(" ");
				var p = l[0].Replace("p=", "").Split(",");
				var v = l[1].Replace("v=", "").Split(",");

				pl.Add(((int.Parse(p[0]), int.Parse(p[1])), (int.Parse(v[0]), int.Parse(v[1]))));
			}

			return pl;
		}
		else
		{
			throw new Exception("Le fichier n'existe pas.");
		}
	}

	private static void PrintMap(List<(int x, int y)> endmap)
	{

		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
				if (endmap.Any(yy => yy.x == j && yy.y == i))
				{
					Console.Write(endmap.Count(yy => yy.x == j && yy.y == i));
				}
				else
				{
					Console.Write(".");
				}
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}
}