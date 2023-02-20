using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022;

public static class Utils
{
    public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        int max = source.Max(l => l.Count());

        var result = new T[source.Count(), max];

        for (int i = 0; i < source.Count(); i++)
        {
            for (int j = 0; j < source.ElementAt(i).Count(); j++)
            {
                result[i, j] = source.ElementAt(i).ElementAt(j);
            }
        }

        return result;
    }


    public static double DistanceTo(this (int X, int Y) from, (int X, int Y) to)
    {
        return Math.Sqrt(Math.Pow((from.X - to.X), 2) + Math.Pow((from.Y - to.Y), 2));
    }
}
