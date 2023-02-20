using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day9 : Puzzle<int>
{
    public override int Day => 9;

    public enum Direction
    {
        R,
        L,
        U,
        D
    }
    public override int First()
    {
        var lines = GetInputLines();

        (int x, int y) GetIncrement(Direction d) => d switch
        {
            Direction.R => (0, 1),
            Direction.L => (0, -1),
            Direction.U => (-1, 0),
            Direction.D => (1, 0),
        };


        IEnumerable<(Direction direction, int amount)> instructions = lines.Select(l => l.Split(" ")).Select(l => ((Direction)Enum.Parse(typeof(Direction), l[0]), int.Parse(l[1])));

        var visitedTailPositions = new HashSet<(int, int)>();
        (int x, int y) currentPosition = (0, 0);
        (int x, int y) currentTailPosition = (0, 0);

        visitedTailPositions.Add(currentTailPosition);
        foreach (var (direction, amount) in instructions)
        {
            var (x, y) = GetIncrement(direction);

            for (int i = 0; i < amount; i++)
            {
                var nextPosition = (currentPosition.x + x, currentPosition.y + y);

                if (nextPosition.DistanceTo(currentTailPosition) >= 2)
                {
                    currentTailPosition = currentPosition;
                    visitedTailPositions.Add(currentTailPosition);
                }

                currentPosition = nextPosition;

                //Print(visitedTailPositions);
            }

        }

        return visitedTailPositions.Count;
    }

    private void Print(HashSet<(int x, int y)> visitedTailPositions)
    {
        var minX = visitedTailPositions.Min(a => a.x);
        var maxX = visitedTailPositions.Max(a => a.x);
        var minY = visitedTailPositions.Min(a => a.y);
        var maxY = visitedTailPositions.Max(a => a.y);

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                Console.Write(visitedTailPositions.Contains((i, j)) ? "# " : ". ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public override int Second()
    {
        var lines = GetInputLines();

        (int x, int y) GetIncrement(Direction d) => d switch
        {
            Direction.R => (0, 1),
            Direction.L => (0, -1),
            Direction.U => (1, 0),
            Direction.D => (-1, 0),
        };


        IEnumerable<(Direction direction, int amount)> instructions = lines.Select(l => l.Split(" ")).Select(l => ((Direction)Enum.Parse(typeof(Direction), l[0]), int.Parse(l[1])));

        var visitedTailPositions = new HashSet<(int, int)>();
        (int x, int y)[] currentPositions = Enumerable.Repeat((0, 0), 10).ToArray();

        visitedTailPositions.Add(currentPositions[9]);
        foreach (var (direction, amount) in instructions)
        {
            var (x, y) = GetIncrement(direction);

            for (int i = 0; i < amount; i++)
            {
                // move first item by increment
                // check for next item if distance to current item >= 2
                // if it is, move next item as well

                (currentPositions[0].x, currentPositions[0].y) = (currentPositions[0].x + x, currentPositions[0].y + y);

                for (int n = 1; n < 10; n++)
                {
                    var prevPosition = currentPositions[n - 1];
                    var currPosition = currentPositions[n];

                    if (prevPosition == currPosition)
                        break;

                    var follow = GetFollow(prevPosition, currPosition);

                    currentPositions[n] = (currentPositions[n].x + follow.x, currentPositions[n].y + follow.y);
                }
                visitedTailPositions.Add(currentPositions[9]);

                //Print(visitedTailPositions);
            }

        }

        return visitedTailPositions.Count;

    }

    public (int x, int y) GetFollow((int x, int y) a, (int x, int y) b)
    {
        if (a.DistanceTo(b) < 2)
            return (0, 0);

        var xDif = a.x - b.x;
        var yDif = a.y - b.y;

        return (Math.Sign(xDif), Math.Sign(yDif));
    }
}
