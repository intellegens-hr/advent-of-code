using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Solutions.Task5
{
    public class VentMap
    {
        private int _x;
        private int _y;
        private int[,] _map;
        public int NumberOfIntersections;

        public VentMap(int x, int y)
        {
            _x = x;
            _y = y;
            _map = new int[y, x];
            NumberOfIntersections = 0;
        }

        public int AddLine(int x1, int y1, int x2, int y2, bool includeDiagonals)
        {
            // Vertical line
            if (x1 == x2)
            {
                if (y1 > y2)
                {
                    var temp = y1;
                    y1 = y2;
                    y2 = temp;
                }
                for (int i = y1; i <= y2; i++)
                {
                    if (x1 > x2)
                    {
                        var temp = x1;
                        x1 = x2;
                        x2 = temp;
                    }
                    if (_map[i, x1] == 1)
                    {
                        NumberOfIntersections++;
                    }
                    _map[i, x1]++;
                }
            }

            // Horizontal line
            if (y1 == y2)
            {
                if (x1 > x2)
                {
                    var temp = x1;
                    x1 = x2;
                    x2 = temp;
                }
                for (int i = x1; i <= x2; i++)
                {
                    if (_map[y1, i] == 1)
                    {
                        NumberOfIntersections++;
                    }
                    _map[y1, i]++;
                }
            }

            if (includeDiagonals)
            {
                if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
                {
                    // Diagonal

                    // Make point 1 one with lower Y
                    if (y1 > y2)
                    {
                        var temp = y1;
                        y1 = y2;
                        y2 = temp;
                        temp = x1;
                        x1 = x2;
                        x2 = temp;
                    }

                    // find the direction in which to "increment" x
                    var incr = x2 > x1 ? 1 : -1;
                    var myX = x1;

                    for (int i = y1; i <= y2; i++)
                    {
                        if (_map[i, myX] == 1)
                        {
                            NumberOfIntersections++;
                        }
                        _map[i, myX]++;
                        myX += incr;
                    }

                }
            }

            return NumberOfIntersections;
        }
    }
}
