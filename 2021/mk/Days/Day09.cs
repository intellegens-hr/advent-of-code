using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day09 : DayBase<int>
    {
        public override int Day => 9;

        public override int First()
        {
            var lines = GetInputLines().ToList();
            (int x, int y) max = (lines[0].Length, lines.Count);

            var grid = new int[max.x, max.y];

            for (var i = 0; i < max.x; i++)
            {
                for (int j = 0; j < max.y; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            var sum = 0;

            for (var i = 0; i < max.x; i++)
            {
                for (int j = 0; j < max.y; j++)
                {
                    if (IsLowPoint(grid, i, j, max))
                        sum += grid[i, j] + 1;
                }
            }
            return sum;
        }

        bool IsLowPoint(int[,] grid, int i, int j, (int x, int y) max)
        {
            var adds = new List<(int x, int y)> { (0, 1), (1, 0), (-1, 0), (0, -1) };
            var number = grid[i, j];
            foreach (var add in adds)
            {
                if (i + add.x < 0 || i + add.x >= max.x)
                    continue;

                if (j + add.y < 0 || j + add.y >= max.y)
                    continue;

                var neighbour = grid[i + add.x, j + add.y];
                if (number >= neighbour)
                    return false;
            }

            return true;
        }

        public override int Second()
        {
            var lines = GetInputLines().ToList();
            (int x, int y) max = (lines[0].Length, lines.Count);

            var grid = new int[max.x, max.y];

            for (var i = 0; i < max.x; i++)
            {
                for (int j = 0; j < max.y; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            var visited = new List<(int, int)>();
            var sizes = new List<int>();

            for (var i = 0; i < max.x; i++)
            {
                for (int j = 0; j < max.y; j++)
                { 
                    var size = MeasureBasin(grid, visited, max, (i, j));

                    sizes.Add(size);
                }
            }

            var s = sizes.OrderByDescending(a => a).ToList();

            return s[0] * s[1] * s[2];
        }

        private int MeasureBasin(int[,] grid, List<(int, int)> visited, (int x, int y) max, (int x, int y) current)
        {
            if (grid[current.x, current.y] != 9 && !visited.Contains(current))
            {
                visited.Add(current);

                var adds = new List<(int x, int y)> { (0, 1), (1, 0), (-1, 0), (0, -1) };
                var total = 1;
                foreach (var add in adds)
                {
                    if (current.x + add.x < 0 || current.x + add.x >= max.x)
                        continue;

                    if (current.y + add.y < 0 || current.y + add.y >= max.y)
                        continue;

                    var neighbour = (current.x + add.x, current.y + add.y);

                    total += MeasureBasin(grid, visited, max, neighbour);

                }
                return total;
            }

            return 0;
        }
    }
}
