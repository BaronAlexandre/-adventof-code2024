namespace AdventOfCode.Day7;

public class Day7
{
    const string FILEPATH = "Day7\\input.txt";

    public Day7()
    {
        Console.WriteLine(GetResultCalculs(GetCalculs(), ['*', '+']));
        Console.WriteLine(GetResultCalculs(GetCalculs(), ['*', '+', '&']));
    }

    private static long GetResultCalculs(List<Tuple<long, List<long>>> calculs, char[] operators)
    {
        long result = 0;

        foreach (var calcul in calculs)
        {
            if (Compute(calcul.Item1, calcul.Item2, operators))
            {
                result += calcul.Item1;
            }
        }

        return result;
    }

    /// <summary>
    /// Retourne si un résultat match
    /// </summary>
    /// <param name="resCalcul"></param>
    /// <param name="numbers"></param>
    /// <returns></returns>
    static bool Compute(long resCalcul, List<long> numbers, char[] operators)
    {
        // Générer toutes les combinaisons d'opérateurs possibles
        var operatorCombinations = GetAllCombinaisonsOperands(operators, numbers.Count - 1);

        // Tester chaque combinaison d'opérateurs
        foreach (var ops in operatorCombinations)
        {
            var result = numbers[0];
            bool valid = true;

            for (int i = 1; i < numbers.Count; i++)
            {
                result = Calcul(result, numbers[i], ops[i - 1]);
                // Si res dépassé on skip
                if (result > resCalcul)
                {
                    valid = false;
                    break;
                }
            }

            // Si le résultat est correct && que tous les chiffres y sont passés
            if (valid && result == resCalcul) return true;
        }

        return false;
    }

    /// <summary>
    /// Génération de la matrice des opérateurs ex ++++ //// **** /+/+ */+/ etc
    /// </summary>
    /// <param name="operators"></param>
    /// <param name="numOperators"></param>
    /// <returns></returns>
    static string[] GetAllCombinaisonsOperands(char[] operators, int numOperators)
    {
        var combins = (int)Math.Pow(operators.Length, numOperators);
        var combinations = new string[combins];

        for (int i = 0; i < combins; i++)
        {
            char[] combin = new char[numOperators];

            for (int j = 0; j < numOperators; j++)
            {
                combin[j] = operators[(i / (int)Math.Pow(operators.Length, numOperators - j - 1)) % operators.Length];
            }

            combinations[i] = new string(combin);
        }

        return combinations;
    }

    private static long Calcul(long n1, long n2, char o)
    {
        return o switch
        {
            '+' => n1 + n2,
            '*' => n1 * n2,
            '/' => n1 / n2,
            '&' => long.Parse($"{n1}{n2}"),
            _ => throw new Exception(),
        };
    }

    private static List<Tuple<long, List<long>>> GetCalculs()
    {
        List<Tuple<long, List<long>>> reports = new List<Tuple<long, List<long>>>();

        if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
        {
            foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                var splittedline = line.Split(":");
                reports.Add(new(long.Parse(splittedline.FirstOrDefault()), splittedline[1].Trim().Split(" ").Select(long.Parse).ToList()));
            }

            return reports;
        }
        else
        {
            throw new Exception("Le fichier n'existe pas.");
        }
    }
}