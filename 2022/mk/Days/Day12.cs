using Microsoft.Win32;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static AdventOfCode2022.Days.Day9;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days;

public class Day12 : Puzzle<int>
{
    public override int Day => 12;

    public override int First()
    {
        var lines = GetInputLines();

        (int x, int y) start = (-1, -1);
        (int x, int y) end = (-1, -1);

        for (int i = 0; i < lines[0].Length; i++)
        {
            for (int j = 0; j < lines.Length; j++)
            {
                if (lines[j][i] == 'S')
                {
                    start = (i, j);
                }
                else if (lines[j][i] == 'E')
                {
                    end = (i, j);
                }
            }
        }

        var visited = new Dictionary<(int, int), int>
        {
            { start, 1 }
        };

        Move(lines, visited, start);

        return 0;
    }

    private void Move(string[] lines, Dictionary<(int, int), int> visited, (int x, int y) current)
    {
        var directions = new[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        IEnumerable<(int x, int y)> nexts = directions.Select(direction => (current.x + direction.Item1, current.y + direction.Item2));

        foreach (var next in nexts)
        {
            if (IsValidIndex(lines, next.x, next.y) && (Math.Abs(lines[current.y][current.x] - lines[next.y][next.x]) <= 1 || lines[current.y][current.x] == 'S'))
            {
                if (!visited.ContainsKey(next))
                {
                    visited.Add(next, visited[current] + 1);
                    Move(lines, visited, next);
                }
                else if (visited[next] > visited[current])
                {
                    visited[next] = visited[current] + 1;
                }
            }

        }
    }
    public bool IsValidIndex(string[] lines, int x, int y)
    {
        return x >= 0 && x < lines[0].Length && y >= 0 && y < lines.Length;
    }

    public override int Second()
    {
        var lines = GetInputLines();
        return 0;
    }

}
