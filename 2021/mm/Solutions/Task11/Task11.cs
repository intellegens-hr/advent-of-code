using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task11
    {
        public static int Solve()
        {
            // Read data
            var lines = File.ReadLines(Constants.PREPATH + "Task11\\Input.txt");
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

            for (int steps = 0; steps < 100; steps++)
            {
                // Increase matrix by 1
                IncreaseMatrixByOne(rows, cols, matrix);

                // Explode 10s
                sum += Explode10s(rows, cols, matrix);

                // Return 10s to 0
                Reset10s(rows, cols, matrix);
            }

            return sum;
        }

        private static int Explode10s(int rows, int cols, int[,] matrix)
        {
            int sum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Call recursion to explode everything!
                    sum += Explode(i, j, rows, cols, matrix);
                }
            }
            return sum;
        }

        private static int Explode(int y, int x, int rows, int cols, int[,] matrix)
        {
            int sum = 0;
            if (matrix[y, x] == 10)
            {
                // Explode me!
                matrix[y, x]++;
                sum++;

                // Increase everyone around me. 
                // If someone around me is already set to 10 (to explode in this round), do not touch them, they will explode themselves once it's their turn!
                for (int deltaX = -1; deltaX <= 1; deltaX++)
                    for (int deltaY = -1; deltaY <= 1; deltaY++)
                    {
                        if (x + deltaX >= 0 && y + deltaY >= 0 && x + deltaX < cols && y + deltaY < rows && !(deltaY == 0 && deltaX == 0))
                        {
                            if (matrix[y + deltaY, x + deltaX] != 10) matrix[y + deltaY, x + deltaX]++;
                            if (matrix[y + deltaY, x + deltaX] <= 10) sum += Explode(y + deltaY, x + deltaX, rows, cols, matrix);
                        }
                    }
            }
            return sum;
        }

        private static void Reset10s(int rows, int cols, int[,] matrix)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > 10) matrix[i, j] = 0;
                }
            }
        }

        private static void IncreaseMatrixByOne(int rows, int cols, int[,] matrix)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j]++;
                }
            }
        }

        public static int Solve2()
        {
            // Read data
            var lines = File.ReadLines(Constants.PREPATH + "Task11\\Input.txt");
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


            int steps = 0;

            while (true) {
                // Increase step
                steps++;

                // Increase matrix by 1
                IncreaseMatrixByOne(rows, cols, matrix);

                // Explode 10s
                if (Explode10s(rows, cols, matrix) == 100) break ;

                // Return 10s to 0
                Reset10s(rows, cols, matrix);
            }

            return steps;
        }

        
    }
}
