namespace AdventOfCode2022.Days;

public class Day3 : Puzzle<int>
{
    public override int Day => 3;

    int GetPriority(char c)
    {
        var val = (int)c;
        return val > 90 ? val - 96 : val - 38;
    }

    public override int First()
    {
        var lines = GetInputLines();
        var sum = 0;

        foreach (var line in lines)
        {
            var first = line.Substring(0, line.Length / 2);
            var second = line.Substring(line.Length / 2, line.Length / 2);

            foreach (var item in first)
            {
                if (second.Contains(item))
                {
                    sum += GetPriority(item);
                    break;
                }
            }
        }

        return sum;

    }

    public override int Second()
    {
        var lines = GetInputLines();
        var sum = 0;

        for (int i = 0; i < lines.Length; i += 3)
        {
            var first = lines[i];
            var second = lines[i + 1];
            var third = lines[i + 2];

            foreach (var item in first)
            {
                if (second.Contains(item) && third.Contains(item))
                {
                    sum += GetPriority(item);
                    break;
                }
            }
        }

        return sum;
    }
}
