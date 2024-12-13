int day = DateTime.Now.Day;

if (DateTime.Now.Month != 12)
{
    Console.WriteLine("Quel jour ?");
    day = int.Parse(Console.ReadLine());
}

string className = $"AdventOfCode.Day{day}.Day{day}";

Activator.CreateInstance(Type.GetType(className));
