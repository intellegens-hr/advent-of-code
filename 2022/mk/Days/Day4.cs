namespace AdventOfCode2022.Days;

public class Day4 : Puzzle<int>
{
    public override int Day => 4;

    public override int First()
    {
        var lines = GetInputLines();
        var sum = 0;

        foreach (var line in lines)
        {
            var a = line.Split(",").Select(a => a.Split("-")).ToArray();
            var blje = ((int.Parse(a[0][0]), int.Parse(a[0][1])), (int.Parse(a[1][0]), int.Parse(a[1][1])));

            if (blje.Item1.Item1 >= blje.Item2.Item1 && blje.Item1.Item2 <= blje.Item2.Item2)
            {
                sum++;
            }
            else if (blje.Item1.Item1 <= blje.Item2.Item1 && blje.Item1.Item2 >= blje.Item2.Item2)
            {
                sum++;
            }
        }

        return sum;

    }

    public override int Second()
    {
        var lines = GetInputLines();
        var sum = 0;

        foreach (var line in lines)
        {
            var a = line.Split(",").Select(a => a.Split("-")).ToArray();
            var blje = ((int.Parse(a[0][0]), int.Parse(a[0][1])), (int.Parse(a[1][0]), int.Parse(a[1][1])));

            if (blje.Item1.Item1 >= blje.Item2.Item1 && blje.Item1.Item2 <= blje.Item2.Item2)
            {
                sum++;
            }
            else if (blje.Item1.Item1 <= blje.Item2.Item1 && blje.Item1.Item2 >= blje.Item2.Item2)
            {
                sum++;
            }
            else if (blje.Item1.Item1 <= blje.Item2.Item1 && blje.Item1.Item2 >= blje.Item2.Item1)
            {
                sum++;
            }
            else if (blje.Item1.Item1 <= blje.Item2.Item2 && blje.Item1.Item2 >= blje.Item2.Item2)
            {
                sum++;
            }
        }

        return sum;
    }
}
