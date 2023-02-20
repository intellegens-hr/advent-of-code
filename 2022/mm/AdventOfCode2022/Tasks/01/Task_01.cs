using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._01
{
    public class Task_01
    {
        public static int PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\01\PartOne.txt");
            string line = String.Empty;
            int s = 0;
            int max = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (s > max) max = s;
                    s = 0;
                }
                else
                {
                    s += Convert.ToInt32(line);
                }
            }

            return max;
        }

        public static int PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\01\PartOne.txt");
            string line = String.Empty;
            int s = 0;
            int[] max = { 0, 0, 0, 0 };
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (s > max[2])
                    {
                        max[3] = s;
                        Array.Sort(max);
                        Array.Reverse(max);
                    }
                    s = 0;
                }
                else
                {
                    s += Convert.ToInt32(line);
                }
            }

            return max[0] + max[1] + max[2];
        }
    }
}
