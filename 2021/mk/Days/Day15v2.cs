using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day15v2 : DayBase<long>
    {
        public override int Day => 15;

        public override long First()
        {
            var input = GetInputLines();
            (int x, int y) max = (input[0].Length, input.Length);
            var grid = new int[max.x, max.y];
            var dist = new int[max.x, max.y];

            for (int y = 0; y < max.y; y++)
            {
                for (int x = 0; x < max.x; x++)
                {
                    grid[x, y] = int.Parse(input[y][x].ToString());
                }
            }

            return Solve(max, grid, dist);
        }

        public override long Second()
        {
            var input = GetInputLines();
            (int x, int y) max = (input[0].Length * 5, input.Length * 5);
            var grid = new int[max.x, max.y];
            var dist = new int[max.x, max.y];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int y = 0; y < input.Length; y++)
                    {
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            grid[input[y].Length * i + x, input.Length * j + y] = (int.Parse(input[y][x].ToString()) + i + j) >= 10 ? (int.Parse(input[y][x].ToString()) + i + j) - 9 : (int.Parse(input[y][x].ToString()) + i + j);
                        }
                    }
                }
            }


            return Solve(max, grid, dist);
        }

        private static long Solve((int x, int y) max, int[,] grid, int[,] dist)
        {
            dist[0, 0] = 0;

            for (int y = 1; y < max.y; y++)
                dist[0, y] = dist[0, y - 1] + grid[0, y];

            for (int x = 1; x < max.x; x++)
                dist[x, 0] = dist[x - 1, 0] + grid[x, 0];

            for (int y = 1; y < max.y; y++)
            {
                for (int x = 1; x < max.x; x++)
                {
                    var min = Math.Min(dist[x - 1, y], dist[x, y - 1]);
                    dist[x, y] = grid[x, y] + min;
                }
            }

            return dist[max.x - 1, max.y - 1];
        }

    }
}
