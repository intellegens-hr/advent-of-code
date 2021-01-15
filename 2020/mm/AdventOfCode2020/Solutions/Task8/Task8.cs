using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task8
{
    public class Task8
    {
        public static int FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int sum;
            bool isInfiniteLoop;

            (isInfiniteLoop, sum) = WhileLoop(lines);

            return sum;

        }




        public static int SecondPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int sum;
            bool isInfiniteLoop;

            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].IndexOf("jmp") != -1)
                {
                    // Replace jmp with nop
                    lines[i] = lines[i].Replace("jmp", "nop");

                    // Run the loop
                    (isInfiniteLoop, sum) = WhileLoop(lines);

                    // Check for conditions
                    if (!isInfiniteLoop) return sum;

                    // Replace back
                    lines[i] = lines[i].Replace("nop", "jmp");
                }
                else if (lines[i].IndexOf("nop") != -1)
                {
                    // Replace jmp with nop
                    lines[i] = lines[i].Replace("nop", "jmp");

                    // Run the loop
                    (isInfiniteLoop, sum) = WhileLoop(lines);

                    // Check for conditions
                    if (!isInfiniteLoop) return sum;

                    // Replace back
                    lines[i] = lines[i].Replace("jmp", "nop");
                }
            }

            throw new Exception("We should never end up here!");
        }

        private static (bool, int) WhileLoop(List<string> lines)
        {
            int i = 0;
            int sum = 0;
            List<int> linesAlreadyCounted = new List<int>();

            while (i < lines.Count)
            {
                if (linesAlreadyCounted.Contains(i))
                {
                    return (true, sum);
                }
                else
                {
                    linesAlreadyCounted.Add(i);
                }

                var parts = lines[i].Split(" ");
                if (parts[1].StartsWith("+")) parts[1] = parts[1] = parts[1].Substring(1);

                var command = parts[0];
                var argument = Convert.ToInt32(parts[1]);

                switch (command)
                {
                    case "nop":
                        i++;
                        break;
                    case "jmp":
                        i += argument;
                        break;
                    case "acc":
                        sum += argument;
                        i++;
                        break;
                }
            }

            return (false, sum);
        }
    }
}
