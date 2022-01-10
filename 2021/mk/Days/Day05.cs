using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public struct IntVector2
    {
        public int X, Y;

        public IntVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    internal class Day05 : DayBase<int>
    {
        public override int Day => 5;

        public override int First()
        {
            var numbers = GetInputLines().Select(l => new
            {
                from = new IntVector2(int.Parse(l.Split(" -> ")[0].Split(',')[0]), int.Parse(l.Split(" -> ")[0].Split(',')[1])),
                to = new IntVector2(int.Parse(l.Split(" -> ")[1].Split(',')[0]), int.Parse(l.Split(" -> ")[1].Split(',')[1]))
            }).ToList();

            var max = new List<int> { numbers.Max(a => a.from.X), numbers.Max(a => a.from.Y), numbers.Max(a => a.to.X), numbers.Max(a => a.to.Y) }.Max() + 1;

            var grid = new int[max, max];
            var cnt = 0;

            foreach (var item in numbers)
            {

                if (item.from.X == item.to.X)
                {
                    var fromto = item.from.Y < item.to.Y ? (item.from.Y, item.to.Y) : (item.to.Y, item.from.Y);
                    for (int i = fromto.Item1; i <= fromto.Item2; i++)
                    {
                        grid[item.from.X, i]++;

                        if (grid[item.from.X, i] == 2)
                            cnt++;
                    }
                    //Console.WriteLine(item.from.X + "," + item.from.Y + " -> " + item.to.X + "," + item.to.Y);
                    //printGrid(grid);
                }
                else if (item.from.Y == item.to.Y)
                {
                    var fromto = item.from.X < item.to.X ? (item.from.X, item.to.X) : (item.to.X, item.from.X);
                    for (int i = fromto.Item1; i <= fromto.Item2; i++)
                    {
                        grid[i, item.from.Y]++;

                        if (grid[i, item.from.Y] == 2)
                            cnt++;
                    }
                    //Console.WriteLine(item.from.X + "," + item.from.Y + " -> " + item.to.X + "," + item.to.Y);
                    //printGrid(grid);
                }
                //else if (item.from.Y - item.to.Y == item.from.X - item.to.X)
                //{
                //    var fromto = item.from.X < item.to.X ? (item.from.X, item.to.X) : (item.to.X, item.from.X);
                //    for (int i = fromto.Item1; i <= fromto.Item2; i++)
                //    {
                //        grid[i, item.from.Y]++;

                //        if (grid[i, item.from.Y] == 2)
                //            cnt++;
                //    }
                //    Console.WriteLine(item.from.X + "," + item.from.Y + " -> " + item.to.X + "," + item.to.Y);
                //    printGrid(grid);
                //}
            }

            return cnt;
        }

        public override int Second()
        {
            var numbers = GetInputLines().Select(l => new
            {
                from = new IntVector2(int.Parse(l.Split(" -> ")[0].Split(',')[0]), int.Parse(l.Split(" -> ")[0].Split(',')[1])),
                to = new IntVector2(int.Parse(l.Split(" -> ")[1].Split(',')[0]), int.Parse(l.Split(" -> ")[1].Split(',')[1]))
            }).ToList();

            var max = new List<int> { numbers.Max(a => a.from.X), numbers.Max(a => a.from.Y), numbers.Max(a => a.to.X), numbers.Max(a => a.to.Y) }.Max() + 1;

            var grid = new int[max, max];
            var cnt = 0;

            foreach (var item in numbers)
            {
                (int x, int y) increment = (0, 0);
                var start = item.from.X < item.to.X ? item.from : item.to;
                var amt = Math.Abs(item.to.X - item.from.X);

                if (item.from.X == item.to.X)
                {
                    increment = (0, 1);
                    start = item.from.Y < item.to.Y ? item.from : item.to;
                    amt = Math.Abs(item.to.Y - item.from.Y);
                }
                else if (item.from.Y == item.to.Y)
                {
                    increment = (1, 0);
                    start = item.from.X < item.to.X ? item.from : item.to;
                }
                else if (item.from.Y - item.to.Y == item.from.X - item.to.X)
                {
                    increment = (1, 1);
                }
                else if (item.from.Y - item.to.Y == -(item.from.X - item.to.X))
                {
                    increment = (1, -1);
                }

                for (int i = 0; i <= amt; i++)
                {
                    grid[start.X + increment.x * i, start.Y + increment.y * i]++;

                    if (grid[start.X + increment.x * i, start.Y + increment.y * i] == 2)
                        cnt++;
                }
            }

            return cnt;
        }

        static void printGrid(int[,] grid)
        {
            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0}", grid[j, i] == 0 ? "." : grid[j, i]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.ReadLine();
        }

    }
}
