namespace MyNamespace;

public class Day2
{
	const string FILEPATH = "Day2\\input.txt";

	public Day2()
	{
		Console.WriteLine(GetStrictSafeReport());
		Console.WriteLine(GetLaxistSafeReport());
	}

	private static int GetStrictSafeReport()
	{
		var reports = GetReports();
		var safereports = 0;

		foreach (var report in reports)
			if (IsSafe(report))
				safereports++;

		return safereports;
	}

	private static int GetLaxistSafeReport()
	{
		var reports = GetReports();
		var safereports = 0;

		foreach (var report in reports)
			if (IsSafe(report))
				safereports++;
			else
			{
				for (int i = 0; i < report.Count; i++)
				{
					var modifiedReport = new List<int>(report);
					modifiedReport.RemoveAt(i);

					if (IsSafe(modifiedReport))
					{
						safereports++;
						break;
					}
				}
			}

		return safereports;
	}

	private static bool IsSafe(List<int> report)
	{
		for (int i = 1; i < report.Count; i++)
			if (!(Math.Abs(report[i] - report[i - 1]) < 4 && Math.Abs(report[i] - report[i - 1]) > 0 && report[i] > report[i - 1] != report[0] > report[1]))
				return false;

		return true;
	}

	private static List<List<int>> GetReports()
	{
		List<List<int>> reports = [];

		if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
		{
			foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
			{
				var report = line.Split(" ").Select(int.Parse).ToList();
				reports.Add(report);
			}

			return (reports);
		}
		else
		{
			throw new Exception("Le fichier n'existe pas.");
		}
	}
}