using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day1
{
    internal class Day1
    {
        const string path = "C:\\Users\\nikol\\source\\VisualStudio\\AdventOfCode\\2023\\AdventOfCode2023\\AdventOfCode2023\\Day1\\calibrations2.txt";

        public int SumCalibrations()
        {
            int calibrationsSum = 0;

            // for each line
            foreach (string fileLine in File.ReadLines(path))
            {
                bool foundFirstNumber = false;
                int firstNumber = 0, lastNumber = 0;

                var line = ReplaceNumbers(fileLine);

                // for each character in the line
                for (int i = 0; i < line.Length; i++)
                {
                    var character = line[i];

                    // if it is a number
                    if (char.IsNumber(character))
                    {
                        // convert char to int
                        if (foundFirstNumber == false)
                        {
                            firstNumber = character - '0';
                            foundFirstNumber = true;
                        }

                        lastNumber = character - '0';
                    }
                }

                // combine the numbers
                int lineNumber = firstNumber * 10 + lastNumber;

                // increase sum
                calibrationsSum += lineNumber;
            }

            return calibrationsSum;
        }

        private static string ReplaceNumbers(string line)
        {
            return line.Replace("one", "o1e")
                       .Replace("two", "t2o")
                       .Replace("three", "t3e")
                       .Replace("four", "f4r")
                       .Replace("five", "f5e")
                       .Replace("six", "s6x")
                       .Replace("seven", "s7n")
                       .Replace("eight", "e8t")
                       .Replace("nine", "n9e");
        }
    }
}
