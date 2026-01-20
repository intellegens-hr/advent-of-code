namespace AdventOfCode2025.Days;

public class Day05 : Puzzle<int, long>
{
    public override int First(string input)
    {
        var lines = input.ToLines();

        List<(long, long)> ranges = [];
        List<long> ids = [];

        var i = 0;
        for (; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
                break;

            ranges.Add(lines[i].Split('-').Select(long.Parse).ToTuple());
        }
        for (; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
                continue;

            ids.Add(long.Parse(lines[i]));
        }

        var result = 0;
        foreach (var id in ids)
        {
            foreach (var range in ranges)
            {
                if (id >= range.Item1 && id <= range.Item2)
                {
                    result++;
                    break;
                }
            }
        }


        return result;
    }

    public override long Second(string input)
    {
        var lines = input.ToLines();

        List<(long, long)> ranges = [];
        List<long> ids = [];

        var i = 0;
        for (; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
                break;

            ranges.Add(lines[i].Split('-').Select(long.Parse).ToTuple());
        }

        ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        for (int j = 1; j < ranges.Count; j++)
        {
            if (ranges[j].Item1 <= ranges[j - 1].Item2)
            {
                var newRange = (ranges[j - 1].Item1, Math.Max(ranges[j - 1].Item2, ranges[j].Item2));
                ranges[j - 1] = newRange;
                ranges.RemoveAt(j);
                j--;
            }
        }

        var result = ranges.Select(x => 1 + x.Item2 - x.Item1).Sum();


        return result;
    }
}
