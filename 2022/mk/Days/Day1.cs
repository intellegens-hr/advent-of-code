namespace AdventOfCode2022.Days;

public class Day1 : Puzzle<int>
{
    public override int Day => 1;

    public override int First()
    {
        var lines = GetInputLines();

        var max = int.MinValue;
        var sum = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                if (sum > max)
                {
                    max = sum;
                }
                sum = 0;
            }
            else
            {
                sum += int.Parse(line);
            }
        }

        return max;
    }

    public override int Second()
    {
        var calories = GetInputText()
            .Split("\r\n\r\n")
            .Select(a => a.Split("\r\n").Sum(a => int.Parse(a)))
            .OrderByDescending(x => x);

        return calories.Take(3).Sum();
    }
}
