using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2021.Solutions.Task2
{
    public class Task2
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task2\\Input.txt");
            string line = string.Empty;

            int x = 0;
            int depth = 0;

            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var parsed = line.Split(" ");

                    var value = Convert.ToInt32(parsed[1]);
                    switch (parsed[0])
                    {
                        case "forward":
                            x += value;
                            break;
                        case "up":
                            depth -= value;
                            break;
                        case "down":
                            depth += value;
                            break;
                    }
                }
            }

            reader.Close();
            return x * depth;
        }

        public static long Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task2\\Input.txt");
            string line = string.Empty;

            int x = 0;
            long depth = 0;
            int aim = 0;

            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var parsed = line.Split(" ");

                    var value = Convert.ToInt32(parsed[1]);
                    switch (parsed[0])
                    {
                        case "forward":
                            depth += aim * value;
                            x += value;
                            
                            break;
                        case "up":
                            aim -= value;
                            break;
                        case "down":
                            aim += value;
                            break;
                    }
                }
            }

            reader.Close();
            return x * depth;
        }
    }
}
