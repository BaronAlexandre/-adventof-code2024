namespace AdventOfCode.Day4;

public class Day4
{
    const string FILEPATH = "Day4\\input.txt";

    public Day4()
    {
        Console.WriteLine(GetX_MAS());
    }

    private static int GetX_MAS()
    {
        var text = GetArray();

        var result = 0;

        for (var i = 1; i < text.Count - 1; i++)
            for (var j = 1; j < text[i].Count - 1; j++)
                if (text[i][j] == 'A' && FindX_MAS(text, i, j)) result++;

        return result;
    }

    static bool FindX_MAS(List<List<char>> text, int row, int col)
    {
        var topLeftLetter = text[row - 1][col - 1];
        var topRightLetter = text[row - 1][col + 1];
        var bottomLeftLetter = text[row + 1][col - 1];
        var bottomRightLetter = text[row + 1][col + 1];

        // Check les lettres sont M ou S
        return (($"{topLeftLetter}A{bottomRightLetter}" == "MAS" || $"{topLeftLetter}A{bottomRightLetter}" == "SAM")
            && ($"{topRightLetter}A{bottomLeftLetter}" == "MAS" || $"{topRightLetter}A{bottomLeftLetter}" == "SAM"));
    }

    private static List<List<char>> GetArray()
    {
        List<List<char>> xmas = [];

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
			var text = File.ReadAllLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH));

			foreach (var t in text)
            {
                var newline = new List<char>();
                foreach (var c in t)
                {
                    newline.Add(c);
				}
                xmas.Add(newline);
            }

            return xmas;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}