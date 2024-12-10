namespace AdventOfCode.Day9;

public class Day9
{
    const string FILEPATH = "Day9\\input.txt";

    public Day9()
    {
        Console.WriteLine(CalculFragmentation(GetInt()));
        Console.WriteLine(CalculFragemntationByBlock(GetInt()));
    }
    private static long CalculFragmentation(List<int> ints)
    {
        var finalmap = new List<string>();
        var id = 0;
        bool isBlock = true;

        foreach (var num in ints)
        {
            for (int i = 0; i < num; i++)
            {
                finalmap.Add(isBlock ? id.ToString() : ".");
            }
            isBlock = !isBlock;
            if (isBlock)
                id++;
        }

        var left = 0;
        var right = finalmap.Count - 1;
        while (left < right)
        {
            if (finalmap[right] == ".")
            {
                right--;
            }
            else if (finalmap[left] != ".")
            {
                left++;
            }
            else
            {
                finalmap[left] = finalmap[right];
                finalmap[right] = ".";
                left++;
                right--;
            }
        }

        return finalmap
            .Where(number => number != ".")
            .Select((numer, index) => long.Parse(numer) * index)
            .Sum();
    }

    private static long CalculFragemntationByBlock(List<int> ints)
    {
        var totalValue = 0L;
        int totalLength = ints.Count;

        var diskValues = ints.ToArray();

        var diskCapacities = new List<int>[totalLength / 2];
        for (int i = 0; i < totalLength / 2; i++)
            diskCapacities[i] = [];

        for (int diskIndex = totalLength - 1; diskIndex > 0; diskIndex -= 2)
            for (int spaceIndex = 1; spaceIndex < diskIndex; spaceIndex += 2)
                if (diskValues[spaceIndex] >= diskValues[diskIndex])
                {
                    diskCapacities[spaceIndex / 2].Add(diskIndex);
                    diskValues[spaceIndex] -= diskValues[diskIndex];
                    diskValues[diskIndex] = -1;
                    break;
                }

        int spaceCounter = 0;
        for (int index = 0; index < totalLength; index++)
        {
            if (index % 2 != 0)
            {
                foreach (var diskIndex in diskCapacities[index / 2])
                {
                    var diskSpace = ints[diskIndex];
                    for (int k = 0; k < diskSpace; k++)
                        totalValue += (diskIndex / 2) * spaceCounter++;
                }
                spaceCounter += diskValues[index];
            }
            else
            {
                var diskSpace = ints[index];
                if (diskValues[index] == -1)
                    spaceCounter += diskSpace;
                else
                    for (int k = 0; k < diskSpace; k++)
                        totalValue += (index / 2) * spaceCounter++;
            }
        }

        return totalValue;
    }


    private static List<int> GetInt()
    {
        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {

            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                return line.Select(i => int.Parse(i.ToString())).ToList();
            }
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
        return null;
    }

}