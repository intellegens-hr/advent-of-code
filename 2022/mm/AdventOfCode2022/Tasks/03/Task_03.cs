using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._03
{
    public class Task_03
    {
        private static int GetPriority(char x)
        {
            if (x >= 'a' && x <= 'z')
            {
                return ((int)x - (int)'a' + 1);
            }
            else if (x >= 'A' && x <= 'Z')
            {
                return ((int)x - (int)'A' + 27);
            }
            else
            {
                throw new Exception("Shouldn't be in this range");
            }
        }

        public static int PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\03\PartOne.txt");
            // using var reader = new StreamReader(@"..\..\..\Tasks\03\Example.txt");
            string line = String.Empty;
            int s = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var processedLetters = new List<char> { };
                for (int i = 0; i < line.Length / 2; i++)
                {
                    if (!processedLetters.Contains(line[i]) && line.LastIndexOf(line[i]) >= line.Length / 2)
                    {
                        s += GetPriority(line[i]);
                        processedLetters.Add(line[i]);
                    }
                }
            }

            return s;
        }

        public static int PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\03\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\03\Example.txt");
            string line = String.Empty;
            int s = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var line1 = reader.ReadLine();
                var line2 = reader.ReadLine();

                var processedLetters = new List<char> { };
                for (int i = 0; i < line.Length; i++)
                {
                    if (!processedLetters.Contains(line[i]))
                    {
                        // New letter, let's try
                        if (line1.IndexOf(line[i]) == -1)
                        {
                            // Doesn't contain, add to processed letters and continue.
                            processedLetters.Add(line[i]);
                            continue;
                        }
                        else if (line2.IndexOf(line[i]) == -1)
                        {
                            // Doesn't contain, add to processed letters and continue.
                            processedLetters.Add(line[i]);
                            continue;
                        }
                        else
                        {
                            // Both contain, calculate priortiy, add and break for loop.
                            s += GetPriority(line[i]);
                            break;
                        }
                    }

                }
            }

            return s;
        }
    }
}
