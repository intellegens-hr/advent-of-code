using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day25 : DayBase<long>
    {
        public override int Day => 25;

        public override long First()
        {
            var lines = GetInputLines();
            (int x, int y) gridSize = (lines[0].Length, lines.Length);

            var grid = new char[gridSize.x, gridSize.y];
            var changes = true;
            var iterations = 0;

            for (var i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    grid[i, j] = lines[j][i];
                }
            }

            //printGrid(grid);

            while (changes)
            {
                iterations++;
                changes = false;

                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        if (grid[x, y] == '>')
                        {
                            var next = (x + 1) % gridSize.x;

                            if (grid[next, y] == '.')
                            {
                                changes = true;
                                grid[next, y] = '<';
                                grid[x, y] = 'x';

                                //printGrid(grid);
                            }
                        }
                    }
                }
                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        if (grid[x, y] == '<')
                            grid[x, y] = '>';
                        if (grid[x, y] == 'A')
                            grid[x, y] = 'v';
                        if (grid[x, y] == 'x')
                            grid[x, y] = '.';
                    }
                }

                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        if (grid[x, y] == 'v')
                        {
                            var next = (y + 1) % gridSize.y;

                            if (grid[x, next] == '.')
                            {
                                changes = true;
                                grid[x, next] = 'A';
                                grid[x, y] = 'x';

                                //printGrid(grid);
                            }
                        }
                    }
                }

                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        if (grid[x, y] == '<')
                            grid[x, y] = '>';
                        if (grid[x, y] == 'A')
                            grid[x, y] = 'v';
                        if (grid[x, y] == 'x')
                            grid[x, y] = '.';
                    }
                }
                //printGrid(grid);
            }

            return iterations;
        }

        public override long Second()
        {
            throw new NotImplementedException();
        }


        static void printGrid(char[,] grid)
        {
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            for (int j = 0; j < colLength; j++)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    Console.Write(string.Format("{0}", grid[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.ReadLine();
        }
    }
}
