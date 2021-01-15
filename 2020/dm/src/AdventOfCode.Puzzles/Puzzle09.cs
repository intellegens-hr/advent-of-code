using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle09
    {
        public static long FirstNonSummableElement(IEnumerable<long> input, int preamble)
        => input
            .Select((element, index) => (element, index))
            .Skip(preamble)
            .Where(x => !input.Skip(x.index - preamble).Take(preamble).SumExistsForElement(x.element))
            .Select(x => x.element)
            .First();

        public static long FindRangeSum(IEnumerable<long> input, int preamble)
        {
            var elementToSumCheck = FirstNonSummableElement(input, preamble);

            var foundRange = Enumerable
                .Range(0, input.Count())
                .Select(x => input.Skip(x).FindSummableSubset(elementToSumCheck))
                .Where(x => x?.Any() == true)
                .First();

            return foundRange.Min() + foundRange.Max();

            throw new ArgumentException();
        }

        public static IEnumerable<long> ToPuzzle9Input(this string input)
        => input
            .Split("\r\n")
            .Select(x => Convert.ToInt64(x))
            .ToList();

        private static IEnumerable<long> FindSummableSubset(this IEnumerable<long> input, long sumToFind)
        {
            for (var i = 0; i < input.Count(); i++)
            {
                var range = input.Take(i);
                var sum = range.Sum();
                if (sum > sumToFind)
                    break;
                else if (sum == sumToFind)
                    return range;
            }

            return null;
        }

        private static bool SumExistsForElement(this IEnumerable<long> input, long element)
        => input
            .Select((element1, index1) => new
            {
                element1,
                index1,
                rangeData = input.Select((element2, index2) => new { element2, index2 }).Where(y => y.index2 != index1)
            })
            .Where(x => x.rangeData.Where(y => (y.element2 + x.element1) == element).Any())
            .Any();
    }
}