using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day13 : DayBase<long>
    {
        public override int Day => 13;

        public override long First()
        {
            var input = GetInputText();
            var coords = input.Split("\r\n\r\n")[0].Split("\r\n").Select(a => (int.Parse(a.Split(',')[0]), int.Parse(a.Split(',')[1]))).ToList();

            var commands = input.Split("\r\n\r\n")[1].Split("\r\n").Select(a => a.Split(" along ")[1]).Select(a => a.Split("=")).Select(a => (a[0], int.Parse(a[1])));


            foreach (var command in commands)
            {
                for (int i = 0; i < coords.Count; i++)
                {
                    (int, int) coord = coords[i];
                    if (command.Item1 == "x")
                    {
                        if (coord.Item1 < command.Item2)
                            continue;

                        var newCoord = (coord.Item1 - (coord.Item1 - command.Item2) * 2, coord.Item2);

                        if (!coords.Contains(newCoord))
                            coords.Add(newCoord);

                        coords.Remove(coord);
                        i--;
                    }
                    else if (command.Item1 == "y")
                    {
                        if (coord.Item2 < command.Item2)
                            continue;

                        var newCoord = (coord.Item1, coord.Item2 - (coord.Item2 - command.Item2) * 2);

                        if (!coords.Contains(newCoord))
                            coords.Add(newCoord);

                        coords.Remove(coord);
                        i--;
                    }
                }

            }

            printGrid(coords);



            return 0;
        }

        static void printGrid(IEnumerable<(int,int)> coords)
        {
            var max = (coords.Max(a => a.Item1), coords.Max(a => a.Item2));
            var grid = new int[max.Item1 + 1, max.Item2 + 1];

            for (int i = 0; i <= max.Item1; i++)
            {
                for (int j = 0; j <= max.Item2; j++)
                {
                    if (coords.Contains((i, j)))
                    {
                        grid[i, j] = 1;
                    }
                    else
                    {
                        grid[i, j] = 0;
                    }
                }
            }

            int rowLength = grid.GetLength(0);
            int colLength = grid.GetLength(1);

            for (int j = 0; j < colLength; j++)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    Console.Write(string.Format("{0}", grid[i,j] == 0 ? " " : grid[i,j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.ReadLine();
        }

        public override long Second()
        {
            return 0;
        }
    }
}
