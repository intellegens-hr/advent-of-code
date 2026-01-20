namespace AdventOfCode2025.Days;

public class Day02 : Puzzle<long>
{
    public override long First(string input)
    {
        var pairs = input.Split(",").Select(y => y.Split("-")).Select(x => (x[0], x[1])).ToList();
        long sum = 0;

        foreach (var pair in pairs)
        {
            var length = pair.Item1.Length;
            var firstPart = length > 1 ? long.Parse(pair.Item1[..(length / 2)]) : 0;

            var num = long.Parse(firstPart.ToString() + firstPart.ToString());
            while (num <= long.Parse(pair.Item2))
            {
                if (num >= long.Parse(pair.Item1))
                {
                    sum += num;
                }
                firstPart++;
                num = long.Parse(firstPart.ToString() + firstPart.ToString());
            }
        }
        return sum;
    }

    public override long Second(string input)
    {
        var pairs = input.Split(",").Select(y => y.Split("-")).Select(x => (x[0], x[1])).ToList();
        long sum = 0;

        foreach (var pair in pairs)
        {
            var length = pair.Item2.Length;
            var invalids = new HashSet<long>();

            Process(pair, invalids);

            if (pair.Item1.Length != pair.Item2.Length)
                Process(("0" + pair.Item1, pair.Item2), invalids);

            sum += invalids.Sum();
        }

        return sum;
    }

    private static void Process((string, string) pair, HashSet<long> invalids)
    {
        for (int i = 1; i <= pair.Item1.Length / 2; i++)
        {
            var firstPart = long.Parse(pair.Item1[..i]);
            var repeat = pair.Item1.Length / i;

            var num = Repeat(firstPart, repeat);
            while (num <= long.Parse(pair.Item2))
            {
                if (num >= long.Parse(pair.Item1))
                {
                    invalids.Add(num);
                }

                firstPart++;
                num = long.Parse(string.Join("", Enumerable.Repeat(firstPart.ToString(), repeat)));
            }
        }
    }

    private static long Repeat(long firstPart, int repeat)
    {
        return long.Parse(string.Join("", Enumerable.Repeat(firstPart.ToString(), repeat)));
    }
}
