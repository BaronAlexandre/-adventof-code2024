namespace AdventOfCode.Day4;

public class Day4
{
    const string FILEPATH = "Day4\\input.txt";

    public Day4()
    {
        //Console.WriteLine(GetXMAS());
        Console.WriteLine(GetX_MAS());
    }

	private static int GetXMAS()
	{
		var text = GetArray();

		var result = 0;

		for (int i = 0; i < text.Count; i++)
		{
			for (int j = 0; j < text[i].Count; j++)
			{
				if (FindXMAS(text, i, j, 0, 1)) result++;
				if (FindXMAS(text, i, j, 0, -1)) result++;
				if (FindXMAS(text, i, j, 1, 0)) result++;
				if (FindXMAS(text, i, j, -1, 0)) result++;
				if (FindXMAS(text, i, j, 1, 1)) result++;
				if (FindXMAS(text, i, j, 1, -1)) result++;
				if (FindXMAS(text, i, j, -1, 1)) result++;
				if (FindXMAS(text, i, j, -1, -1)) result++;
			}
		}

		return result;
	}
	static bool FindXMAS(List<List<char>> grid, int startRow, int startCol, int directionX, int directionY)
	{
		int row = startRow;
		int col = startCol;
		var searched = "XMAS";

		var size = 0;

		// Pour chaque lettre de XMAS
		for (int i = 0; i < searched.Length; i++)
		{
			// Check si hors grille
			if (row < 0 || row >= grid.Count || col < 0 || col >= grid[row].Count)
				return false;

			// Check si la lettre correspond
			if (grid[row][col] == searched[i])
				size++;

			row += directionX;
			col += directionY;
		}

		return size == 4;
	}

	private static int GetX_MAS()
	{
		var text = GetArray();

		var result = 0;
		var resultOk = 0;

		for (int i = 1; i < text.Count-1; i++)
		{
			for (int j = 1; j < text[i].Count-1; j++)
			{
				if (($"{text[i - 1][j - 1]}{text[i][j]}{text[i + 1][j + 1]}" == "MAS" || $"{text[i - 1][j - 1]}{text[i][j]}{text[i + 1][j + 1]}" == "SAM") &&
					($"{text[i - 1][j + 1]}{text[i][j]}{text[i + 1][j - 1]}" == "MAS"
					 || $"{text[i - 1][j + 1]}{text[i][j]}{text[i + 1][j - 1]}" == "SAM"))
				{
					resultOk++;
				}
				if (text[i][j] == 'A')
				{
					var topLeftLetter = text[i - 1][j - 1];
					var topRightLetter = text[i - 1][j + 1];
					var bottomLeftLetter = text[i + 1][j - 1];
					var bottomRightLetter = text[i + 1][j + 1];

					var isX = (topLeftLetter == bottomLeftLetter && topRightLetter == bottomRightLetter && topLeftLetter != topRightLetter) ||
						(topLeftLetter == topRightLetter && bottomLeftLetter == bottomRightLetter && bottomLeftLetter != topLeftLetter);

					if (isX)
					{
						result++;
					}
				}
			}
		}
		Console.WriteLine(resultOk);
		Console.WriteLine(result);
		return result;
	}
	static bool FindX_MAS(List<List<char>> grid, int row, int col)
	{
		var topLeftLetter = grid[row - 1][col - 1];
		var topRightLetter = grid[row - 1][col + 1];
		var bottomLeftLetter = grid[row + 1][col - 1];
		var bottomRightLetter = grid[row + 1][col + 1];

		var isX = (topLeftLetter == bottomLeftLetter && topRightLetter == bottomRightLetter) ||
			(topLeftLetter == topRightLetter && bottomLeftLetter == bottomRightLetter);

		return isX;
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