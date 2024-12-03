using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3
{
    const string FILEPATH = "Day3\\input.txt";

    public Day3()
    {
        Console.WriteLine(GetSimpleMultiplies(new(@"mul\((\d+),(\d+)\)")));
        Console.WriteLine(GetComplexMultiplies(new(@"(do\(\))|(don't\(\))|mul\((\d+),(\d+)\)")));
    }

    private static int GetSimpleMultiplies(Regex regex)
    {
        var result = 0;

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
            var text = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH));

            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                int num1 = int.Parse(match.Groups[1].Value);
                int num2 = int.Parse(match.Groups[2].Value);
                result += num1 * num2;
            }
            return result;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }

    private static int GetComplexMultiplies(string regex)
    {
        var result = 0;

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
            var text = File.ReadAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH));

            bool actif = true;

            foreach (Match match in Regex.Matches(text, regex))
            {
                if (!actif && match.Groups[1].Success)
                    actif = true;
                else if (actif && match.Groups[2].Success)
                    actif = false;
                else if (match.Groups[3].Success && match.Groups[4].Success && actif)
                {
                    int num1 = int.Parse(match.Groups[3].Value);
                    int num2 = int.Parse(match.Groups[4].Value);
                    result += num1 * num2;
                }
            }

            return result;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}