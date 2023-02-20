using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day5 : Puzzle<string>
{
    public override int Day => 5;

    public override string First()
    {
        var lines = GetInputLines();
        var stacks = new List<Stack<char>>();
        var index = 0;

        var stackLines = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            string? line = lines[i];
            if (!string.IsNullOrEmpty(line))
            {
                stackLines.Add(line);
            }
            else
            {
                index = i;
                break;
            }
        }

        for (int j = 1; j < stackLines.Max(a => a.Length); j += 4)
        {
            stacks.Add(new Stack<char>());

            for (int i = stackLines.Count - 2; i >= 0; i--)
            {
                if (stackLines[i][j] != ' ')
                    stacks.Last().Push(stackLines[i][j]);
            }
        }

        var instructions = new List<(int amount, int from, int to)>();
        for (int i = index + 1; i < lines.Length; i++)
        {
            string? line = lines[i];
            var instr = line.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
            instructions.Add((int.Parse(instr[0]), int.Parse(instr[1]), int.Parse(instr[2])));
        }

        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.amount; i++)
            {
                stacks[instruction.to - 1].Push(stacks[instruction.from - 1].Pop());
            }
        }

        return new string(stacks.Select(a => a.Peek()).ToArray());

    }

    public override string Second()
    {
        var lines = GetInputLines();
        var stacks = new List<List<char>>();
        var index = 0;

        var stackLines = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            string? line = lines[i];
            if (!string.IsNullOrEmpty(line))
            {
                stackLines.Add(line);
            }
            else
            {
                index = i;
                break;
            }
        }

        for (int j = 1; j < stackLines.Max(a => a.Length); j += 4)
        {
            stacks.Add(new List<char>());

            for (int i = stackLines.Count - 2; i >= 0; i--)
            {
                if (stackLines[i][j] != ' ')
                    stacks.Last().Add(stackLines[i][j]);
            }
        }

        var instructions = new List<(int amount, int from, int to)>();
        for (int i = index + 1; i < lines.Length; i++)
        {
            string? line = lines[i];
            var instr = line.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
            instructions.Add((int.Parse(instr[0]), int.Parse(instr[1]), int.Parse(instr[2])));
        }

        foreach (var instruction in instructions)
        {
            stacks[instruction.to - 1].AddRange(stacks[instruction.from - 1].GetRange(stacks[instruction.from - 1].Count - instruction.amount, instruction.amount));
            stacks[instruction.from - 1].RemoveRange(stacks[instruction.from - 1].Count - instruction.amount, instruction.amount);
        }

        return new string(stacks.Select(a => a.Last()).ToArray());
    }
}
