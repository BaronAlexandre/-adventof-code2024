string filePath = "C:\\Users\\Alexandre\\Documents\\Developpement\\AdventOfCode\\AdventOfCode\\Day1\\input.txt";

Console.WriteLine(CalculDistanceEntreColonnes(filePath));
Console.WriteLine(CalculSimilariteColonnes(filePath));

static int CalculDistanceEntreColonnes(string filePath)
{
    var (left, right) = GetColumns(filePath);

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


static int CalculSimilariteColonnes(string filePath)
{
    var (left, right) = GetColumns(filePath);

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

static (List<int> left, List<int> right) GetColumns(string filePath)
{
    List<int> left = [];
    List<int> right = [];

    foreach (var line in File.ReadLines(filePath))
    {
        var numbers = line.Split("  ");
        left.Add(int.Parse(numbers[0]));
        right.Add(int.Parse(numbers[1]));
    }

    return (left, right);
}