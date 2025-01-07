using System.Linq;

namespace AdventOfCode2024.Days;

public class Day08 : Puzzle<int>
{
    public record Position(int x, int y);

    public override int First(string input)
    {
        var lines = input.ToLines();
        var map = lines.To2DArray();

        var antennaLocations = GetAntennaLocations(map);
        var antinodes = new HashSet<Position>();

        foreach (var antennas in antennaLocations.Values)
        {
            for (int i = 0; i < antennas.Count - 1; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var locations = Cross(antennas[i], antennas[j]);
                    foreach (var location in locations.Where(location => WithinMap(map, location)))
                    {
                        antinodes.Add(location);
                    }
                }
            }
        }

        return antinodes.Count;
    }
    public bool WithinMap(char[,] map, Position pos) => pos.x >= 0 && pos.y >= 0 && pos.x < map.GetLength(0) && pos.y < map.GetLength(1);

    private List<Position> Cross(Position position1, Position position2)
    {
        var diff = (position1.x - position2.x, position1.y - position2.y);

        return [new Position(position1.x + diff.Item1, position1.y + diff.Item2), new Position(position2.x - diff.Item1, position2.y - diff.Item2)];
    }

    private static Dictionary<char, List<Position>> GetAntennaLocations(char[,] map)
    {
        var antennas = new Dictionary<char, List<Position>>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] != '.')
                {
                    if (!antennas.ContainsKey(map[i, j]))
                        antennas.Add(map[i, j], []);

                    antennas[map[i, j]].Add(new Position(i, j));
                }
            }
        }
        return antennas;
    }

    private IEnumerable<Position> Cross2(Position position1, Position position2, char[,] map)
    {
        var diff = (position1.x - position2.x, position1.y - position2.y);

        int i = 0;
        while (true)
        {
            var nextPosition = new Position(position1.x + diff.Item1 * i, position1.y + diff.Item2 * i);
            if (WithinMap(map, nextPosition))
                yield return nextPosition;
            else
                break;

            i++;
        }

        i = 0;
        while (true)
        {
            var nextPosition = new Position(position2.x - diff.Item1 * i, position2.y - diff.Item2 * i);
            if (WithinMap(map, nextPosition))
                yield return nextPosition;
            else
                break;

            i++;
        }
    }

    public override int Second(string input)
    {
        var lines = input.ToLines();
        var map = lines.To2DArray();

        var antennaLocations = GetAntennaLocations(map);
        var antinodes = new HashSet<Position>();

        foreach (var antennas in antennaLocations.Values)
        {
            for (int i = 0; i < antennas.Count - 1; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var locations = Cross2(antennas[i], antennas[j], map);
                    antinodes.UnionWith(locations.ToHashSet());
                }
            }
        }

        return antinodes.Count;
    }
}

