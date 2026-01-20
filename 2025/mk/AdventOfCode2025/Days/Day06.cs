namespace AdventOfCode2025.Days;

public class Day06 : Puzzle<long>
{
    public override long First(string input)
    {
        var lines = input.ToLines();
        var numbers = lines[..^1].Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)).To2DArray();
        var operators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        long total = 0;
        for (int i = 0; i < numbers.GetLength(1); i++)
        {
            long colSum = operators[i] == "*" ? 1 : 0;
            for (int j = 0; j < numbers.GetLength(0); j++)
            {
                if (operators[i] == "*")
                    colSum *= numbers[j,i];
                else
                    colSum += numbers[j,i];
            }
            total += colSum;
        }

        return total;
    }

    public override long Second(string input)
    {
        var lines = input.ToLines();
        var rotated = lines[..^1].Transpose().Select(x => new string([.. x]).Trim()).ToList();
        var operators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

        rotated.Add(""); // TODO: clean this, it only makes sure the for loop runs one last final time

        int i = 0;
        long total = 0;
        var temp = new List<long>();
        for (int j = 0; j < rotated.Count; j++)
        {
            if (string.IsNullOrEmpty(rotated[j]))
            {
                var op = operators[i++];
                total += temp.Aggregate((a, b) => op == "*" ? a * b : a + b);
                temp.Clear();
                continue;
            }
            temp.Add(long.Parse(rotated[j]));
        }
        return total;
    }
}