using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Solutions.Task5
{
    public class Line
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        public Line(string fileLine)
        {
            var points = fileLine.Split(" -> ");
            var point1 = points[0].Split(',');
            X1 = Convert.ToInt32(point1[0]);
            Y1 = Convert.ToInt32(point1[1]);

            var point2 = points[1].Split(',');
            X2 = Convert.ToInt32(point2[0]);
            Y2 = Convert.ToInt32(point2[1]);
        }
    }
}
