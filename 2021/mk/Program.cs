using AdventOfCode2021.Days;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021
{
    enum Direction
    {
        forward,
        up,
        down,
        back
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Day25().First());
            Console.WriteLine(new Day25().Second());
            Console.ReadLine();
        
            }

    }
}
