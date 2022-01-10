using AdventOfCode2021.Helpers;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task07
    {
        public static long Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task7\\Input.txt");
            int[] crabs = reader.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
            reader.Close();

            var minIndex = 0;
            var maxIndex = crabs.Max() + 1;
            int[] solutions = new int[maxIndex];

            for (int i = 0; i < crabs.Length; i++)
            {
                int crab = crabs[i];
                bool lowerBroken = false;
                bool higherBroken = false;
                int j = 0;
                while (lowerBroken == false || higherBroken == false)
                {
                    if (crab + j < maxIndex) solutions[crab + j] += j; else higherBroken = true;
                    if (crab - j >= minIndex) solutions[crab - j] += j; else lowerBroken = true;

                    j++;
                }
            }

            long min = solutions.Min();

            return min;
        }

        public static long Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task7\\Input.txt");
            int[] crabs = reader.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
            reader.Close();

            var minIndex = 0;
            var maxIndex = crabs.Max() + 1;
            int[] solutions = new int[maxIndex];

            for (int i = 0; i < crabs.Length; i++)
            {
                int crab = crabs[i];
                int stepIncrement = 0;
                bool lowerBroken = false;
                bool higherBroken = false;
                int j = 1;
                while (lowerBroken == false || higherBroken == false)
                {
                    if (crab + j < maxIndex) solutions[crab + j] += j + stepIncrement; else higherBroken = true;
                    if (crab - j >= minIndex) solutions[crab - j] += j + stepIncrement; else lowerBroken = true;

                    stepIncrement += j;
                    j++;
                }
            }

            long min = solutions.Min();

            return min;
        }
    }
}
