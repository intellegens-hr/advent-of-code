using Microsoft.Win32;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day11 : Puzzle<long>
{
    public override int Day => 11;

    public override long First()
    {
        var lines = GetInputLines();
        var monkeys = ParseMonkeys(lines).ToList();

        for (long i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.Inspected++;

                    var item = monkey.Items.First();
                    monkey.Items.RemoveAt(0);

                    var newLevel = monkey.GetNewLevel(item.value);
                    newLevel /= 3;

                    if (newLevel % monkey.Divisible == 0)
                    {
                        monkeys[monkey.True].Items.Add((item.i, newLevel));
                    }
                    else
                    {
                        monkeys[monkey.False].Items.Add((item.i, newLevel));
                    }
                }
            }

        }

        var orderedMonkeys = monkeys.OrderByDescending(a => a.Inspected).ToList();

        return orderedMonkeys[0].Inspected * orderedMonkeys[1].Inspected;
    }

    private IEnumerable<Monkey> ParseMonkeys(string[] lines)
    {
        var i = 1;
        while (i < lines.Length)
        {
            var startingItems = lines[i].Split(new[] { "  Starting items: ", ", " }, StringSplitOptions.RemoveEmptyEntries).Select(a => long.Parse(a));
            var operation = lines[i + 1].Split("  Operation: new = ", StringSplitOptions.RemoveEmptyEntries)[0];
            var divisible = long.Parse(lines[i + 2].Split("  Test: divisible by ", StringSplitOptions.RemoveEmptyEntries)[0]);
            var trueThrow = int.Parse(lines[i + 3].Split("    If true: throw to monkey ", StringSplitOptions.RemoveEmptyEntries)[0]);
            var falseThrow = int.Parse(lines[i + 4].Split("    If false: throw to monkey ", StringSplitOptions.RemoveEmptyEntries)[0]);

            yield return new Monkey
            {
                Items = startingItems.Select(a => ((long)0, (long)a)).ToList(),
                Divisible = divisible,
                Operation = operation,
                True = trueThrow,
                False = falseThrow
            };

            i += 7;
        }
    }

    public override long Second()
    {
        var lines = GetInputLines();
        var monkeys = ParseMonkeys(lines).ToList();

        var j = 0;
        for (int i1 = 0; i1 < monkeys.Count; i1++)
        {
            var monkey = monkeys[i1];
            for (int i2 = 0; i2 < monkey.Items.Count; i2++)
            {
                monkey.Items[i2] = (j++, monkey.Items[i2].value);
            }
        }

        var denominator = monkeys.Select(a => a.Divisible).Distinct().Aggregate((a, i) => a * i);

        //Console.WriteLine($"After round 0, the monkeys are holding items with these worry levels:");
        //for (int i1 = 0; i1 < monkeys.Count; i1++)
        //{
        //    var mon = monkeys[i1];
        //    Console.WriteLine($"Monkey {i1}: {string.Join(", ", mon.Items.OrderBy(a => a.i))}");
        //}
        //Console.WriteLine();

        for (long i = 1; i <= 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.Inspected++;

                    var item = monkey.Items.First();
                    monkey.Items.RemoveAt(0);

                    var newLevel = monkey.GetNewLevel(item.value);

                    if (newLevel % monkey.Divisible == 0)
                    {
                        monkeys[monkey.True].Items.Add((item.i, newLevel % denominator));
                    }
                    else
                    {
                        monkeys[monkey.False].Items.Add((item.i, newLevel % denominator));
                    }
                }
            }

            //Console.WriteLine($"After round {i}, the monkeys are holding items with these worry levels:");
            //for (int i1 = 0; i1 < monkeys.Count; i1++)
            //{
            //    var mon = monkeys[i1];
            //    Console.WriteLine($"Monkey {i1}: {string.Join(", ", mon.Items.OrderBy(a => a.i))}");
            //}
            //Console.WriteLine();

            //if ((i) % 1000 == 0 || i == 20 || i == 1)
            //{
            //    Console.WriteLine($"== After round {i} ==");
            //    for (int i1 = 0; i1 < monkeys.Count; i1++)
            //    {
            //        var mon = monkeys[i1];
            //        Console.WriteLine($"Monkey {i1} inspected items {mon.Inspected} times.");
            //    }
            //    Console.WriteLine();
            //}

        }

        var orderedMonkeys = monkeys.OrderByDescending(a => a.Inspected).ToList();

        return orderedMonkeys[0].Inspected * orderedMonkeys[1].Inspected;
    }

    public class Monkey
    {
        public List<(long i, long value)> Items { get; set; } = new List<(long, long)>();

        public string Operation { get; set; } = default!;

        public long Divisible { get; set; }

        public int True { get; set; }

        public int False { get; set; }

        public long Inspected { get; set; } = 0;
        public long GetNewLevel(long item)
        {
            var operation = Operation.Split(" ");
            var left = operation[0] == "old" ? item : long.Parse(operation[0]);
            var right = operation[2] == "old" ? item : long.Parse(operation[2]);

            return operation[1] switch
            {
                "*" => left * right,
                "+" => left + right,
                "-" => left - right,
                "/" => left / right
            };
        }
    }

}
