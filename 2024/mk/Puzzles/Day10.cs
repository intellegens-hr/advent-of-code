

namespace AdventOfCode2024.Days;

public class Day10 : Puzzle<int>
{
    public override int First(string input)
    {
        var map = input.ToLines().To2DArray();
        var scores = new int[map.GetLength(0), map.GetLength(1)];
        var zeros = GetZeroLocations(map);

        return zeros.Sum(x => GetScore(x, map).Count);
    }

    private static List<(int x, int y)> GetZeroLocations(char[,] map)
    {
        var zeros = new List<(int x, int y)>();

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == '0')
                    zeros.Add((i, j));
            }
        }

        return zeros;
    }

    readonly Dictionary<(int, int), int> _memo = [];

    private HashSet<(int,int)> GetScore((int x, int y) location, char[,] map)
    {
        if (map[location.x, location.y] == '9')
            return new HashSet<(int, int)>([location]);

        //if (_memo.ContainsKey(location))
        //    return _memo[location];

        var score = GetNeighoursUpByOne(location, map).Select(x => GetScore(x, map)).Where(x => x.Count > 0);
        
        if (score.Any())
            return score.Aggregate((x, y) => x.Union(y).ToHashSet());

        //_memo.Add(location, score);

        return [];       

    }

    readonly List<(int x, int y)> _directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    private IEnumerable<(int x, int y)> GetNeighoursUpByOne((int x, int y) location, char[,] map)
    {
        foreach (var (x, y) in _directions)
        {
            var neighbourLocation = (location.x + x, location.y + y);
            if (WithinMap(neighbourLocation, map))
            {
                var current = map[location.x, location.y];
                var neighbour = map[neighbourLocation.Item1, neighbourLocation.Item2];
                if (current + 1 == neighbour)
                {
                    yield return neighbourLocation;
                }
            }
        }
    }

    public bool WithinMap((int x, int y) pos, char[,] map) => pos.x >= 0 && pos.y >= 0 && pos.x < map.GetLength(0) && pos.y < map.GetLength(1);


    //readonly Dictionary<(int, int), int> _memo = [];

    private int GetScoreDistinct((int x, int y) location, char[,] map)
    {
        if (map[location.x, location.y] == '9')
            return 1;

        //if (_memo.ContainsKey(location))
        //    return _memo[location];

        var score = GetNeighoursUpByOne(location, map).Sum(x => GetScoreDistinct(x, map));

        //_memo.Add(location, score);

        return score;

    } 
    public override int Second(string input)
    {
        var map = input.ToLines().To2DArray();
        var scores = new int[map.GetLength(0), map.GetLength(1)];
        var zeros = GetZeroLocations(map);

        return zeros.Sum(x => GetScoreDistinct(x, map));
    }
}

