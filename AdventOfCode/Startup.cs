Console.WriteLine("Quel jour ?");
var input = Console.ReadLine();

string className = $"AdventOfCode.Day{input}.Day{input}";

Activator.CreateInstance(Type.GetType(className));

