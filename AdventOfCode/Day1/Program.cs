namespace AdventOfCode.Day1;

public class Day1
{
    const string FILEPATH = "Day1\\input.txt";

    public Day1()
    {

        Console.WriteLine(CalculDistanceEntreColonnes());
        Console.WriteLine(CalculSimilariteColonnes());
    }

    private static int CalculDistanceEntreColonnes()
    {
        var (left, right) = GetColumns();

        left.Sort();
        right.Sort();

        int distance = 0;
        for (int i = 0; i < left.Count; i++)
        {
            int difference = left[i] - right[i];
            if (difference < 0)
                difference = -difference;
            distance += difference;
        }

        return distance;
    }

    private static int CalculSimilariteColonnes()
    {
        var (left, right) = GetColumns();

        Dictionary<int, int> rightCounts = [];

        foreach (var num in right)
        {
            if (rightCounts.TryGetValue(num, out int value))
                rightCounts[num] = ++value;
            else
                rightCounts[num] = 1;
        }

        int similarity = 0;

        foreach (var leftNum in left)
            if (rightCounts.TryGetValue(leftNum, out int value))
                similarity += leftNum * value;

        return similarity;
    }

    private static (List<int> left, List<int> right) GetColumns()
    {
        List<int> left = [];
        List<int> right = [];

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                var numbers = line.Split("  ");
                left.Add(int.Parse(numbers[0]));
                right.Add(int.Parse(numbers[1]));
            }

            return (left, right);
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}