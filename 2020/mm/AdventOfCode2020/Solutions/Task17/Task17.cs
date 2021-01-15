using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task17
{
    public class Task17
    {
        public static char ACTIVE = '#';
        public static char INACTIVE = '.';
        public static int FirstPart(string filename)
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
            char[,,] matrix = new char[maxWidth, maxHeight, maxDepth];
            char[,,] nextMatrix = new char[maxWidth, maxHeight, maxDepth];

            // Initialize matrixes from lines
            for (int i = 0; i < maxWidth; i++)
                for (int j = 0; j < maxHeight; j++)
                    for (int k = 0; k < maxDepth; k++)
                    {
                        matrix[i, j, k] = INACTIVE;
                        nextMatrix[i, j, k] = INACTIVE;
                    }


            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[0].Length; j++)
                {
                    var tempChars = lines[i].Substring(j, 1).ToCharArray();
                    matrix[n + j, n + i, n] = tempChars[0];
                    nextMatrix[n + j, n + i, n] = tempChars[0];
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

                // PrintMatrix(matrix, currentX, currentY, currentZ, currentWidth, currentHeight, currentDepth);

                // Check required space
                for (int i = 0; i < currentWidth; i++)
                    for (int j = 0; j < currentHeight; j++)
                        for (int k = 0; k < currentDepth; k++)
                        {
                            // Modify nextMatrix based on matrix
                            ModifyNextMatrix(currentX + i, currentY + j, currentZ + k, matrix, nextMatrix, maxWidth, maxHeight, maxDepth);
                        }


                matrix = (char[,,])nextMatrix.Clone();



                
            }


            return CountActives(matrix, maxWidth, maxHeight, maxDepth);
        }

        private static void PrintMatrix(char[,,] matrix, int currentX, int currentY, int currentZ, int currentWidth, int currentHeight, int currentDepth)
        {
            for (int k = currentZ; k < currentDepth + currentZ; k++)
            {
                Console.WriteLine("z = " + k);
                for (int j = currentY; j < currentY + currentHeight; j++)
                {
                    for (int i = currentX; i < currentWidth + currentX; i++)
                    {
                        Console.Write(matrix[i, j, k]);
                    }
                    Console.WriteLine();
                }

            }

            Console.ReadKey();
        }

        private static int CountActives(char[,,] matrix, int maxWidth, int maxHeight, int maxDepth)
        {
            int count = 0;
            for (int i = 0; i < maxWidth; i++)
                for (int j = 0; j < maxHeight; j++)
                    for (int k = 0; k < maxDepth; k++)
                        if (matrix[i, j, k] == ACTIVE) count++;
            return count;
        }

        private static void ModifyNextMatrix(int x, int y, int z, char[,,] matrix, char[,,] nextMatrix, int maxWidth, int maxHeight, int maxDepth)
        {
            int numberOfSurroundingActives = CountAdjacentFields(x, y, z, matrix, maxWidth, maxHeight, maxDepth);
            //Console.WriteLine(String.Format("{0} {1} {2} ==> {3} neighbours", x, y, z, numberOfSurroundingActives));

            // Implement rules
            // Rule 1: If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
            if (matrix[x, y, z] == ACTIVE)
            {
                if (numberOfSurroundingActives == 2 || numberOfSurroundingActives == 3)
                {
                    nextMatrix[x, y, z] = ACTIVE;
                }
                else
                {
                    nextMatrix[x, y, z] = INACTIVE;
                }
            }
            // Rule 2: If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
            else if (matrix[x, y, z] == INACTIVE)
            {
                if (numberOfSurroundingActives == 3)
                {
                    nextMatrix[x, y, z] = ACTIVE;
                }
                else
                {
                    nextMatrix[x, y, z] = INACTIVE;
                }
            }
        }

        private static int CountAdjacentFields(int x, int y, int z, char[,,] matrix, int maxWidth, int maxHeight, int maxDepth)
        {
            int count = 0;
            for (int i = x - 1; i >= 0 && i < maxWidth && i <= x + 1; i++)
            {
                for (int j = y - 1; j >= 0 && j < maxHeight && j <= y + 1; j++)
                {
                    for (int k = z - 1; k >= 0 && k < maxDepth && k <= z + 1; k++)
                    {
                        if (!(i == x && j == y && k == z))
                        {
                            if (matrix[i, j, k] == ACTIVE)
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
}
