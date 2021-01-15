using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle15
    {
        public static int Task1(List<int> input, int length = 2020)
        {
            Dictionary<int, (int x1, int? x2)> cache = new Dictionary<int, (int x1, int? x2)>();

            for (var i = 0; i < input.Count; i++)
                cache[input[i]] = (i, null);

            var lastElement = input.Last();
            var lastElementData = cache[lastElement];

            for (var i = input.Count; i < length; i++)
            {
                lastElement = (i - 1) - (lastElementData.x2 ?? (i - 1));

                cache[lastElement] = lastElementData = cache.ContainsKey(lastElement)
                    ? (i, cache[lastElement].x1)
                    : (i, null);
            }

            return (int)lastElement;
        }

        public static List<int> ToPuzzle15Input(this string input)
        {
            return input.Split(',').Select(x => Convert.ToInt32(x)).ToList();
        }
    }
}