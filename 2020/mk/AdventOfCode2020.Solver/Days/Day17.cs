using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public struct Point3D
    {
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Day17 : PuzzleDay<long>
    {
        public override int Day => 17;

        public Dictionary<Point3D, bool> Coords { get; set; }

        public override long First()
        {
            var input = GetInputLines();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    var pt = new Point3D
                    {
                        X = i,
                        Y = j,
                        Z = 0
                    };
                    
                    Coords.Add(pt, input[j][i] == '#');
                }
            }

            return -1;
        }

        bool AreNeighbours(Point3D point1, Point3D point2)
        {
            if (GetNeighbours(point1).Contains(point2))
                return true;

            return false;
        }

        List<Point3D> GetNeighbours(Point3D point)
        {
            var neighbours = new List<Point3D>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        if (x != 0 && y != 0 && z != 0)
                        {
                            neighbours.Add(new Point3D(point.X + x, point.Y + y, point.Z + z));
                        }
                    }
                }
            }
            return neighbours;
        }

        public override long Second()
        {
            return -1;
        }
    }
}
