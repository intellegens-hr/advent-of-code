namespace AdventOfCode2025.Days;

public class Day04 : Puzzle<int>
{
    private (int, int)[] DIRECTIONS =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (1, -1),
        (-1, 1),
        (-1, -1)
    ];

    public override int First(string input)
    {
        var lines = input.ToLines().To2DArray<char>();
        var result = 0;

        for (var i = 0; i < lines.GetLength(0); i++)
        {
            for (var j = 0; j < lines.GetLength(1); j++)
            {
                if (lines[i, j] == '.')
                    continue;

                var neighborCount = 0;
                foreach (var dir in DIRECTIONS)
                {
                    var ni = i + dir.Item1;
                    var nj = j + dir.Item2;
                    if (ni >= 0 && ni < lines.GetLength(0) && nj >= 0 && nj < lines.GetLength(1))
                    {
                        if (lines[ni, nj] == '@')
                            neighborCount++;
                    }
                }

                if (neighborCount < 4)
                    result++;
            }
        }

        return result;
    }

    public override int Second(string input)
    {
        var lines = input.ToLines().To2DArray<char>();
        var result = 0;
        var stillAccessible = true;

        while (stillAccessible)
        {
            stillAccessible = false;
            for (var i = 0; i < lines.GetLength(0); i++)
            {
                for (var j = 0; j < lines.GetLength(1); j++)
                {
                    if (lines[i, j] == '.')
                        continue;

                    var neighborCount = 0;
                    foreach (var dir in DIRECTIONS)
                    {
                        var ni = i + dir.Item1;
                        var nj = j + dir.Item2;
                        if (ni >= 0 && ni < lines.GetLength(0) && nj >= 0 && nj < lines.GetLength(1))
                        {
                            if (lines[ni, nj] == '@')
                                neighborCount++;
                        }
                    }

                    if (neighborCount < 4)
                    {
                        lines[i, j] = '.';
                        result++;
                        stillAccessible = true;
                    }
                }
            }

        }

        return result;
    }
}
