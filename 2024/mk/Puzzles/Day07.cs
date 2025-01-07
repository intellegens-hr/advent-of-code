namespace AdventOfCode2024.Days;

public class Day07 : Puzzle<long>
{
    public override long First(string input)
    {
        var lines = input.ToLines();

        var equations = lines.Select(x => x.Split(": ")).Select(x => (long.Parse(x[0]), x[1].Split(" ").Select(y => long.Parse(y)).ToArray()));

        long sum = 0;
        foreach (var equation in equations)
        {
            if (CanMakeFrom(equation.Item1, equation.Item2, GenerateOperatorPermutations(equation.Item2.Length - 1)))
                sum += equation.Item1;
        }
        return sum;
    }

    public enum Operator { Times, Plus, Concat };

    public List<Operator[]> GenerateOperatorPermutations(int n, bool includeConcat = false)
    {
        if (n == 0)
            return [[]];

        var next = GenerateOperatorPermutations(n - 1, includeConcat);

        if (includeConcat)
        {
            return
            [
                .. next.Select(x => x.Append(Operator.Times).ToArray()), 
                .. next.Select(x => x.Append(Operator.Plus).ToArray()).ToList(),
                .. next.Select(x => x.Append(Operator.Concat).ToArray()),
            ];
        }
        else
        {
            return 
            [
                .. next.Select(x => x.Append(Operator.Times).ToArray()), 
                .. next.Select(x => x.Append(Operator.Plus).ToArray()).ToList()
            ];

        }

    }

    private bool CanMakeFrom(long value, long[] numbers, List<Operator[]> permutations)
    {
        foreach (var operators in permutations)
        {
            var tempValue = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                tempValue = DoOperation(tempValue, numbers[i], operators[i - 1]);
                if (tempValue > value)
                    break;
            }
            if (tempValue == value)
                return true;
        }
        return false;
    }

    private long DoOperation(long left, long right, Operator op)
    {
        return op switch
        {
            Operator.Times => left * right,
            Operator.Plus => left + right,
            Operator.Concat => left * (long)Math.Pow(10, right.ToString().Length) + right,
            _ => throw new NotImplementedException(),
        };
    }

    public override long Second(string input)
    {
        var lines = input.ToLines();

        var equations = lines.Select(x => x.Split(": ")).Select(x => (long.Parse(x[0]), x[1].Split(" ").Select(y => long.Parse(y)).ToArray()));
        long sum = 0;

        foreach (var equation in equations)
        {
            if (CanMakeFrom(equation.Item1, equation.Item2, GenerateOperatorPermutations(equation.Item2.Length - 1, true)))
                sum += equation.Item1;
        }

        return sum;
    }
    
}
