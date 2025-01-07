using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days;

public class Day03 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines();

        var sum = 0;
        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, "mul\\(\\d+,\\d+\\)");
            foreach (var match in matches)
            {
                sum += Parse(match.ToString()!);
            }

        }


        return sum;
    }

    private int Parse(string v)
    {
        var parts = v.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries);

        return int.Parse(parts[0]) * int.Parse(parts[1]);
    }

    public override int Second(string input)
    {
        var lines = input.ToLines();
        int sum = 0;
        bool @do = true;

        foreach (var line in lines)
        {
            var matches = Regex.Matches(line, "(do\\(\\)|don't\\(\\)|mul\\(\\d+,\\d+\\))");
            foreach(var match in matches)
            {
                if (match.ToString() == "do()")
                    @do = true;
                else if (match.ToString() == "don't()")
                    @do = false;
                else if (@do)
                    sum += Parse(match.ToString()!);
            }
        }

        return sum;
    }

}
