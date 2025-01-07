namespace AdventOfCode2024.Days;

public class Day04 : Puzzle<int>
{
    private (int x, int y)[] directions = [(1, 1), (-1, -1), (1, -1), (-1, 1), (0, 1), (0, -1), (1, 0), (-1, 0)];

    public override int First(string input)
    {
        var lines = input.ToLines().To2DArray();
        int count = 0;

        for (var i = 0; i< lines.GetLength(0); i++)
        {
            for (var j = 0; j < lines.GetLength(1); j++)
            {
                count += CountXmasOn(i, j, lines);
            }
        }


        return count;
    }

    private int CountXmasOn(int i, int j, char[,] lines)
    {
        if (lines[i, j] != 'X')
            return 0;

        var count = 0;
        foreach (var item in directions)
        {
            var chunk = TakeChunk(i, j, item, lines, 4);
            if (chunk == "XMAS")
                count++;
            else
                Console.WriteLine(chunk);
        }
        return count;
    }

    private string TakeChunk(int i, int j, (int x, int y) direction, char[,] lines, int length)
    {
        var str = lines[i, j].ToString();
        for (int ii = 0; ii < length - 1; ii++)
        {
            i += direction.x;
            j += direction.y;

            if (i < 0 || i >= lines.GetLength(0))
                return str;

            if (j < 0 || j >= lines.GetLength(1))
                return str;

            str += lines[i, j];
        }
        return str;
    }

    public override int Second(string input)
    {
        var lines = input.ToLines().To2DArray();
        int count = 0;

        for (var i = 1; i < lines.GetLength(0) - 1; i++)
        {
            for (var j = 1; j < lines.GetLength(1) - 1; j++)
            {
                if (
                    lines[i, j] == 'A' && 
                    ((lines[i - 1, j - 1] == 'M' && lines[i + 1, j + 1] == 'S') || (lines[i - 1, j - 1] == 'S' && lines[i + 1, j + 1] == 'M')) &&
                    ((lines[i - 1, j + 1] == 'M' && lines[i + 1, j - 1] == 'S') || (lines[i - 1, j + 1] == 'S' && lines[i + 1, j - 1] == 'M')))
                    count++;
            }
        }


        return count;
    }

}
