namespace AdventOfCode2025.Days;

public class Day11 : Puzzle<long>
{
    public override long First(string input)
    {
        var positions = input.ToLines().Select(x => x.Split(": ")).ToDictionary(y => y[0], y => y[1].Split(" ").ToHashSet());

        return GetTotal(positions, [], "you", "out");
    }

    private long GetTotal(Dictionary<string, HashSet<string>> positions, Dictionary<string, long> _memo, string from, string to)
    {
        if (from == to)
            return 1;

        if (_memo.TryGetValue(from, out long value))
            return value;

        if (!positions.TryGetValue(from, out var nextPositions))
            return 0;

        var result = nextPositions.Sum(x => GetTotal(positions, _memo, x, to));
        _memo[from] = result;
        return result;
    }

    public override long Second(string input)
    {
        var positions = input.ToLines().Select(x => x.Split(": ")).ToDictionary(y => y[0], y => y[1].Split(" ").ToHashSet());

        var total1 = GetTotal(positions, [], "svr", "fft");
        var total2 = GetTotal(positions, [], "fft", "dac");
        var total3 = GetTotal(positions, [], "dac", "out");

        var total4 = GetTotal(positions, [], "svr", "dac");
        var total5 = GetTotal(positions, [], "dac", "fft");
        var total6 = GetTotal(positions, [], "fft", "out");

        return total1 * total2 * total3 + total4 * total5 * total6;
    }


}