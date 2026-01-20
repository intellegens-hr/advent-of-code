using System.Drawing;

namespace AdventOfCode2025.Days;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public record Point(long X, long Y)
{
    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
public record Line(Point Start, Point End)
{
    public override string ToString()
    {
        return $"{Start} -> {End}";
    }
}

public class Day09 : Puzzle<long>
{
    public override long First(string input)
    {
        var positions = input.ToLines().Select(x => x.Split(",").Select(long.Parse).ToList()).Select(x => new Point(x[0], x[1])).ToList();

        long maxArea = 0;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                long newArea = (1 + Math.Abs(positions[i].X - positions[j].X)) * (1 + Math.Abs(positions[i].Y - positions[j].Y));
                if (newArea > maxArea)
                    maxArea = newArea;
            }
        }

        return maxArea;
    }

    public override long Second(string input)
    {
        var positions = input.ToLines().Select(x => x.Split(",").Select(long.Parse).ToArray()).Select(x => new Point(x[0], x[1])).ToArray();
        var boundaryPositions = GetBoundary(positions);
        var boundaryLines = GetLines(boundaryPositions).ToArray();

        long maxArea = 0;
        for (int i = 0; i < positions.Length - 1; i++)
        {
            for (int j = i + 1; j < positions.Length; j++)
            {
                long newArea = (1 + Math.Abs(positions[i].X - positions[j].X)) * (1 + Math.Abs(positions[i].Y - positions[j].Y));
                if (newArea > maxArea)
                {
                    var sides = GetFourSquareSides(positions[i], positions[j]);

                    if (CrossesAnyLine(sides, boundaryLines))
                        continue;

                    maxArea = newArea;
                }
            }
        }

        return maxArea;
    }

    private Dictionary<Line, bool> memo = new();
    private bool CrossesAnyLine(Line[] sides, Line[] boundaryLines)
    {
        foreach (var side in sides)
        {
            if (memo.TryGetValue(side, out var crosses))
                return crosses;

            if (side.Start.X == side.End.X)
            {
                // vertical line
                var crossingLines = boundaryLines.Count(line => line.Start.Y == line.End.Y && line.Start.X < side.Start.X && line.End.X > side.Start.X && line.Start.Y > side.Start.Y && line.Start.Y < side.End.Y);
                if (crossingLines > 0)
                {
                    memo.Add(side, true);
                    return true;
                }
            }
            else
            {
                // horizontal line
                var crossingLines = boundaryLines.Count(line => line.Start.X == line.End.X && line.Start.Y < side.Start.Y && line.End.Y > side.Start.Y && line.Start.X > side.Start.X && line.Start.X < side.End.X);
                if (crossingLines > 0)
                {
                    memo.Add(side, true);
                    return true;
                }
            }
        }
        return false;
    }

    private Point[] GetBoundary(Point[] positions)
    {
        var boundaryPoints = new Point[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            var current = positions[i];
            var next = positions[(i + 1) % positions.Length];
            var third = positions[(i + 2) % positions.Length];
            if (current.X == next.X)
            {
                // vertical line
                if (next.Y < current.Y)
                {
                    // going up
                    if (third.X > next.X)
                    {
                        // right turn
                        boundaryPoints[i] = new Point(next.X - 1, next.Y - 1); //
                    }
                    else
                    {
                        // left turn
                        boundaryPoints[i] = new Point(next.X - 1, next.Y + 1); //
                    }
                }
                else
                {
                    // going down
                    if (third.X < next.X)
                    {
                        // right turn
                        boundaryPoints[i] = new Point(next.X + 1, next.Y + 1); //
                    }
                    else
                    {
                        // left turn
                        boundaryPoints[i] = new Point(next.X + 1, next.Y - 1); //
                    }
                }
            }
            else
            {
                // horizontal line
                if (next.X > current.X)
                {
                    // going right
                    if (third.Y > next.Y)
                    {
                        // right turn
                        boundaryPoints[i] = new Point(next.X + 1, next.Y - 1); //
                    }
                    else
                    {
                        // left turn
                        boundaryPoints[i] = new Point(next.X - 1, next.Y - 1); //
                    }
                }
                else
                {
                    // going left
                    if (third.Y < next.Y)
                    {
                        // right turn
                        boundaryPoints[i] = new Point(next.X - 1, next.Y + 1); //
                    }
                    else
                    {
                        // left turn
                        boundaryPoints[i] = new Point(next.X + 1, next.Y + 1); //
                    }
                }
            }
        }
        return boundaryPoints;
    }

    private IEnumerable<Line> GetLines(Point[] positionsInput)
    {
        var positions = positionsInput.Append(positionsInput[0]).ToArray();
        for (int i = 0; i < positions.Length - 1; i++)
        {
            if (positions[i].X == positions[i + 1].X)
            {
                if (positions[i].Y > positions[i + 1].Y)
                {
                    yield return new Line(positions[i + 1], positions[i]);
                }
                else
                {
                    yield return new Line(positions[i], positions[i + 1]);
                }
            }
            else
            {
                if (positions[i].X > positions[i + 1].X)
                {
                    yield return new Line(positions[i + 1], positions[i]);
                }
                else
                {
                    yield return new Line(positions[i], positions[i + 1]);
                }
            }
        }
    }

    private Line[] GetFourSquareSides(Point point1, Point point2)
    {
        Point[] points =
        [
            new Point(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y)),
            new Point(Math.Min(point1.X, point2.X), Math.Max(point1.Y, point2.Y)),
            new Point(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y)),
            new Point(Math.Max(point1.X, point2.X), Math.Min(point1.Y, point2.Y)),
        ];

        return GetLines(points).ToArray();

    }
}