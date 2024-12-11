namespace AdventOfCode.Day11;

public class Day11
{
    const string FILEPATH = "Day11\\input.txt";

    public Day11()
    {

        Console.WriteLine(GetBlinks(GetStones(), 25));
        Console.WriteLine(GetBlinks(GetStones(), 75));
    }

    private static decimal GetBlinks(string line, int nbblinks)
    {
        var ints = line.Split(' ').Select(ulong.Parse).ToList();

        Dictionary<ulong, ulong> transformedStones = ints.GroupBy(i => i).ToDictionary(i => i.Key, i => (ulong)i.Count());


        for (int i = 0; i < nbblinks; i++)
        {
            transformedStones = Blink(transformedStones);
        }

        ulong r = 0;
        foreach (var kvp in transformedStones)
        {
            r += kvp.Value;
        }

        return r;
    }
    static Dictionary<ulong, ulong> Blink(Dictionary<ulong, ulong> stones)
    {
        Dictionary<ulong, ulong> result = [];

        foreach (var stone in stones)
        {
            try
            {
                if (stone.Key == 0)
                {
                    if (result.ContainsKey(1))
                        result[1] += stone.Value;
                    else
                        result[1] = stone.Value;
                }
                else if (stone.Key >= 1 && stone.Key < 10)
                {
                    if (result.ContainsKey(stone.Key * 2024))
                        result[stone.Key * 2024] += stone.Value;
                    else
                        result[stone.Key * 2024] = stone.Value;
                }
                else if (stone.Key.ToString().Count() % 2 == 0)
                {
                    var part1 = ulong.Parse(stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2));
                    var part2 = ulong.Parse(stone.Key.ToString().Substring(stone.Key.ToString().Length / 2));

                    if (result.ContainsKey(part1))
                        result[part1] += stone.Value;
                    else
                        result[part1] = stone.Value;

                    if (result.ContainsKey(part2))
                        result[part2] += stone.Value;
                    else
                        result[part2] = stone.Value;
                }
                else
                {
                    if (result.ContainsKey(stone.Key * 2024))
                        result[stone.Key * 2024] += stone.Value;
                    else
                        result[stone.Key * 2024] = stone.Value;
                }
            }
            catch (Exception e)
            {

            }
        }

        return result;
    }
    private static string GetStones()
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