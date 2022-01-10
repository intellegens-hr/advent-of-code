using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task14
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task14\\Input.txt");
            string line = string.Empty;
            var rules = new Dictionary<string, string>();
            var counts = new Dictionary<char, int>();
            var str = reader.ReadLine();
            line = reader.ReadLine();   // Empty line

            // Read rules
            do
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(" -> ");
                    rules.Add(parts[0], parts[1]);
                }
            } while (!string.IsNullOrEmpty(line));

            reader.Close();


            // Initial count
            line = str;
            for (int i = 0; i < line.Length; i++)
            {
                if (!counts.ContainsKey(line[i])) counts.Add(line[i], 0);
                counts[line[i]]++;
            }

            // Apply rules
            int n = 10;     // number of steps
            for (int k = 0; k < n; k++)
            {
                Console.WriteLine("Step: " + (k + 1));
                for (int i = 0; i < line.Length - 1; i++)
                {
                    var key = line[i].ToString() + line[i + 1].ToString();
                    if (rules.ContainsKey(key))
                    {
                        var newChar = rules[key].ToCharArray()[0];

                        // increase count
                        if (!counts.ContainsKey(newChar)) counts.Add(newChar, 0);
                        counts[newChar]++;

                        // update string and counter
                        line = line.Substring(0, i + 1) + rules[key] + line.Substring(i + 1);
                        i++;    //skip newly inserted character
                    }
                }
            }

            var max = counts.Values.Max(x => x);
            var min = counts.Values.Min(x => x);

            return max-min;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task14\\Input.txt");
            string line = string.Empty;

            reader.Close();
            return 0;
        }
    }
}
