using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle08
    {
        public static int CountTask1(IEnumerable<(string instruction, int count)> input)
        {
            var executed = Enumerable.Repeat(0, input.Count()).ToList();
            var index = 0;
            var accumulator = 0;

            while (true)
            {
                var (instruction, count) = input.ElementAt(index);
                if (executed[index] > 0)
                {
                    break;
                }
                else
                {
                    executed[index]++;
                }

                index += GetOperationIndexIncrement(instruction, count);
                accumulator += instruction == "acc" ? count : 0;

                index %= input.Count();

                if (index < 0)
                    index = input.Count() + index;
            }

            return accumulator;
        }

        public static int CountTask2(IEnumerable<(string instruction, int count)> input)
        {
            var executedInstructions = input.GetExecutingIndexes();
            var possibleSwitches = input
                .Select((x, i) => new
                {
                    x.instruction,
                    x.count,
                    index = i
                })
                .Where(x => x.instruction != "acc")
                .Where(x => executedInstructions.Contains(x.index));

            foreach (var element in possibleSwitches)
            {
                var copy = input
                    .Take(element.index)
                    .Append((element.instruction == "jmp" ? "nop" : "jmp", element.count))
                    .Concat(input.Skip(element.index + 1));

                if (!copy.IsLocalInfinite())
                    return CountTask1(copy);
            }

            throw new ArgumentException();
        }

        public static IEnumerable<(string instruction, int count)> ToPuzzle8Input(this string input)
        => input
            .Split("\r\n")
            .Select(x => x.Split(" "))
            .Select(x => (x[0], Convert.ToInt32(x[1])));

        private static IEnumerable<int> GetExecutingIndexes(this IEnumerable<(string instruction, int count)> input)
        {
            var executed = Enumerable.Repeat(0, input.Count()).ToList();
            var index = 0;

            while (true)
            {
                var (instruction, count) = input.ElementAt(index);
                if (executed[index] > 0)
                {
                    break;
                }
                else
                {
                    executed[index]++;
                }

                index += GetOperationIndexIncrement(instruction, count);
                index %= input.Count();

                if (index < 0)
                    index = input.Count() + index;
            }

            return executed
                .Select((x, i) => new { index = i, count = x })
                .Where(x => x.count > 0)
                .Select(x => x.index);
        }

        private static int GetOperationIndexIncrement(string instruction, int count)
        {
            var increment = 0;
            switch (instruction)
            {
                case "nop":
                    increment++;
                    break;

                case "acc":
                    increment++;
                    break;

                case "jmp":
                    increment += count;
                    break;
            }

            return increment;
        }

        private static bool IsLocalInfinite(this IEnumerable<(string instruction, int count)> input)
        {
            var executed = Enumerable.Repeat(0, input.Count()).ToList();
            var index = 0;

            bool? isLocalInfinite = null;

            while (!isLocalInfinite.HasValue)
            {
                var (instruction, count) = input.ElementAt(index);
                if (executed[index] > 0)
                {
                    isLocalInfinite = true;
                    break;
                }
                else
                {
                    executed[index]++;
                }

                index += GetOperationIndexIncrement(instruction, count);

                if (index < 0 || index >= input.Count())
                    isLocalInfinite = false;
            }

            return isLocalInfinite.Value;
        }
    }
}