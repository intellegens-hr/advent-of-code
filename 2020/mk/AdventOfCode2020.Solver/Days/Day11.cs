using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day11 : PuzzleDay<int>
    {
        public override int Day => 11;

        readonly char EMPTY = 'L';
        readonly char OCCUPIED = '#';

        public override int First()
        {
            var input = GetInputLines().Select(l => l.ToCharArray()).ToArray();
            var changed = false;

            do
            {
                changed = false;

                var copy = input.Select(a => a.ToArray()).ToArray();
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] == EMPTY && Adjacent(copy, i, j, OCCUPIED) == 0)
                        {
                            changed = true;
                            input[i][j] = OCCUPIED;
                        }
                        else if (input[i][j] == OCCUPIED && Adjacent(copy, i, j, OCCUPIED) >= 4)
                        {
                            changed = true;
                            input[i][j] = EMPTY;
                        }
                    }
                }

                //foreach (var item in input)
                //    Console.WriteLine(new string(item));

                //Console.WriteLine();
            }
            while (changed);

            return input.Sum(a => a.Count(b => b == OCCUPIED));


        }

        int Adjacent(char[][] input, int i, int j, char charToCheck)
        {
            List<(int, int)> adjacentIndexes = GetAdjacentIndexes(input, i, j);

            return adjacentIndexes.Count(ai => input[ai.Item1][ai.Item2] == charToCheck);
        }


        List<(int, int)> GetDirections()
        {
            var directions = new List<(int, int)>();

            for (int m = -1; m <= 1; m++)
            {
                for (int n = -1; n <= 1; n++)
                {
                    if (m == 0 && n == 0)
                        continue;

                        directions.Add((m, n));
                }
            }

            return directions;
        }

        List<(int, int)> GetAdjacentIndexes(char[][] input, int i, int j)
        {
            var adjacentIndexes = new List<(int, int)>();

            for (int m = -1; m <= 1; m++)
            {
                for (int n = -1; n <= 1; n++)
                {
                    if (m == 0 && n == 0)
                        continue;

                    if ((i + m >= 0 && i + m < input.Length) && (j + n >= 0 && j + n < input[0].Length))
                    {
                        adjacentIndexes.Add((i + m, j + n));
                    }
                }
            }

            return adjacentIndexes;
        }

        public override int Second()
        {
            var input = GetInputLines().Select(l => l.ToCharArray()).ToArray();
            var changed = false;

            do
            {
                changed = false;

                var copy = input.Select(a => a.ToArray()).ToArray();
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] == EMPTY && FirstInLineSight(copy, i, j, OCCUPIED) == 0)
                        {
                            changed = true;
                            input[i][j] = OCCUPIED;
                        }
                        else if (input[i][j] == OCCUPIED && FirstInLineSight(copy, i, j, OCCUPIED) >= 5)
                        {
                            changed = true;
                            input[i][j] = EMPTY;
                        }
                    }
                }

                //foreach (var item in input)
                //    Console.WriteLine(new string(item));

                //Console.WriteLine();
            }
            while (changed);

            return input.Sum(a => a.Count(b => b == OCCUPIED));


        }

        int FirstInLineSight(char[][] input, int i, int j, char charToCheck)
        {
            var directions = GetDirections();
            var cnt = 0;

            foreach (var direction in directions)
            {
                var start = (i, j);
                while (true)
                {
                    start = (start.i + direction.Item1, start.j + direction.Item2);

                    if (start.i < 0 || start.i >= input.Length)
                        break;
                    if (start.j < 0 || start.j >= input[0].Length)
                        break;

                    if (input[start.i][start.j] == '.')
                    {
                        continue;
                    }
                    else if (input[start.i][start.j] == charToCheck)
                    {
                        cnt++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return cnt;
        }
    }
}
