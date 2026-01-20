namespace AdventOfCode2025.Days;

public class Day03 : Puzzle<long>
{
    public override long First(string input)
    {
        var lines = input.ToLines().Select(x => x.Select(y => int.Parse(y.ToString())).ToList()).ToList();
        long sum = 0;

        foreach (var line in lines)
        {
            var first = line[..^1].Max();
            var second = line[(line.IndexOf(first) + 1)..].Max();

            sum += first * 10 + second;
        }

        return sum;
    }

    public override long Second(string input)
    {
        var lines = input.ToLines().Select(x => x.Select(y => int.Parse(y.ToString())).ToList()).ToList();
        long sum = 0;

        foreach (var line in lines)
        {
            var leftIndex = 0;
            for (int i = 12; i >= 1; i--)
            {
                var digit = line[leftIndex..(line.Count - (i - 1))].Max();
                leftIndex = line.IndexOf(digit, leftIndex) + 1;

                sum += digit * (long)Math.Pow(10, i -1);
            }
        }

        return sum;
    }
}
