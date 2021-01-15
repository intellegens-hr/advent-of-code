using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task17
{
    public class Task17_2
    {
        public static char ACTIVE = '#';
        public static char INACTIVE = '.';
        public static int SecondPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));

            int initialWidth = lines[0].Length;
            int initialHeight = lines.Count;
            int n = 6;  // number of steps to play

            // Define matrixes. 
            // Max possibility of expanding is n in each direction => 2 n + initial dimension!
            var maxWidth = initialWidth + (2 * n);
            var maxHeight = initialHeight + (2 * n);
            var maxDepth = (2 * n) + 1;
            var maxTime = (2 * n) + 1;

            char[,,,] matrix = new char[maxWidth, maxHeight, maxDepth, maxTime];
            char[,,,] nextMatrix = new char[maxWidth, maxHeight, maxDepth, maxTime];

            // Initialize matrixes from lines
            for (int i = 0; i < maxWidth; i++)
                for (int j = 0; j < maxHeight; j++)
                    for (int k = 0; k < maxDepth; k++)
                        for (int l = 0; l < maxTime; l++)
                        {
                            matrix[i, j, k, l] = INACTIVE;
                            nextMatrix[i, j, k, l] = INACTIVE;
                        }


            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[0].Length; j++)
                {
                    var tempChars = lines[i].Substring(j, 1).ToCharArray();
                    matrix[n + j, n + i, n, n] = tempChars[0];
                    nextMatrix[n + j, n + i, n, n] = tempChars[0];
                }


            // Do the loop
            int currentStep = 0;
            while (currentStep < n)
            {
                currentStep++;

                // Define initial coordinates and widths
                var currentX = n - currentStep;
                var currentWidth = initialWidth + (2 * currentStep);

                var currentY = n - currentStep;
                var currentHeight = initialHeight + (2 * currentStep);

                var currentZ = n - currentStep;
                var currentDepth = 1 + 2 * currentStep;

                var currentT = n - currentStep;
                var currentTime = 1 + 2 * currentStep;

                Console.WriteLine("Current count = " + CountActives(matrix, maxWidth, maxHeight, maxDepth, maxTime));
                //PrintMatrix(matrix, currentX, currentY, currentZ, currentT, currentWidth, currentHeight, currentDepth, currentTime);
                
                // Check required space
                for (int i = 0; i < currentWidth; i++)
                    for (int j = 0; j < currentHeight; j++)
                        for (int k = 0; k < currentDepth; k++)
                            for (int l = 0; l < currentTime; l++)
                            {
                                // Modify nextMatrix based on matrix
                                ModifyNextMatrix(currentX + i, currentY + j, currentZ + k, currentT + l, matrix, nextMatrix, maxWidth, maxHeight, maxDepth, maxTime);
                            }


                matrix = (char[,,,])nextMatrix.Clone();




            }


            return CountActives(matrix, maxWidth, maxHeight, maxDepth, maxTime);
        }

        private static void PrintMatrix(char[,,,] matrix, int currentX, int currentY, int currentZ, int currentT, int currentWidth, int currentHeight, int currentDepth, int currentTime)
        {
            for (int l = currentT + 1; l < currentTime + currentT - 1; l++)
            for (int k = currentZ + 1; k < currentDepth + currentZ - 1; k++)
            {
                Console.WriteLine("z = " + (k - 6) + ", w =" + (l - 6) );
                for (int j = currentY + 1; j < currentY + currentHeight - 1; j++)
                {
                    for (int i = currentX + 1; i < currentWidth + currentX - 1; i++)
                    {
                        Console.Write(matrix[i, j, k,l]);
                    }
                    Console.WriteLine();
                }

            }

            Console.ReadKey();
        }

        private static int CountActives(char[,,,] matrix, int maxWidth, int maxHeight, int maxDepth, int maxTime)
        {
            int count = 0;
            for (int i = 0; i < maxWidth; i++)
                for (int j = 0; j < maxHeight; j++)
                    for (int k = 0; k < maxDepth; k++)
                        for (int l = 0; l < maxTime; l++)
                            if (matrix[i, j, k, l] == ACTIVE) count++;
            return count;
        }

        private static void ModifyNextMatrix(int x, int y, int z, int t, char[,,,] matrix, char[,,,] nextMatrix, int maxWidth, int maxHeight, int maxDepth, int maxTime)
        {
            int numberOfSurroundingActives = CountAdjacentFields(x, y, z, t, matrix, maxWidth, maxHeight, maxDepth, maxTime);
            //Console.WriteLine(String.Format("{0} {1} {2} ==> {3} neighbours", x, y, z, numberOfSurroundingActives));

            // Implement rules
            // Rule 1: If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
            if (matrix[x, y, z, t] == ACTIVE)
            {
                if (numberOfSurroundingActives == 2 || numberOfSurroundingActives == 3)
                {
                    nextMatrix[x, y, z, t] = ACTIVE;
                }
                else
                {
                    nextMatrix[x, y, z, t] = INACTIVE;
                }
            }
            // Rule 2: If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
            else if (matrix[x, y, z, t] == INACTIVE)
            {
                if (numberOfSurroundingActives == 3)
                {
                    nextMatrix[x, y, z, t] = ACTIVE;
                }
                else
                {
                    nextMatrix[x, y, z, t] = INACTIVE;
                }
            }
        }

        private static int CountAdjacentFields(int x, int y, int z, int t, char[,,,] matrix, int maxWidth, int maxHeight, int maxDepth, int maxTime)
        {
            int count = 0;
            for (int i = x - 1; i >= 0 && i < maxWidth && i <= x + 1; i++)
            {
                for (int j = y - 1; j >= 0 && j < maxHeight && j <= y + 1; j++)
                {
                    for (int k = z - 1; k >= 0 && k < maxDepth && k <= z + 1; k++)
                    {
                        for (int l = t - 1; l >= 0 && l < maxTime && l <= t + 1; l++)
                        {
                            if (!(i == x && j == y && k == z && l == t))
                            {
                                if (matrix[i, j, k, l] == ACTIVE)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
}
