using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day13;

public class Day13
{
    const string FILEPATH = "Day13\\input.txt";

    public Day13()
    {

        Console.WriteLine(GetCost(GetData()));
        Console.WriteLine(GetCost(GetData(true), true));
	}

	private static long GetCost(List<((long, long, long), (long, long, long), (long, long))> machines, bool ispart2 = false)
	{
		var totalCost = 0L;

		// 3 Token pour A et 1 pour B
		// Axe X et Y
		// One prize pour le gagner tu dois etre sur le X et Y
		// Je veux connaitre le minimum de token pour gagner

		// J'ai une liste de machines

		// Part 2 j'ajoute 10000000000000

		var machinum = 0;
		if (!ispart2)
		{
			foreach (var machine in machines)
			{
				machinum++;
				totalCost += GetOneCost(machine, ispart2);
			}
		}
		else
		{
			foreach (var machine in machines)
			{
				// A.Y * B.X - B.Y * A.X
				var m = machine.Item1.Item2 * machine.Item2.Item1 - machine.Item2.Item2 * machine.Item1.Item1;

				// x =(Prize.Y * B.X - Prize.Y * B.Y) / m
				var x = (machine.Item3.Item2 * machine.Item2.Item1 - machine.Item3.Item1 * machine.Item2.Item2) / m;
				// y = (Prize.X - A.X * x) / B.X
				var y = (machine.Item3.Item1 - machine.Item1.Item1 * x) / machine.Item2.Item1;

				// SSi price
				if (machine.Item1.Item1 * x + machine.Item2.Item1 * y == machine.Item3.Item1 &&
					machine.Item1.Item2 * x + machine.Item2.Item2 * y == machine.Item3.Item2)
				{
					// 3 * pour le A
					totalCost += x * 3 + y;
				}
			}
		}

		return totalCost;
	}

	static long GetOneCost(((long, long, long), (long, long, long), (long, long)) machine, bool ispart2)
	{
		var totalcost = 0L;
		var visited = new Dictionary<string, (long, long)>();

		var myqueue = new Queue<(long x, long y, long c, long nbA, long nbB)>();
		myqueue.Enqueue((0, 0, 0, 0, 0));
		visited[$"0,0"] = (0, 0);

		while (myqueue.Count > 0)
		{
			var (x, y, cost, aPresses, bPresses) = myqueue.Dequeue();

			if (x == machine.Item3.Item1 && y == machine.Item3.Item2)
			{
				// Console.WriteLine($"Min : {cost}");
				totalcost += cost;
				break;
			}
			var buttons = new[] { machine.Item1, machine.Item2 };

			foreach (var button in buttons)
			{
				var newX = x + button.Item1;
				var newY = y + button.Item2;
				var newCost = cost + button.Item3;

				var nbna = aPresses + (button.Equals(machine.Item1) ? 1 : 0);
				var nbnb = bPresses + (button.Equals(machine.Item2) ? 1 : 0);

				if (!ispart2)
				{
					if (nbna > 100 || nbnb > 100)
					{
						//Console.WriteLine($"Trop cher déso : {cost}");
						continue;
					}
				}

				var key = $"{newX},{newY}";

				if (!visited.ContainsKey(key) || visited[key].Item1 > newCost
					|| (visited[key].Item1 == newCost && visited[key].Item2 < nbna))
				{
					visited[key] = (newCost, nbna);
					myqueue.Enqueue((newX, newY, newCost, nbna, nbnb));
				}
			}
		}

		return totalcost;
	}

	static List<((long, long, long), (long, long, long), (long, long))> GetData(bool ispart2 = false)
    {
        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
            var i = 0;
            List<((long, long, long), (long, long, long), (long, long))> machines = [];
            ((long, long, long), (long, long, long), (long, long)) machine = new();
			var r1 = new Regex(@".*X.(\d+)");
			var r2 = new Regex(@".*Y.(\d+)");

			foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
			{
                if (string.IsNullOrWhiteSpace(line)) continue;
                i++;
                if (i == 1)
                {
					machine.Item1 = (long.Parse(r1.Match(line).Groups[1].Value), long.Parse(r2.Match(line).Groups[1].Value),3);
                }
                else if (i == 2)
                {
					machine.Item2 = (long.Parse(r1.Match(line).Groups[1].Value), long.Parse(r2.Match(line).Groups[1].Value),1);
				}
				else if (i == 3)
                {
					machine.Item3 = (long.Parse(r1.Match(line).Groups[1].Value), long.Parse(r2.Match(line).Groups[1].Value));
					i = 0;
					if (ispart2)
					{
						machine.Item3.Item1 += 10000000000000L;
						machine.Item3.Item2 += 10000000000000L;
					}
                    machines.Add(machine);
                }

            }
            return machines;
		}
		else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}