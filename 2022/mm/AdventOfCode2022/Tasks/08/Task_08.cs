using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2022.Tasks._08
{
    public class Task_08
    {
        public static int[,] Matrix;
        public static List<(int, int)> treesAdded;

        public static int PartOne()
        {
            // var lines = File.ReadAllLines(@"..\..\..\Tasks\08\Example.txt");
            var lines = File.ReadAllLines(@"..\..\..\Tasks\08\PartOne.txt");
            var rows = lines.Length;
            var cols = lines[0].Length;

            Matrix = new int[rows, cols];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Matrix[i, j] = Convert.ToInt32(lines[i][j].ToString());
                }
            }

            treesAdded = new();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int[] maxIterations =
                    {
                        i, j, rows - i, cols - j
                    };
                    Array.Sort(maxIterations);
                    var max = maxIterations[3];

                    var k = 1;
                    bool top = true, left = true, bottom = true , right = true;
                    while (k <= max)
                    {
                        if (i - k >= 0 && Matrix[i - k, j] >= Matrix[i, j]) top = false;
                        if (i + k < cols && Matrix[i + k, j] >= Matrix[i, j]) bottom = false;
                        if (j - k >= 0 && Matrix[i, j - k] >= Matrix[i, j]) left = false;
                        if (j + k < rows && Matrix[i, j + k] >= Matrix[i, j]) right = false;

                        k++;
                    }

                    if (top || left || bottom || right) treesAdded.Add((i, j));
                }
            }


            return treesAdded.Count;
        }

        public static int PartTwo()
        {
            // var lines = File.ReadAllLines(@"..\..\..\Tasks\08\Example.txt");
            var lines = File.ReadAllLines(@"..\..\..\Tasks\08\PartOne.txt");
            var rows = lines.Length;
            var cols = lines[0].Length;

            Matrix = new int[rows, cols];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Matrix[i, j] = Convert.ToInt32(lines[i][j].ToString());
                }
            }

            treesAdded = new();
            int scenic = -1;
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    int[] maxIterations =
                    {
                        i, j, rows - i, cols - j
                    };
                    Array.Sort(maxIterations);
                    var max = maxIterations[3];

                    var k = 1;
                    bool top = true, left = true, bottom = true, right = true;
                    int s = 1;
                    while (k <= max)
                    {
                        if (top && (i - k == 0 || (i - k > 0 && Matrix[i - k, j] >= Matrix[i, j] ))) { top = false; s *= k; }
                        if (bottom && (i + k == cols - 1 || (i + k < cols && Matrix[i + k, j] >= Matrix[i, j] ))) { bottom = false; s *= k; }
                        if (left && (j - k == 0 || (j - k >= 0 && Matrix[i, j - k] >= Matrix[i, j] ))) { left = false; s *= k; }
                        if (right && (j + k == rows - 1 || (j + k < rows && Matrix[i, j + k] >= Matrix[i, j] ))) { right = false; s *= k; }

                        k++;
                    }

                    if (top || left || bottom || right) treesAdded.Add((i, j));

                    if (s > scenic)
                    {
                        // Console.WriteLine($"Current max is {s} for ({i},{j})");
                        scenic = s;
                    }
                }
            }


            return scenic;
        }
    }
}
