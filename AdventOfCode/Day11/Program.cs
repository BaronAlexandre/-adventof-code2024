using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day11;

public class Day11
{
    const string FILEPATH = "Day11\\input.txt";

    public Day11()
    {

        Console.WriteLine(CalculDistanceEntreColonnes(GetColumns()));
    }

    private static int CalculDistanceEntreColonnes(string line)
    {
		var ints = line.Split(' ').Select(ulong.Parse).ToList();

		List<ulong> transformedStones = ints;


		for (int i = 0; i < 75; i++)
		{
			transformedStones = Blink(transformedStones);
		}

		return transformedStones.Count();
    }
	static List<ulong> Blink(List<ulong> stones)
	{
		List<ulong> result = [];

		foreach (var stone in stones)
		{
			try
			{
				if (stone == 0)
				{
					result.Add(1);
				}
				else if (stone >= 1 && stone < 10)
				{
					result.Add(stone * (ulong)2024);
				}
				else if (stone.ToString().Count() % 2 == 0)
				{
					string firstPart = stone.ToString().Substring(0, stone.ToString().Length / 2);
					string secondPart = stone.ToString().Substring(stone.ToString().Length / 2);

					result.Add(ulong.Parse(firstPart));
					result.Add(ulong.Parse(secondPart));
				}
				else
				{
					result.Add(stone * 2024);
				}
			}
			catch(Exception e)
			{

			}
		}

		return result;
	}
	private static string GetColumns()
    {
        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                return line;
            }
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
        return null;
    }
}