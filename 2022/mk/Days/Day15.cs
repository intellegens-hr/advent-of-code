using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static AdventOfCode2022.Days.Day9;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days;

public class Day15 : Puzzle<BigInteger>
{
    public override int Day => 15;

    public override BigInteger First()
    {
        var lines = GetInputLines();
        IEnumerable<((int x, int y) sensor, (int x, int y) beacon)> sensors = lines
            .Select(x => x.Split(new[] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Select(y => int.Parse(y)).ToArray())
            .Select(x => ((x[0], x[1]), (x[2], x[3])))
            .ToList();

        var row = 2000000;
        var points = new HashSet<int>();

        foreach (var pair in sensors)
        {
            var dist = GetDistance(pair);
            var dif = Math.Abs(row - pair.sensor.y);
            if (dif <= dist)
            {
                points.Add(pair.sensor.x);
                for (int i = 1; i <= Math.Abs(dist - dif); i++)
                {
                    points.Add(pair.sensor.x - i);
                    points.Add(pair.sensor.x + i);
                }
            }
        }

        return points.Where(x => !sensors.Any(a => a.beacon == (x, row) || a.sensor == (x, row))).Count();
    }

    private int GetDistance(((int x, int y) sensor, (int x, int y) beacon) pair)
    {
        return Math.Abs(pair.sensor.x - pair.beacon.x) + Math.Abs(pair.sensor.y - pair.beacon.y);
    }

    public override BigInteger Second()
    {
        var lines = GetInputLines();
        IEnumerable<((int x, int y) sensor, (int x, int y) beacon)> sensors = lines
            .Select(x => x.Split(new[] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Select(y => int.Parse(y)).ToArray())
            .Select(x => ((x[0], x[1]), (x[2], x[3])))
            .ToList();

        var limit = 4000000; // 20;

        (int, int)[] incs = new[] { (1, 1), (1, -1), (-1, -1), (-1, 1) };

        foreach (var pair in sensors)
        {
            var dist = GetDistance(pair) + 1;

            for (int i = 0; i < dist; i++)
            {
                var xx = i;
                var yy = dist - i;

                foreach (var inc in incs)
                {
                    var nextX = (pair.sensor.x + inc.Item1 * xx);
                    if (nextX < 0 || nextX > limit)
                    {
                        continue;
                    }

                    var nextY = (pair.sensor.y + inc.Item2 * yy);
                    if (nextY < 0 || nextY > limit)
                    {
                        continue;
                    }

                    if (PointNotInsideAnyCircle((nextX, nextY), sensors))
                    {
                        return (BigInteger)nextX * 4000000 + (BigInteger)nextY;
                    }
                }
            }
        }

        return 0;
    }

    private bool PointNotInsideAnyCircle((int nextX, int nextY) point, IEnumerable<((int x, int y) sensor, (int x, int y) beacon)> sensors)
    {
        foreach (var item in sensors)
        {
            if (point == item.beacon || point == item.sensor)
            {
                return false;
            }
            if (GetDistance((point, item.sensor)) <= GetDistance(item))
            {
                return false;
            }
        }
        return true;
    }
}