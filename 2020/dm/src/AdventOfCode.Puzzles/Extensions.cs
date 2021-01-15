using System;

namespace AdventOfCode.Puzzles
{
    internal static class Extensions
    {
        internal static bool Between(this int i, int lower, int upper)
        => i >= lower && i <= upper;

        internal static bool Between(this int i, (int lower, int upper) range)
        => i.Between(range.lower, range.upper);

        internal static int ToInt32(this string input)
        => Convert.ToInt32(input);
    }
}