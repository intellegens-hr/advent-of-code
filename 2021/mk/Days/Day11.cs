using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day11 : DayBase<long>
    {
        public override int Day => 11;

        private int flashes = 0;
        private const int grid_size = 10;

        public override long First()
        {
            var lines = GetInputLines();
            var grid = new int[grid_size, grid_size];

            for (var i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            //Console.WriteLine($"Before any steps:");
            //PrintGrid(grid);

            for (int day = 1; day <= 100; day++)
            {

                IncreaseAll(grid);

                ResetAll(grid);

                //Console.WriteLine($"After step {day}:");
                //PrintGrid(grid);
                //for (var x = 0; x < grid_size; x++)
                //{
                //    for (int y= 0; y < grid_size; y++)
                //    {
                //        if (grid[x, y] >= grid_size)
                //        {
                //            IncreaseAdjacent(grid, x, y);
                //        }
                //    }
                //}
            }

            return flashes;
        }

        private void IncreaseAll(int[,] grid)
        {
            for (var i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    grid[i, j] += 1;
                    //grid[i, j] %= 10;

                    if (grid[i, j] == 10)
                    {
                        IncreaseAdjacent(grid, i, j);
                    }

                }
            }
        }
        private void ResetAll(int[,] grid)
        {
            for (var i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    if (grid[i, j] >= 10)
                    {
                        grid[i, j] = 0;
                    }

                }
            }
        }
        private bool ResetAllAndCheckEqual(int[,] grid)
        {
            var all = true;

            for (var i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    if (grid[i, j] >= 10)
                    {
                        grid[i, j] = 0;
                    }
                    else
                    {
                        all = false;
                    }

                }
            }

            return all;
        }

        private void IncreaseAdjacent(int[,] grid, int x, int y)
        {
            flashes++;

            //grid[x, y] = 0;
            //PrintGrid(grid);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i >= 0 && x + i < grid_size && y + j >= 0 && y + j < grid_size)
                    {
                        if (grid[x + i, y + j] >= 10)
                            continue;

                        grid[x + i, y + j] += 1;
                        //grid[x + i, y + j] %= 10;

                        if (grid[x + i, y + j] == 10)
                        {
                            IncreaseAdjacent(grid, x + i, y + j);
                        }
                    }
                }
            }
        }

        public override long Second()
        {
            var lines = GetInputLines();
            var grid = new int[grid_size, grid_size];

            for (var i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            for (int day = 1; day <= Int32.MaxValue; day++)
            {
                IncreaseAll(grid);

                if (ResetAllAndCheckEqual(grid))
                {

                    return day;
                }
            }

            return 0;
        }

        static void PrintGrid(int[,] grid)
        {
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0}", grid[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
