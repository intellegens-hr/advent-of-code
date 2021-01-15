using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task11
{
    public class Task11
    {
        const char OCCUPIED = '#';
        const char FREE = 'L';
        const char IGNORE = '.';

        public static int FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            var rows = lines.Count;
            var cols = lines[0].Length;
            char[,] matrix = new char[rows, cols];
            char[,] nextMatrix = new char[rows, cols];
            for (var i = 0; i < lines.Count; i++)
            {
                var currArray = lines[i].ToCharArray();
                for (var j = 0; j < currArray.Length; j++)
                {
                    matrix[i, j] = currArray[j];
                }
            }

            int numOfChanges = -1;

            while (numOfChanges != 0)
            {
                // PrintMatrix(matrix, rows, cols);

                nextMatrix = (char[,])matrix.Clone();

                numOfChanges = ApplyRulesForPartTwo(matrix, nextMatrix, rows, cols);

                matrix = (char[,])nextMatrix.Clone();
            }

            return CountPlaces(matrix, rows, cols);
        }


        private static void PrintMatrix(char[,] matrix, int rows, int cols)
        {
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
        /// If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty
        /// </summary>
        /// <param name="inputMatrix"></param>
        /// <param name="nextMatrix"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static int ApplyRulesForPartOne(char[,] inputMatrix, char[,] nextMatrix, int rows, int cols)
        {
            int changes = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Check for rule 1
                    if (inputMatrix[i, j] == FREE)
                    {
                        if (CountCharInAdjacentSeats(inputMatrix, OCCUPIED, i, j, rows, cols) == 0)
                        {
                            nextMatrix[i, j] = OCCUPIED;
                            changes++;
                        }
                    }
                    // Check for rule 2
                    else if (inputMatrix[i, j] == OCCUPIED)
                    {
                        if (CountCharInAdjacentSeats(inputMatrix, OCCUPIED, i, j, rows, cols) >= 4)
                        {
                            nextMatrix[i, j] = FREE;
                            changes++;
                        }
                    }
                }
            }

            return changes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMatrix"></param>
        /// <param name="nextMatrix"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static int ApplyRulesForPartTwo(char[,] inputMatrix, char[,] nextMatrix, int rows, int cols)
        {
            int changes = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Check for rule 1
                    if (inputMatrix[i, j] == FREE)
                    {
                        if (CountCharInDirections(inputMatrix, OCCUPIED, FREE, i, j, rows, cols) == 0)
                        {
                            nextMatrix[i, j] = OCCUPIED;
                            changes++;
                        }
                    }
                    // Check for rule 2
                    else if (inputMatrix[i, j] == OCCUPIED)
                    {
                        if (CountCharInDirections(inputMatrix, OCCUPIED, FREE, i, j, rows, cols) >= 5)
                        {
                            nextMatrix[i, j] = FREE;
                            changes++;
                        }
                    }
                }
            }

            return changes;
        }


        private static int CountCharInAdjacentSeats(char[,] matrix, char charToCheck, int y, int x, int maxRows, int maxCols)
        {
            int count = 0;

            if (y - 1 >= 0 && x - 1 >= 0 && matrix[y - 1, x - 1] == charToCheck) count++;
            if (y - 1 >= 0 && matrix[y - 1, x] == charToCheck) count++;
            if (y - 1 >= 0 && x + 1 < maxCols && matrix[y - 1, x + 1] == charToCheck) count++;

            if (x - 1 >= 0 && matrix[y, x - 1] == charToCheck) count++;
            if (x + 1 < maxCols && matrix[y, x + 1] == charToCheck) count++;

            if (y + 1 < maxRows && x - 1 >= 0 && matrix[y + 1, x - 1] == charToCheck) count++;
            if (y + 1 < maxRows && matrix[y + 1, x] == charToCheck) count++;
            if (y + 1 < maxRows && x + 1 < maxCols && matrix[y + 1, x + 1] == charToCheck) count++;

            return count;
        }


        private static int CountCharInDirections(char[,] matrix, char charToCheck, char charToBlock, int y, int x, int maxRows, int maxCols)
        {
            int count = 0;

            // Up left
            var currX = x - 1;
            var currY = y - 1;
            while (currY >= 0 && currX >= 0)
            {
                if (matrix[currY, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, currX] == charToBlock) break;

                currX--;
                currY--;
            }

            // Up
            currY = y - 1;
            while (currY >= 0)
            {
                if (matrix[currY, x] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, x] == charToBlock) break;

                currY--;
            }

            // Up right
            currX = x + 1;
            currY = y - 1;
            while (currY >= 0 && currX < maxCols)
            {
                if (matrix[currY, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, currX] == charToBlock) break;

                currX++;
                currY--;
            }

            // Left
            currX = x - 1;
            while (currX >= 0)
            {
                if (matrix[y, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[y, currX] == charToBlock) break;

                currX--;
            }

            // Right
            currX = x + 1;
            while (currX < maxCols)
            {
                if (matrix[y, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[y, currX] == charToBlock) break;

                currX++;
            }

            // Down left
            currX = x - 1;
            currY = y + 1;
            while (currY < maxRows && currX >= 0)
            {
                if (matrix[currY, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, currX] == charToBlock) break;

                currX--;
                currY++;
            }

            // Down
            currY = y + 1;
            while (currY < maxRows)
            {
                if (matrix[currY, x] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, x] == charToBlock) break;

                currY++;
            }

            // Down right
            currX = x + 1;
            currY = y + 1;
            while (currY < maxRows && currX < maxCols)
            {
                if (matrix[currY, currX] == charToCheck)
                {
                    count++;
                    break;
                }
                else if (matrix[currY, currX] == charToBlock) break;
                currX++;
                currY++;
            }


            return count;
        }

        /// <summary>
        /// Method used in the end to calculate occupied places
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="occupiedChar"></param>
        /// <returns></returns>
        private static int CountPlaces(char[,] matrix, int rows, int cols)
        {
            var count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == OCCUPIED) count++;
                }
            }
            return count;
        }
    }
}
