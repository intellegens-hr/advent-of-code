using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Solutions
{
    public static class Task13
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task13\\Input.txt");

            var points = ReadPoints(reader);
            var folds = ReadFolds(reader);

            FoldX(points, 655);

            reader.Close();
            return points.Count;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task13\\Input.txt");

            var points = ReadPoints(reader);
            var folds = ReadFolds(reader);

            foreach (var fold in folds)
            {
                if (fold.Item1) FoldX(points,fold.Item2); else FoldY(points, fold.Item2);
            }

            //find max x and max y
            var maxX = -1;
            var maxY = -1;
            foreach (var point in points)
            {
                if (point.Item1 > maxX) maxX = point.Item1;
                if (point.Item2 > maxY) maxY = point.Item2;
            }

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (points.Contains((x, y))) Console.Write("*"); else Console.Write(" ");
                }
                Console.WriteLine();
            }

            reader.Close();
            return points.Count;
        }


        public static List<(int, int)> ReadPoints(StreamReader reader)
        {
            var points = new List<(int, int)>();
            var line = "INITIAL VALUE";
            while (!string.IsNullOrWhiteSpace(line))
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(',');
                    points.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                }
            }
            return points;
        }

        public static List<(bool, int)> ReadFolds(StreamReader reader)
        {
            List<(bool, int)> folds = new List<(bool, int)>();
            var line = "INITIAL VALUE";
            while (!string.IsNullOrWhiteSpace(line))
            {
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split('=');
                    var parts2 = parts[0].Split(' ');

                    if (parts2[2] == "x") folds.Add((true, int.Parse(parts[1]))); else folds.Add((false, int.Parse(parts[1])));
                }
            }
            return folds;
        }


        public static void FoldY(List<(int, int)> points, int y)
        {
            var pointsCopy = new List<(int, int)>();
            pointsCopy.AddRange(points);
            foreach (var point in pointsCopy)
            {
                if (point.Item2 > y)
                {
                    var newPoint = (point.Item1, y - (point.Item2 - y));
                    if (!points.Contains(newPoint)) points.Add(newPoint);

                    points.Remove(point);
                }
            }
        }

        public static void FoldX(List<(int, int)> points, int x)
        {
            var pointsCopy = new List<(int, int)>();
            pointsCopy.AddRange(points);
            foreach (var point in pointsCopy)
            {
                if (point.Item1 > x)
                {
                    var newPoint = (x - (point.Item1 - x), point.Item2);
                    if (!points.Contains(newPoint)) points.Add(newPoint);

                    points.Remove(point);
                }
            }
        }
    }
}
