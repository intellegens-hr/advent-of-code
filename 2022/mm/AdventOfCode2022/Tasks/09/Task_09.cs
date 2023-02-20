using AdventOfCode2022.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._09
{
    public class Point
    {
        public int X;
        public int Y;

        public Point(int y, int x)
        {
            Y = y;
            X = x;
        }
    }

    public class Task_09
    {
        public static Point Transform(Point h, Point t)
        {
            if (h.X - t.X == 2)
            {
                t.X++;
                if (h.Y > t.Y) t.Y++;
                if (h.Y < t.Y) t.Y--;
            }

            if (h.X - t.X == -2)
            {
                t.X--;
                if (h.Y > t.Y) t.Y++;
                if (h.Y < t.Y) t.Y--;
            }

            if (h.Y - t.Y == 2)
            {
                t.Y++;
                if (h.X > t.X) t.X++;
                if (h.X < t.X) t.X--;
            }

            if (h.Y - t.Y == -2)
            {
                t.Y--;
                if (h.X > t.X) t.X++;
                if (h.X < t.X) t.X--;
            }

            return t;
        }

        public static long PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\09\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\09\Example.txt");
            string line = String.Empty;
            List<(int, int)> positions = new();

            var h = (0, 0);
            var t = (0, 0);

            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(' ');
                var value = Convert.ToInt32(parts[1]);

                for (int i = 0; i < value; i++)
                {
                    // Console.WriteLine($"h = {h.ToString()}, t = {t.ToString()}");
                    switch (parts[0])
                    {
                        case "R":
                            if (h.Item2 > t.Item2)
                            {
                                t.Item2++;
                                if (h.Item1 != t.Item1) t.Item1 = h.Item1;
                            }
                            h.Item2++;
                            break;

                        case "L":
                            if (h.Item2 < t.Item2)
                            {
                                t.Item2--;
                                if (h.Item1 != t.Item1) t.Item1 = h.Item1;
                            }
                            h.Item2--;
                            break;

                        case "U":
                            if (h.Item1 < t.Item1)
                            {
                                t.Item1--;
                                if (t.Item2 != h.Item2) t.Item2 = h.Item2;
                            }
                            h.Item1--;
                            break;

                        case "D":
                            if (h.Item1 > t.Item1)
                            {
                                t.Item1++;
                                if (h.Item2 != t.Item2) t.Item2 = h.Item2;
                            }
                            h.Item1++;
                            break;

                        default:
                            break;
                    }

                    if (!positions.Contains(t)) positions.Add(t);
                }
            }

            return positions.Count;
        }
        public static int PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\09\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\09\Example.txt");
            string line = String.Empty;
            List<(int, int)> positions = new();

            List<Point> rope = new();
            for (int i = 0; i < 10; i++) rope.Add(new Point(0, 0));

            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(' ');
                var value = Convert.ToInt32(parts[1]);

                for (int i = 0; i < value; i++)
                {
                    // Console.WriteLine($"h = {h.ToString()}, t = {t.ToString()}");
                    switch (parts[0])
                    {
                        case "R":
                            rope[0].X++;
                            break;

                        case "L":
                            rope[0].X--;
                            break;

                        case "U":
                            rope[0].Y--;
                            break;

                        case "D":
                            rope[0].Y++;
                            break;

                        default:
                            break;
                    }

                    for (int j = 1; j < 10; j++)
                    {
                        rope[j] = Transform(rope[j - 1], rope[j]);
                    }

                    if (!positions.Contains((rope[9].Y, rope[9].X))) positions.Add((rope[9].Y, rope[9].X));
                }
            }

            return positions.Count;
        }
    }
}
