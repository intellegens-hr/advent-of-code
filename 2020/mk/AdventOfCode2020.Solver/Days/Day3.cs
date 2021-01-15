using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day3 : PuzzleDay<long>
    {
        public override int Day => 3;

        public override long First()
        {
            var input = GetInputLines();
            int trees = CheckTreesForSteps(3, 1, input);

            return trees;
        }

        int CheckTreesForSteps(int right, int down, string[] input)
        {
            int x = 0, y = 0, trees = 0;

            while (y < input.Length)
            {
                if (input[y][x] == '#')
                    trees++;

                x += right;
                x %= input[0].Length;

                y += down;
            }

            Console.WriteLine($"{right}, {down}, {trees}");
            return trees;
        }

        public override long Second()
        {
            var input = GetInputLines();
            var slopes = new (int,int)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            return slopes.Aggregate((long)1, (factor, el) => factor * CheckTreesForSteps(el.Item1, el.Item2, input));
        }
    }
}
