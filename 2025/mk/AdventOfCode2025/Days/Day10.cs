
namespace AdventOfCode2025.Days;

public record Machine(string Diagram, List<int[]> Schematics, int[] JoltageRequirements);

public class Day10 : Puzzle<long>
{
    public override long First(string input)
    {
        var lines = input.ToLines();
        var machines = new List<Machine>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var diagram = parts[0][1..^1];
            var schematics = parts[1..^1].Select(x => x[1..^1].Split(',').Select(y => int.Parse(y)).ToArray()).ToList();
            var reqs = parts[^1][1..^1].Split(',').Select(x => int.Parse(x)).ToArray();
            machines.Add(new Machine(diagram, schematics, reqs));
        }

        var sum = 0;
        foreach (var machine in machines)
        {
            minSteps = int.MaxValue;
            sum += ProcessLights(machine.Diagram, machine.Schematics, 0);
        }
        
        return sum;
    }

    private int minSteps = int.MaxValue;

    private int ProcessLights(string diagram, List<int[]> schematics, int steps)
    {
        if (steps == minSteps)
            return int.MaxValue;

        //Console.WriteLine($"{new String(' ', steps)} diagram: {diagram}, steps: {steps}, schematics: {schematics.Count}");

        if (diagram.All(x => x == '.'))
            return steps;

        var firstOneIndex = diagram.IndexOf("#");
        var options = schematics.Where(x => x.Contains(firstOneIndex));

        steps++;

        var results = new List<int>();
        foreach (var option in options)
        {
            var newDiagram = SwitchLights(diagram, option);

            //Console.WriteLine($"{new String(' ', steps)} newDiagram: {newDiagram}");

            var newSchematics = schematics.Except([option]).ToList();

            var result = ProcessLights(newDiagram, newSchematics, steps);
            
            results.Add(result);

            if (result < minSteps)
                minSteps = result;
        }

        return results.Any() ? results.Min() : int.MaxValue;
    }

    private string SwitchLights(string diagram, int[] option)
    {
        var diag = diagram.ToArray();
        foreach (var index in option)
        {
            diag[index] = diag[index] == '.' ? '#' : '.';
        }
        return new string(diag);
    }

    public override long Second(string input)
    {
        return 0;
        var lines = input.ToLines();
        var machines = new List<Machine>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var diagram = parts[0][1..^1];
            var schematics = parts[1..^1].Select(x => x[1..^1].Split(',').Select(y => int.Parse(y)).ToArray()).ToList();
            var reqs = parts[^1][1..^1].Split(',').Select(x => int.Parse(x)).ToArray();
            machines.Add(new Machine(diagram, schematics, reqs));
        }

        long sum = 0;
        foreach (var machine in machines)
        {
            minFactor = long.MaxValue;
            var blje =  ProcessLights2(machine.Schematics, machine.JoltageRequirements, 0, 0);

            if (blje == long.MaxValue)
                continue;

            Console.WriteLine($"{machines.IndexOf(machine)} / {machines.Count} done, {DateTime.Now.ToLongTimeString()}");
            sum += blje;
        }

        return sum;
    }

    private long minFactor = long.MaxValue;
    private Dictionary<string, long> memo = new();
    private long ProcessLights2(List<int[]> schematics, int[] joltageRequirements, int totalFactor, int level)
    {
        var key = string.Join('+', schematics.OrderBy(x => x[0]).Select(x => string.Join(',', x))) + "|" + string.Join(',', joltageRequirements);
        if (memo.ContainsKey(key))
            return memo[key];
        if (totalFactor > minFactor)
            return long.MaxValue;

        //Console.WriteLine($"diagram: {diagram}, steps: {steps}, schematics: {schematics.Count}");

        if (joltageRequirements.All(x => x == 0))
            return totalFactor;

        var firstNonZeroIndex = joltageRequirements.IndexOf(joltageRequirements.Max());
        var options = schematics.Where(x => x.Contains(firstNonZeroIndex)).Where(x => x.All(y => joltageRequirements[y] != 0));

        var results = new List<long>();
        foreach (var option in options)
        {
            var (_, maxFactor) = ReduceJoltage(joltageRequirements, option, 0);

            for (var i = maxFactor - 1; i >= 0; i--)
            {
                var (newJoltageRequirements, factor) = ReduceJoltage(joltageRequirements, option, i);

                //Console.WriteLine($"{new String(' ', level)} joltage: {string.Join(',', joltageRequirements)} reduced by {string.Join(',', option)} * {factor} = newJoltage: {string.Join(',', newJoltageRequirements)}");

                var newSchematics = schematics.Except([option]).ToList();

                var result = ProcessLights2(newSchematics, newJoltageRequirements, totalFactor + factor, level + 1);
                if (result == long.MaxValue)
                    continue;

                results.Add(result);

                if (result < minFactor)
                    minFactor = result;
            }
        }

        if (results.Where(x => x != long.MaxValue).Any())
        {
            memo[key] = results.Min();
            return memo[key];
        }

        memo[key] = long.MaxValue;
        return long.MaxValue;
    }

    private (int[] newJoltage, int factor) ReduceJoltage(int[] joltageRequirementsInput, int[] option, int until)
    {
        var factor = 0;
        var joltageRequirements = joltageRequirementsInput.ToArray();

        while (option.Select(x => joltageRequirements[x]).All(x => x > until))
        {
            factor++;

            foreach (var index in option)
            {
                joltageRequirements[index]--;
            }
        }

        return (joltageRequirements, factor);
    }
}