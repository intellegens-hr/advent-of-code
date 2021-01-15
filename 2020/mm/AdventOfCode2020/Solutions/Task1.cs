using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020.Solutions
{
    public class Task1
    {
        /// <summary>
        /// Method to be run to get the first part of the solution.
        /// Find two numbers in list that give a sum of 2020
        /// </summary>
        /// <returns>Product of those two numbers</returns>
        public static int FirstPart(string filename)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Inputs\\", filename);
            List<int> numbersSoFar = new List<int>();

            // Read the file and display it line by line.  
            string line;
            StreamReader file = new System.IO.StreamReader(fullPath);
            while ((line = file.ReadLine()) != null)
            {
                var currentNumber = Convert.ToInt32(line);
                foreach (var number in numbersSoFar)
                {
                    if (number + currentNumber == 2020)
                    {
                        file.Close();
                        return number * currentNumber;
                    } 
                }
                numbersSoFar.Add(currentNumber);
            }

            file.Close();
            throw new Exception("No numbers that provide such sum found!");
        }

        /// <summary>
        /// Method to be run to ge the 2nd part of the solution.
        /// Find three numbers in list that give a sum of 2020
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static int SecondPart(string filename)
        {
            var numbers = ReadInputs.ReadAllInts(filename);

            for (int i = 0; i < numbers.Count; i++)
                for (int j = 0; j < i; j++)
                {
                    if (numbers[i] + numbers[j] > 2020) continue;                   // if two numbers are greater than 2020, no sense trying out the 3rd.
                    for (int k = 0; k < j; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                        {
                            return numbers[i] * numbers[j] * numbers[k];
                        }
                    }
                }

            throw new Exception("No sequence found.");
        }
    }
}
