using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static AdventOfCode2022.Days.Day9;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days;

public class Directions
{
    public static (int x, int y) Up { get; } = (0, -1);
    public static (int x, int y) Down { get; } = (0, 1);
    public static (int x, int y) Left { get; } = (-1, 0);
    public static (int x, int y) Right { get; } = (1, 0);
}

public class Day14 : Puzzle<int>
{
    public override int Day => 14;

    public override int First()
    {
        var lines = GetInputLines();

        HashSet<(int x, int y)> grid = ParseLines(lines);
        HashSet<(int x, int y)> sandGrid = new();
        (int x, int y) start = (500, 0);
        int end = grid.Max(a => a.y);

        var isBlocked = ((int x, int y) a) => grid.Contains(a) || sandGrid.Contains(a);

        while (true)
        {
            var sand = start;
            while (true)
            {
                if (sand.y >= end)
                {
                    return sandGrid.Count;
                }

                (int x, int y) next = (sand.x + Directions.Down.x, sand.y + Directions.Down.y);
                if (!isBlocked(next))
                {
                    sand = next;
                    continue;
                }
                (int x, int y) left = (next.x + Directions.Left.x, next.y + Directions.Left.y);
                if (!isBlocked(left))
                {
                    sand = left;
                    continue;
                }
                (int x, int y) right = (next.x + Directions.Right.x, next.y + Directions.Right.y);
                if (!isBlocked(right))
                {
                    sand = right;
                    continue;
                }

                sandGrid.Add(sand);
                break;
            }
        }

        PrintGrid(grid);
        return 0;
    }
    private void PrintGrid(HashSet<(int x, int y)> grid)
    {
        var row = "";
        for (int j = grid.Min(a => a.y); j <= grid.Max(a => a.y); j++)
        {
            for (int i = grid.Min(a => a.x); i <= grid.Max(a => a.x); i++)
            {
                if (grid.Contains((i, j)))
                {
                    row += "#";
                }
                else
                {
                    row += ".";
                }
            }

            row += Environment.NewLine;
        }
        Console.WriteLine(row);
    }

    private HashSet<(int, int)> ParseLines(string[] lines)
    {
        var grid = new HashSet<(int, int)>();
        foreach (var line in lines)
        {
            var commands = line.Split(" -> ").Select(x => x.Split(",").Select(y => int.Parse(y)));

            for (int i = 0; i < commands.Count() - 1; i++)
            {
                var command = commands.ElementAt(i);
                var nextCommand = commands.ElementAt(i + 1);

                (int x, int y) xyFrom = (command.ElementAt(0), command.ElementAt(1));
                (int x, int y) xyTo = (nextCommand.ElementAt(0), nextCommand.ElementAt(1));

                var xSign = Math.Sign(xyTo.x - xyFrom.x);
                var ySign = Math.Sign(xyTo.y - xyFrom.y);

                if (xyFrom.y == xyTo.y)
                {
                    for (int x = xyFrom.x; x != xyTo.x; x += xSign)
                    {
                        //Console.WriteLine((x, xyFrom.y));
                        grid.Add((x, xyFrom.y));
                    }
                    //Console.WriteLine((xyTo.x, xyFrom.y));
                    grid.Add((xyTo.x, xyFrom.y));
                }
                else if (xyFrom.x == xyTo.x)
                {
                    for (int y = xyFrom.y; y != xyTo.y; y += ySign)
                    {
                        //Console.WriteLine((xyFrom.x, y));
                        grid.Add((xyFrom.x, y));
                    }
                    //Console.WriteLine((xyFrom.x, xyTo.y));
                    grid.Add((xyFrom.x, xyTo.y));
                    //grid.Add((xyTo.y, xyFrom.y));
                }

            }
        }

        return grid;
    }

    public override int Second()
    {
        var lines = GetInputLines().ToList();

        HashSet<(int x, int y)> grid = ParseLines(lines.ToArray());
        HashSet<(int x, int y)> sandGrid = new();
        (int x, int y) start = (500, 0);
        int end = grid.Max(a => a.y) + 2;

        for (int i = -1000; i < 1000; i++)
        {
            grid.Add((i, end));
        }

        var isBlocked = ((int x, int y) a) => grid.Contains(a) || sandGrid.Contains(a);

        while (true)
        {
            var sand = start;
            while (true)
            {

                (int x, int y) next = (sand.x + Directions.Down.x, sand.y + Directions.Down.y);
                if (!isBlocked(next))
                {
                    sand = next;
                    continue;
                }
                (int x, int y) left = (next.x + Directions.Left.x, next.y + Directions.Left.y);
                if (!isBlocked(left))
                {
                    sand = left;
                    continue;
                }
                (int x, int y) right = (next.x + Directions.Right.x, next.y + Directions.Right.y);
                if (!isBlocked(right))
                {
                    sand = right;
                    continue;
                }

                sandGrid.Add(sand);
                if (sand == start)
                {
                    return sandGrid.Count;
                }
                break;
            }
        }

        PrintGrid(grid);
        return 0;
    }
}