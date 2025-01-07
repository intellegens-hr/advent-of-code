namespace AdventOfCode2024.Days;

public class Day06 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines();
        var map = lines.To2DArray();

        var position = GetStart(map);
        var visited = GetVisitedFields(map, position);

        return visited.Count;
    }

    private static HashSet<(int, int)> GetVisitedFields(char[,] map, (int x, int y) position)
    {
        var visited = new HashSet<(int, int)>();
        var directions = new[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var directionsIndex = 0;

        while (position.x + directions[directionsIndex].Item1 >= 0 &&
            position.x + directions[directionsIndex].Item1 < map.GetLength(0) &&
            position.y + directions[directionsIndex].Item2 >= 0 &&
            position.y + directions[directionsIndex].Item2 < map.GetLength(1))
        {
            visited.Add(position);

            if (map[position.x + directions[directionsIndex].Item1, position.y + directions[directionsIndex].Item2] == '#')
                directionsIndex++;

            directionsIndex %= 4;

            position.x += directions[directionsIndex].Item1;
            position.y += directions[directionsIndex].Item2;

            //Console.WriteLine(position);
        }
        visited.Add(position);

        return visited;
    }

    private (int x, int y) GetStart(char[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == '^')
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    public override int Second(string input)
    {
        var lines = input.ToLines();
        var map = lines.To2DArray();

        var startPosition = GetStart(map);
        var visited = GetVisitedFields(map, startPosition);

        var count = 0;
        foreach (var visitedPosition in visited)
        {
            if (IsLoopWithObstacleOn(visitedPosition, map, startPosition))
            {
                count++;
            }
        }

        return count;
    }

    public enum Direction { Up, Right, Down, Left };

    public bool WithinMap((int x, int y) pos, char[,] map) => pos.x >= 0 && pos.y >= 0 && pos.x < map.GetLength(0) && pos.y < map.GetLength(1);

    public (int x, int y) Move((int x, int y) pos, Direction dir) => dir switch
    {
        Direction.Up => ((pos.x - 1, pos.y)),
        Direction.Right => ((pos.x, pos.y + 1)),
        Direction.Down => ((pos.x + 1, pos.y)),
        Direction.Left => ((pos.x, pos.y - 1)),
        _ => throw new NotImplementedException(),
    };

    public Direction Turn(Direction dir) => (Direction)(((int)dir + 1) % 4);

    private bool IsLoopWithObstacleOn((int x, int y) obstaclePosition, char[,] mapInput, (int x, int y) startingPosition)
    {
        var map = mapInput.ToArray();

        map[obstaclePosition.x, obstaclePosition.y] = '#';

        var visited = new HashSet<(int, int, Direction)>();
        var direction = Direction.Up;
        var position = startingPosition;

        while (true)
        {
            // if this position was already visited in the same direction, it's a loop
            if (visited.Contains((position.x, position.y, direction)))
                return true;

            visited.Add((position.x, position.y, direction));

            var nextPosition = Move(position, direction);
            if (!WithinMap(nextPosition, map))
                return false;

            if (map[nextPosition.x, nextPosition.y] == '#')
                direction = Turn(direction);
            else
                position = nextPosition;
        }
    }
}
