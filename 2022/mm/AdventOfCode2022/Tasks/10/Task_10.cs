using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._10
{
    public class Task_10
    {
        public static Dictionary<string, int> FillCycleCosts()
        {
            var dict = new Dictionary<string, int>();
            dict.Add("noop", 1);
            dict.Add("addx", 2);
            return dict;
        }
        public static long PartOne()
        {


            using var reader = new StreamReader(@"..\..\..\Tasks\10\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\10\Example.txt");
            string line = String.Empty;
            int cycles = 0;
            var cycleCosts = FillCycleCosts();
            int nextCycle = 20;
            int x = 1;
            long s = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("noop"))
                {
                    if (cycles + cycleCosts["noop"] >= nextCycle)
                    {
                        s += (x * nextCycle);
                        nextCycle += 40;
                    }
                    cycles += cycleCosts["noop"];
                }
                else if (line.StartsWith("addx"))
                {
                    var parts = line.Split(' ');
                    var value = Convert.ToInt32(parts[1]);

                    if (cycles + cycleCosts["addx"] >= nextCycle)
                    {
                        s += (x * nextCycle);
                        nextCycle += 40;
                    }
                    cycles += cycleCosts["addx"];
                    x += value;
                }
            }

            return s;
        }

        public static long PartTwo()
        {
            return 2;
        }
    }
}
