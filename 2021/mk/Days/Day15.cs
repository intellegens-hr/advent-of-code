using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day15 : DayBase<long>
    {
        public override int Day => 15;

        public override long First()
        {
            var input = GetInputLines();
            var grid = new int[input[0].Length, input.Length];
            var dist = new Dictionary<(int x, int y), (long dist, (int x, int y)? prev)>();
            var visited = new List<(int, int)>();
            var unvisited = new List<(int, int)>();

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    grid[x, y] = int.Parse(input[y][x].ToString());
                    dist[(x, y)] = (long.MaxValue, null);
                    unvisited.Add((x, y));
                }
            }

            return Solve(grid, dist, visited, unvisited);
        }

        public override long Second()
        {
            var input = GetInputLines();
            var grid = new int[input[0].Length * 5, input.Length * 5];
            var dist = new Dictionary<(int x, int y), (long dist, (int x, int y)? prev)>();
            var visited = new List<(int, int)>();
            var unvisited = new List<(int, int)>();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int y = 0; y < input.Length; y++)
                    {
                        for (int x = 0; x < input[y].Length; x++)
                        {
                            grid[input[y].Length * i + x, input.Length * j + y] = (int.Parse(input[y][x].ToString()) + i + j) >= 10 ? (int.Parse(input[y][x].ToString()) + i + j) - 9 : (int.Parse(input[y][x].ToString()) + i + j);
                            dist[(input[y].Length * i + x, input.Length * j + y)] = (long.MaxValue, null);
                            unvisited.Add((input[y].Length * i + x, input.Length * j + y));
                        }
                    }
                }
            }

            var blje = Solve(grid, dist, visited, unvisited);

            for (int j = 0; j < 50; j++)
            {
                for (int i = 0; i < 50; i++)
            {
                    Console.Write(dist[(i,j)].dist + "\t");
                }
                Console.WriteLine();
            }

            return blje;
        }

        private long Solve(int[,] grid, Dictionary<(int x, int y), (long dist, (int x, int y)? prev)> dist, List<(int, int)> visited, List<(int, int)> unvisited)
        {
            dist[(0, 0)] = (0, null);

            while (unvisited.Any())
            {
                var vert = unvisited.First(); //.OrderBy(x => dist[x].dist).First();

                var neighbours = GetAdjacent(grid, vert);

                foreach (var neighbour in neighbours.Where(v => !visited.Contains(v)))
                {
                    if (dist[neighbour].dist == long.MaxValue || dist[neighbour].dist > grid[neighbour.Item1, neighbour.Item2] + dist[vert].dist)
                    {
                        var blje = grid[neighbour.Item1, neighbour.Item2] + dist[vert].dist;
                        dist[neighbour] = (blje, vert);

                        var aa = unvisited.FirstOrDefault(a => dist[a].dist >= blje);
                        var oldIndex = unvisited.IndexOf(neighbour);
                        var newIndex = unvisited.IndexOf(aa);

                        unvisited.RemoveAt(oldIndex);
                        if (newIndex > oldIndex)
                            newIndex--;
                        unvisited.Insert(newIndex, neighbour);
                    }
                }

                visited.Add(vert);
                unvisited.Remove(vert);
            }

            return dist[(grid.GetLength(0) - 1, grid.GetLength(1) - 1)].dist;
        }

        private List<(int, int)> GetAdjacent(int[,] grid, (int x, int y) vert)
        {
            var increments = new (int X, int Y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var vertices = new List<(int, int)>();

            foreach (var inc in increments)
            {
                if (vert.x + inc.X >= 0 && vert.x + inc.X < grid.GetLength(0) && vert.y + inc.Y >= 0 && vert.y + inc.Y < grid.GetLength(1))
                {
                    vertices.Add((vert.x + inc.X, vert.y + inc.Y));
                }
            }

            return vertices;
        }
    }
}
