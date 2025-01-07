namespace AdventOfCode2024.Days;

public class Day05 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines();
        var (rules, pageNumbers) = ParseRulesAndNumbers(lines);

        int count = 0;
        foreach (var item in pageNumbers.Where(item => IsRightOrder(item, rules)))
        {
            count += item[item.Length / 2];
        }

        return count;
    }

    private static (HashSet<(int, int)> rules, List<int[]> pageNumbers) ParseRulesAndNumbers(string[] lines)
    {
        var rules = new HashSet<(int, int)>();
        var pageNumbers = new List<int[]>();

        var i = 0;
        while (lines[i] != "")
        {
            var split = lines[i++].Split("|").Select(x => int.Parse(x)).ToArray();
            rules.Add((split[0], split[1]));
        }
        i++;

        while (i < lines.Length)
        {
            pageNumbers.Add(lines[i++].Split(",").Select(x => int.Parse(x)).ToArray());
        }

        return (rules, pageNumbers);
    }

    private bool IsRightOrder(int[] line, HashSet<(int, int)> rules)
    {

        for (int i = 0; i < line.Length - 1; i++)
        {
            for (int j = i + 1; j < line.Length; j++)
            {
                if (!rules.Contains((line[i], line[j])))
                    return false;
            }
        }

        return true;
    }

    public override int Second(string input)
    {
        var lines = input.ToLines();
        var (rules, pageNumbers) = ParseRulesAndNumbers(lines);

        int count = 0;
        foreach (var item in pageNumbers.Where(item => !IsRightOrder(item, rules)))
        {
            var newOrder = Order(item, rules);
            count += newOrder[newOrder.Length / 2];
        }

        return count;
    }

    private int[] Order(int[] item, HashSet<(int, int)> rules)
    {
        var items = item.ToList();
        items.Sort((a, b) => rules.Contains((a, b)) ? 1 : -1);
        return items.ToArray();
    }
}
