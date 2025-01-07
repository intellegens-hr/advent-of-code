namespace AdventOfCode2024.Days;

public class Day01 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines().Select(x => x.Split("   ").Select(x => int.Parse(x))).To2DArray();
        var col1 = lines.GetColumn(0).OrderBy(x => x);
        var col2 = lines.GetColumn(1).OrderBy(x => x);

        var diff = col1.Zip(col2).Select(x => Math.Abs(x.First - x.Second));

        return diff.Sum();
    }

    public override int Second(string input)
    {
        var lines = input.ToLines().Select(x => x.Split("   ").Select(x => int.Parse(x))).To2DArray();
        var col1 = lines.GetColumn(0);
        var col2 = lines.GetColumn(1).OrderBy(x => x);

        var occurrences = new Dictionary<int, int>();
        foreach (var number in col2)
        {
            if (occurrences.ContainsKey(number))
                occurrences[number]++;
            else
                occurrences[number] = 1;
        }

        var score = 0;
        foreach (var number in col1.Where(number => occurrences.ContainsKey(number)))
        {
            score += number * occurrences[number];
        }

        return score;
    }

    // alternative idea: regex!
    // (one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9).*(one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9)
}
