using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task9
{
    public class Task9
    {
        public static Int64 FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllInt64s(ReadInputs.GetFullPath(filename));
            var preambleLength = 25;

            var currentSums = CalculateInitialSums(lines, preambleLength);

            for (var i = preambleLength; i < lines.Count; i++)
            {
                //foreach (var item in currentSums)
                //{
                //    Console.Write(string.Format("{0} ", item));
                //}
                //Console.WriteLine(string.Format("comparing with {0}", lines[i]));

                if (!currentSums.Contains(lines[i]))
                {
                    return lines[i];
                } else
                {
                    currentSums = RecalculateCurrentSums(currentSums, lines, i, preambleLength);
                }
                
            }

            throw new Exception("We should never get here");
            
        }

        public static Int64 SecondPart(string filename)
        {
            var desiredSum = FirstPart(filename);
            var lines = ReadInputs.ReadAllInt64s(ReadInputs.GetFullPath(filename));

            for (var i = 0; i < lines.Count; i++)
            {
                Int64 sum = lines[i];
                var j = i + 1;
                Int64 min = lines[i];
                Int64 max = lines[i];
                while (sum < desiredSum && j  < lines.Count)
                {
                    if (min > lines[j]) min = lines[j];
                    if (max < lines[j]) max = lines[j];
                    sum += lines[j];
                    j++;
                }

                if (sum == desiredSum)
                {
                    return min + max;
                }
            }

            throw new Exception("We should never get here");
        }

        private static List<Int64> RecalculateCurrentSums(List<long> currentSums, List<Int64> lines, int currentIndex, int preambleLength)
        {
            var newSums = new List<Int64>();
            var newNumber = lines[currentIndex];
            var loopIndex = 0;
            for (var i = 0; i < preambleLength; i++)
            {
                for (var j = i + 1; j< preambleLength; j++)
                {
                    if (i > 0)
                    {
                        newSums.Add(currentSums[loopIndex]);
                        
                    }
                    loopIndex++;

                }

                // Insert new element
                if (i != 0)
                {
                    newSums.Add(lines[currentIndex - preambleLength + i] + newNumber);
                }
            }

            return newSums;
        }

        /// <summary>
        /// Method fills the initial list of sums. First N places are sums of 1st element with others, then next N-1 places are sums of 2nd element with others...
        /// This allows us to skip the first N rows and just add the missing sums
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="preambleLength"></param>
        /// <returns></returns>
        private static List<Int64> CalculateInitialSums(List<long> lines, int preambleLength)
        {
            var currentSums = new List<Int64>();

            for (var i = 0; i < preambleLength; i++)
            {
                for (var j = i + 1; j < preambleLength; j++)
                {
                    currentSums.Add(lines[i] + lines[j]);
                }
            }

            return currentSums;
        }
    }
}
