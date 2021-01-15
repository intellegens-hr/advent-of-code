using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task10
{
    public class Task10
    {
        public static int FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllInts(ReadInputs.GetFullPath(filename)).OrderBy(l => l).ToList();
            var numOfOnes = 0;
            var numOfThrees = 0;

            var currentVoltage = 0;
            var myVoltage = lines[lines.Count - 1] + 3;

            foreach (var line in lines)
            {
                if (line - currentVoltage == 1)
                {
                    numOfOnes++;
                } else if (line - currentVoltage == 3)
                {
                    numOfThrees++;
                }

                currentVoltage = line;
            }

            return numOfOnes * (numOfThrees + 1);
        }

        public static Int64 SecondPart(string filename)
        {
            var lines = ReadInputs.ReadAllInt64s(ReadInputs.GetFullPath(filename)).OrderBy(l => l).ToList();
            var max = lines[lines.Count - 1];
            // lines.Add(max);

            Int64[] numOfSolutions = new Int64[max+1];

            // Knapsackish algorithm
            for (int i = 0; i< lines.Count; i++)
            {
                Int64 num = lines[i];

                if (num <= 3) numOfSolutions[lines[i]]++;          // If number < 3 then it can be added initially

                for (var j = lines[i]-1; j>0 && j > lines[i]-4; j--)
                {
                    // Check if there exist previous solutions for numbers lines[j] - 3
                    if (numOfSolutions[j] != 0)
                    {
                        numOfSolutions[lines[i]] += numOfSolutions[j];
                    }
                }
            }

            return numOfSolutions[numOfSolutions.Length - 1];
        }
    }
}
