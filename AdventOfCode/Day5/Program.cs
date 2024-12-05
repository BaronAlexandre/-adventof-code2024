using System.Text.RegularExpressions;

namespace AdventOfCode.Day5;

public class Day5
{
    const string FILEPATH = "Day5\\input.txt";

    public Day5()
    {

        Console.WriteLine(SumValidSorting());
        Console.WriteLine(SumInvalidSorting());
    }

	private static int SumValidSorting()
	{
		var (pageOrdering, listsPageNumbers) = GetData();

		int result = 0;

		foreach (var pages in listsPageNumbers)
			if (IsValidSorting(pages, pageOrdering)) result += pages[pages.Count / 2];

		return result;
	}

	private static bool IsValidSorting(List<int> pages, List<Tuple<int, int>> pageOrdering)
	{
		foreach (var (before, after) in pageOrdering)
		{
			if (pages.Contains(before) && pages.Contains(after) 
				&& pages.FindIndex(p => p == before) > pages.FindIndex(p => p == after))
			{
				return false;
			}
		}

		return true;
	}

	private static int SumInvalidSorting()
	{
		var (pageOrdering, listsPageNumbers) = GetData();

		int result = 0;

		foreach (var pages in listsPageNumbers)
		{
			if (!IsValidSorting(pages, pageOrdering))
			{
				var sortedPages = Sort(pages, pageOrdering);

				result += sortedPages[sortedPages.Count / 2];
			}
		}

		return result;
	}

	private static List<int> Sort(List<int> pages, List<Tuple<int, int>> pageOrdering)
	{
		var result = new List<int>();
		var remaining = new HashSet<int>(pages);

		while (remaining.Count > 0)
		{
			var pageSansDependance = remaining.First(page =>
				pageOrdering.Where(pageOrder => pageOrder.Item2 == page)
					.All(pageOrder => !remaining.Contains(pageOrder.Item1)));


			result.Add(pageSansDependance);
			remaining.Remove(pageSansDependance);
		}

		return result;
	}


	private static (List<Tuple<int, int>> pageOrdering, List<List<int>> listsPageNumbers) GetData()
    {
        var tuple = new List<Tuple<int, int>>();
        var ints = new List<List<int>>();

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                var list = new List<int>();
                var numbers = line.Split("|");
				if (numbers.Length == 2)
				{
					tuple.Add(new(int.Parse(numbers[0]), int.Parse(numbers[1])));
				}
				else
				{
					var n2 = line.Split(",");
					if (n2.Length > 1)
					{
						ints.Add(n2.Select(int.Parse).ToList());
					}
				}
			}

            return (tuple, ints);
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}