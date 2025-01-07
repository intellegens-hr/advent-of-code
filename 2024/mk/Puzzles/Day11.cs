using System.Numerics;

namespace AdventOfCode2024.Days;

public class Day11 : Puzzle<long>
{
    readonly Dictionary<(int, BigInteger), long> _memo = [];

    public override long First(string input)
    {
        var line = input.Split(' ').Select(x => BigInteger.Parse(x)).ToList();

        return line.Sum(x => GetNumberOfArrangementsAfter(25, x));
    }

    public override long Second(string input)
    {
        var line = input.Split(' ').Select(x => BigInteger.Parse(x)).ToList();

        return line.Sum(x => GetNumberOfArrangementsAfter(75, x));
    }


    private long GetNumberOfArrangementsAfter(int v, BigInteger x)
    {
        if (v == 0)
            return 1;

        if (_memo.ContainsKey((v, x)))
            return _memo[(v, x)];

        var ret = ApplyRules(x).Sum(x => GetNumberOfArrangementsAfter(v - 1, x));

        _memo[(v, x)] = ret;

        return ret;

    }

    private IEnumerable<BigInteger> ApplyRules(BigInteger item)
    {
        if (item == 0)
        {
            yield return 1;
        }
        else
        {
            var str = item.ToString()!;
            if (str.Length % 2 == 0)
            {
                yield return BigInteger.Parse(str.Substring(0, str.Length / 2));
                yield return BigInteger.Parse(str.Substring(str.Length / 2, str.Length / 2));
            }
            else
            {
                yield return item * 2024;
            }
        }
    }
}

