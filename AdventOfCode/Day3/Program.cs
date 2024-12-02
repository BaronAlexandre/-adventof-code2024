namespace AdventOfCode.Day3;

public class Day3
{
    const string FILEPATH = "Day3\\input.txt";

    public Day3()
    {
        Console.WriteLine("Day3");
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

            return reports;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}