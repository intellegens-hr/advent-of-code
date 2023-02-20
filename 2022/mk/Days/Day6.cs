using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day6 : Puzzle<int>
{
    public override int Day => 6;

    public override int First()
    {
        var sequence = GetInputText();
        var num = 4;
        var hashSet = new List<char>(num);

        for (int i = 0; i < sequence.Length; i++)
        {
            hashSet.Add(sequence[i]);

            if (hashSet.Count > num)
                hashSet.RemoveAt(0);

            var blje = new HashSet<char>(hashSet);
            if (blje.Count == num)
                return i + 1;
        }

        return 0;

    }

    public override int Second()
    {
        var sequence = GetInputText();
        var num = 14;
        var hashSet = new List<char>(num);

        for (int i = 0; i < sequence.Length; i++)
        {
            hashSet.Add(sequence[i]);

            if (hashSet.Count > num)
                hashSet.RemoveAt(0);

            var blje = new HashSet<char>(hashSet);
            if (blje.Count == num)
                return i + 1;
        }

        return 0;
    }

    public int SecondD()
    {
        var sequence = GetInputText();
        var num = 14;
        var hashSet = new HashSet<char>(num);

        for (int i = 0; i < sequence.Length; i++)
        {
            hashSet.Add(sequence[i]);

            if (hashSet.Count == num)
                return i + 1;
        }

        return 0;
    }
}
