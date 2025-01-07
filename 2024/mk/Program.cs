// See https://aka.ms/new-console-template for more information

using AdventOfCode2024;
using AdventOfCode2024.Days;
using System.Numerics;

Run<Day17, string>();

static void Run<T, M>() where T : Puzzle<M>, new()
{
    var input = File.ReadAllText($@"D:\Projects\adventofcode2024\AdventOfCode2024\Inputs\{typeof(T).Name}\input.txt");
    var example = File.ReadAllText($@"D:\Projects\adventofcode2024\AdventOfCode2024\Inputs\{typeof(T).Name}\example2.txt");
    //Console.WriteLine(new T().First(example));
    Console.WriteLine("---------");
    //Console.WriteLine(new T().Second(example));
    //Console.WriteLine(new T().First(input));
    Console.WriteLine(new T().Second(input));
    //Console.WriteLine(new Day17().Second());
    Console.Read();
}

