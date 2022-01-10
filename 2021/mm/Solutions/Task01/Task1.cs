using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2021.Solutions.Task1
{
    public class Task1
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task1\\Input.txt");
            string line = string.Empty;
            int prevDepth = 0;
            int counter = 0;
            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    int currentDepth = Convert.ToInt32(line);
                    if (currentDepth > prevDepth)
                    {
                        counter++;
                    }
                    prevDepth = currentDepth;
                }

            } while (line != null);


            reader.Close();
            return counter - 1;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task1\\Input.txt");
            string line = string.Empty;
            int nextLine = 0;
            int counter = 0;

            int[] lines = new int[3];
            lines[0] = Convert.ToInt32(reader.ReadLine());
            lines[1] = Convert.ToInt32(reader.ReadLine());
            lines[2] = Convert.ToInt32(reader.ReadLine());

            int i = 2;
            
            do
            {

                int previousSum = lines[i] + lines[i - 1] + lines[i - 2];
                line = reader.ReadLine();
                
                if (line != null)
                {
                    nextLine = Convert.ToInt32(line);

                    int currentSum = previousSum - lines[i - 2] + nextLine;
                    if (currentSum > previousSum)
                    {
                        counter++;
                    }

                    lines[i - 2] = lines[i - 1];
                    lines[i - 1] = lines[i];
                    lines[i] = nextLine;
                    
                }

            } while (line != null);


            reader.Close();
            return counter;
        }
    }
}
