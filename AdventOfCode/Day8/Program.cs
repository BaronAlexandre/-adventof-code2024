namespace AdventOfCode.Day8
{
    public class Day8
    {
        const string FILEPATH = "Day8\\input.txt";

        public Day8()
        {
            var map = GetMap();
            var antennas = GetMappingByAntenna(map);

            var anti_1 = new HashSet<(int, int)>();
            var anti_2 = new HashSet<(int, int)>();

            foreach (var antenna in antennas)
            {
                var coord = antenna.Value;

                for (var i = 0; i < coord.Count; i++)
                {
                    var antenna1 = coord[i];
                    for (var j = i + 1; j < coord.Count; j++)
                    {
                        var antenna2 = coord[j];

                        var delta = (antenna2.x - antenna1.x, antenna2.y - antenna1.y);

                        Anti1(anti_1, antenna1, antenna2, delta, map.Length, map[0].Length);
                        Anti2(anti_2, antenna1, antenna2, delta, map.Length, map[0].Length);
                    }
                }
            }

            Console.WriteLine(anti_1.Count + " " + anti_2.Count);
        }

        private static void Anti1(HashSet<(int, int)> anti_1, (int x, int y) antenna1, (int x, int y) antenna2, (int, int) delta, int mapLength, int mapWidth)
        {
            // antenne 1
            if (IsOutOfMap((antenna1.x - delta.Item1, antenna1.y - delta.Item2), mapLength, mapWidth))
                anti_1.Add((antenna1.x - delta.Item1, antenna1.y - delta.Item2));

            // antenne 2
            if (IsOutOfMap((antenna2.x + delta.Item1, antenna2.y + delta.Item2), mapLength, mapWidth))
                anti_1.Add((antenna2.x + delta.Item1, antenna2.y + delta.Item2));
        }

        private static void Anti2(HashSet<(int, int)> anti_2, (int x, int y) antenna1, (int x, int y) antenna2, (int, int) delta, int mapLength, int mapWidth)
        {
            var temp = antenna1;
            while (IsOutOfMap((temp.x, temp.y), mapLength, mapWidth))
            {
                anti_2.Add((temp.x, temp.y));
                temp.x -= delta.Item1;
                temp.y -= delta.Item2;
            }

            temp = antenna2;
            while (IsOutOfMap((temp.x, temp.y), mapLength, mapWidth))
            {
                anti_2.Add((temp.x, temp.y));
                temp.x += delta.Item1;
                temp.y += delta.Item2;
            }
        }

        private static Dictionary<char, List<(int x, int y)>> GetMappingByAntenna(string[] map)
        {
            var mapping = new Dictionary<char, List<(int x, int y)>>();

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    char c = map[y][x];
                    if (c != '.')
                    {
                        if (!mapping.ContainsKey(c))
                            mapping[c] = new List<(int x, int y)>();
                        mapping[c].Add((x, y));
                    }
                }
            }

            return mapping;
        }

        static bool IsOutOfMap((int x, int y) pos, int rows, int cols)
        {
            return pos.x >= 0 && pos.x < cols && pos.y >= 0 && pos.y < rows;
        }

        private static string[] GetMap()
        {
            var text = new List<string>();

            if (File.Exists(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
            {
                foreach (var line in File.ReadLines(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, FILEPATH)))
                {
                    text.Add(line);
                }

                return text.ToArray();
            }
            else
            {
                throw new Exception("Le fichier n'existe pas.");
            }
        }
    }
}
