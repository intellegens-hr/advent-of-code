using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task15
{
    public class Task15
    {
        public static int FirstPart(int[] sequence)
        {
            // Initialize dictionary from data
            Dictionary<int, int> spokenOn = new Dictionary<int, int>();
            for (int i = 0; i < sequence.Length; i++)
            {
                UpdateDictionary(spokenOn, sequence[i], i);
            }

            int iteration = sequence.Length;
            int lastSpoken = sequence[sequence.Length - 1];
            int nextSpoken = CalculateInitialNextSpoken(sequence);
            while (iteration < 30000000 - 1)
            {
                // if new number
                if (!spokenOn.ContainsKey(nextSpoken))
                {
                    spokenOn[nextSpoken] = iteration;
                    nextSpoken = 0;
                } 
                // existing number
                else
                {
                    var tempNextSpoken = iteration - spokenOn[nextSpoken];
                    spokenOn[nextSpoken] = iteration;
                    nextSpoken = tempNextSpoken;
                }
                
                iteration++;
                // Console.WriteLine("Iteration " + iteration + ": " + nextSpoken);
            }

            return nextSpoken;
        }

        private static int CalculateInitialNextSpoken(int[] sequence)
        {
            // in all given examples this is always 0
            // probably should write an algorithm for this
            return 0;
        }

        private static void UpdateDictionary(Dictionary<int,int> spokenOn, int number, int step)
        {
            if (!spokenOn.ContainsKey(number))
            {
                spokenOn[number] = step;
            }
            else
            {
                spokenOn.Add(number, step);
            }
        }
    }
}
