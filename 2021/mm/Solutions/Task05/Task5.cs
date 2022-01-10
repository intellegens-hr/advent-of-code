using AdventOfCode2021.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions.Task5
{
    public static class Task5
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task5\\Input.txt");
            string line = string.Empty;
            var lines = new List<Line>();
            int maxX = 0;
            int maxY = 0;


            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var lineToDraw = new Line(line);
                    if (lineToDraw.X1 > maxX) maxX = lineToDraw.X1;
                    if (lineToDraw.X2 > maxX) maxX = lineToDraw.X2;
                    if (lineToDraw.Y1 > maxY) maxY = lineToDraw.Y1;
                    if (lineToDraw.Y2 > maxY) maxY = lineToDraw.Y2;

                    lines.Add(lineToDraw);
                }

            }

            reader.Close();

            var ventMap = new VentMap(maxX + 1, maxY + 1);

            foreach (var lineToDraw in lines)
            {
                ventMap.AddLine(lineToDraw.X1, lineToDraw.Y1, lineToDraw.X2, lineToDraw.Y2, false);
            }

            return ventMap.NumberOfIntersections;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task5\\Input.txt");
            string line = string.Empty;
            var lines = new List<Line>();
            int maxX = 0;
            int maxY = 0;


            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var lineToDraw = new Line(line);
                    if (lineToDraw.X1 > maxX) maxX = lineToDraw.X1;
                    if (lineToDraw.X2 > maxX) maxX = lineToDraw.X2;
                    if (lineToDraw.Y1 > maxY) maxY = lineToDraw.Y1;
                    if (lineToDraw.Y2 > maxY) maxY = lineToDraw.Y2;

                    lines.Add(lineToDraw);
                }

            }

            reader.Close();

            var ventMap = new VentMap(maxX + 1, maxY + 1);

            foreach (var lineToDraw in lines)
            {
                ventMap.AddLine(lineToDraw.X1, lineToDraw.Y1, lineToDraw.X2, lineToDraw.Y2, true);
            }

            return ventMap.NumberOfIntersections;
        }
    }
}
