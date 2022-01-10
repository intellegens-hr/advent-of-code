using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task09
    {
        public static int Solve()
        {
            // Read data
            var lines = File.ReadLines(Constants.PREPATH + "Task9\\Input.txt");
            var rows = lines.Count();
            var cols = lines.First().Length;
            int[,] matrix = new int[rows, cols];
            int row = 0;
            foreach (var line in lines)
            {
                var chars = line.ToCharArray();
                int col = 0;
                foreach (var chr in chars)
                {
                    matrix[row, col] = int.Parse(chr.ToString());
                    col++;
                }
                row++;
            }


            int sum = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (
                        ((i - 1 >= 0 && matrix[i - 1, j] > matrix[i, j]) || (i - 1 < 0))    // up
                        && ((i + 1 < rows && matrix[i + 1, j] > matrix[i, j]) || (i + 1 >= rows))    // down
                        && ((j - 1 >= 0 && matrix[i, j - 1] > matrix[i, j]) || (j - 1 < 0))     // left
                        && ((j + 1 < cols && matrix[i, j + 1] > matrix[i, j]) || (j + 1 >= cols))     // right
                        )
                    {
                        sum += matrix[i, j] + 1;
                    }

                }
            }

            return sum;
        }

        public static int Solve2()
        {
            var lines = File.ReadLines(Constants.PREPATH + "Task9\\Input.txt");
            var rows = lines.Count();
            var cols = lines.First().Length;
            int[,] matrix = new int[rows, cols];
            int row = 0;
            foreach (var line in lines)
            {
                var chars = line.ToCharArray();
                int col = 0;
                foreach (var chr in chars)
                {
                    matrix[row, col] = int.Parse(chr.ToString());
                    col++;
                }
                row++;
            }


            List<(int, int)> bottoms = new List<(int, int)>();
            int product = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (
                        ((i - 1 >= 0 && matrix[i - 1, j] > matrix[i, j]) || (i - 1 < 0))    // up
                        && ((i + 1 < rows && matrix[i + 1, j] > matrix[i, j]) || (i + 1 >= rows))    // down
                        && ((j - 1 >= 0 && matrix[i, j - 1] > matrix[i, j]) || (j - 1 < 0))     // left
                        && ((j + 1 < cols && matrix[i, j + 1] > matrix[i, j]) || (j + 1 >= cols))     // right
                        )
                    {
                        bottoms.Add((i, j));
                    }

                }
            }

            List<int> basinSizes = new List<int>();
            foreach (var bottom in bottoms)
            {
                basinSizes.Add(GetBasinSize(bottom.Item1, bottom.Item2, matrix, rows, cols));
            }
            basinSizes = basinSizes.OrderByDescending(a => a).ToList();

            return basinSizes[0] * basinSizes[1] * basinSizes[2];
        }

        private static int GetBasinSize(int y, int x, int[,] matrix, int rows, int cols)
        {
            matrix[y, x] = -1;      // Set me to already counted!

            // Check if top, bottom, left and right are -1 or 9 or wall. if all are, then return 1
            if (
                    (y - 1 < 0 || matrix[y - 1, x] == -1 || matrix[y - 1, x] == 9)        // top
                    && (y + 1 == rows || matrix[y + 1, x] == -1 || matrix[y + 1, x] == 9)        // bottom
                    && (x - 1 < 0 || matrix[y, x - 1] == -1 || matrix[y, x - 1] == 9)              // left
                    && (x + 1 == cols || matrix[y, x + 1] == -1 || matrix[y, x + 1] == 9)              // right
                )
            {
                return 1;
            }

            // Else, return 1 + sum of results in all directions which are not 0 or 9
            var sum = 1;            // me

            if (!(y - 1 < 0 || matrix[y - 1, x] == -1 || matrix[y - 1, x] == 9)) sum += GetBasinSize(y - 1, x, matrix, rows, cols);     // not blocked by top
            if (!(y + 1 == rows || matrix[y + 1, x] == -1 || matrix[y + 1, x] == 9)) sum += GetBasinSize(y + 1, x, matrix, rows, cols);     // not blocked by top
            if (!(x - 1 < 0 || matrix[y, x - 1] == -1 || matrix[y, x - 1] == 9)) sum += GetBasinSize(y, x - 1, matrix, rows, cols);     // not blocked by top
            if (!(x + 1 == cols || matrix[y, x + 1] == -1 || matrix[y, x + 1] == 9)) sum += GetBasinSize(y, x + 1, matrix, rows, cols);     // not blocked by top

            return sum;
        }
    }
}
