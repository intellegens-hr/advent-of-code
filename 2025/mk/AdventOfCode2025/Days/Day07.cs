namespace AdventOfCode2025.Days;

public class Day07 : Puzzle<int, long>
{
    public override int First(string input)
    {
        var lines = input.ToLines().To2DArray();
        int total = 0;

        for (int i = 1; i < lines.GetLength(0); i++)
        {
            for (int j = 0; j < lines.GetLength(1); j++)
            {
                if (lines[i,j] == '^' && lines[i - 1, j] == '|')
                {
                    lines[i, j - 1] = '|';
                    lines[i, j + 1] = '|';
                    total++;
                }
                else if (lines[i - 1, j] == 'S' || lines[i - 1, j] == '|')
                {
                    lines[i, j] = '|';
                }

            }
        }
        return total;
    }

    public override long Second(string input)
    {
        var lines = input.ToLines().To2DArray();
        var counts = new long[lines.GetLength(0), lines.GetLength(1)];

        for (int i = 1; i < lines.GetLength(0); i++)
        {
            for (int j = 0; j < lines.GetLength(1); j++)
            {
                if (lines[i, j] == '^' && lines[i - 1, j] == '|')
                {
                    lines[i, j - 1] = '|';
                    counts[i, j - 1] += counts[i - 1, j];

                    lines[i, j + 1] = '|';
                    counts[i, j + 1] += counts[i - 1, j];
                }
                else if (lines[i - 1, j] == 'S')
                {
                    lines[i, j] = '|';
                    counts[i, j] = 1;
                }
                else if (lines[i - 1, j] == '|')
                {
                    lines[i, j] = '|';
                    counts[i, j] += counts[i - 1, j];
                }

            }
        }

        return counts.GetRow(counts.GetLength(0) - 1).Sum();
    }
}