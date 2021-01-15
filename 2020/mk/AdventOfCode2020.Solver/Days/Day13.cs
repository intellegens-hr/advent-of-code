using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day13 : PuzzleDay<long>
    {
        public override int Day => 13;


        public override long First()
        {
            var input = GetInputLines().ToArray();

            var depart = Int32.Parse(input[0]);
            var times = input[1].Split(',').Where(a => a != "x").Select(a => Int32.Parse(a));

            var minDiff = int.MaxValue;
            var minId = 0;

            foreach (var time in times)
            {
                var ceil = (int)Math.Ceiling((double)depart/time);
                var diff = ceil * time - depart;

                if (diff < minDiff)
                {
                    minDiff = diff;
                    minId = time;
                }
            }

            return minDiff * minId;
        }

        public override long Second()
        {
            var input = GetInputLines().ToArray();

            //var depart = Int32.Parse(input[0]);
            var increments = input.Last().Split(',').Select((item, idx) => (item, idx)).Where(a => a.item != "x").Select(a => (long.Parse(a.item), a.idx));

            long i = 0;
            long increment = increments.First().Item1;
            var incrementsToTake = 1;

            while (true)
            {
                if (increments.Take(incrementsToTake).All(a => (i + a.idx) % a.Item1 == 0))
                {
                    if (incrementsToTake == increments.Count())
                        return i;

                    Console.WriteLine(increment);
                    increment = increments.Take(incrementsToTake).Select(a => a.Item1).Aggregate((a, b) => a * b);
                    incrementsToTake++;
                }
                i += increment;
            }

            return -1;

            //var last = 0;
            //for (int i = 0; i < 1000; i+=7)
            //{
            //    if (i % 7 == 0 && (i + 1) % 13 == 0)
            //    {
            //        Console.WriteLine(i);
            //        Console.WriteLine(i - last);
            //        last = i;
            //        Console.WriteLine();
            //    }
            //}
        }
    }
}
