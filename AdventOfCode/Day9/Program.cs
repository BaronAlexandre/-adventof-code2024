using System.Text;

namespace AdventOfCode.Day9;

public class Day9
{
    const string FILEPATH = "Day9\\input.txt";

    public Day9()
    {
        Console.WriteLine(CalculDistanceEntreColonnes());
    }

    private static long CalculDistanceEntreColonnes()
    {

        var stringnumber = GetInt();
		stringnumber = "2333133121414131402";
		var ints = stringnumber.Select(c => int.Parse(c.ToString())).ToArray();



		var stringCalculated = "";
		for (var i = 0; i < ints.Length; i++)
		{
			for (var j = 0; j < ints[i]; j++)
			{
				if (i % 2 == 0)
				{
					stringCalculated += i / 2;
				}
				else
				{
					stringCalculated += ".";
				}
			}
		}



		long checksum = 0;



		return checksum;
    }

    private static string GetInt()
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