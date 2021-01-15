using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task3
{
    public class Task3
    {
        private class TobogganPath
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int RowParam { get; set; }
            public int ColParam { get; set; }
            public int NumberOfTrees { get; set; }

            public TobogganPath(int rowParam, int colParam)
            {
                RowParam = rowParam;
                ColParam = colParam;
                NumberOfTrees = 0;
                X = 0;
                Y = 0;
            }
        }

        public static long FirstPart(string filename)
        {
            List<TobogganPath> paths = new List<TobogganPath>()
            {
                new TobogganPath(1, 1),
                new TobogganPath(1, 3),
                new TobogganPath(1, 5),
                new TobogganPath(1, 7),
                new TobogganPath(2, 1)
            };

            int width;
            long product = 1;

            var data = ReadInputs.ReadAllStrings(filename);

            foreach (var path in paths)
            {
                for (var i = path.RowParam; i < data.Count; i += path.RowParam)
                {
                    string line = data[i];
                    width = line.Length;
                    path.Y += path.RowParam;
                    path.X += path.ColParam;

                    if (line.Substring((path.X) % width, 1) == "#")
                    {
                        path.NumberOfTrees++;
                        line = line.Substring(0, ((path.X) % width)) + "X" + line.Substring(((path.X) % width) + 1);
                    } else
                    {
                        line = line.Substring(0, ((path.X) % width)) + "O" + line.Substring(((path.X) % width) + 1);
                    }

                    Console.WriteLine("{0} ---> {1} {2}", line, path.X, path.Y);

                    // Console.WriteLine("{0}, with {1} at position {2} (len: {3})", line, line.Substring((path.X) % width, 1), (path.X) % width, width);
                    

                }
                Console.WriteLine("Path {0} {1} --> Number = {2} \n\n\n", path.ColParam, path.RowParam, path.NumberOfTrees);

                product *= path.NumberOfTrees;
            }

            
            
            return product;
        }
    }
}
