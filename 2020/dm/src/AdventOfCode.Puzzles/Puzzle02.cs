using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle02
    {
        private static Regex TaskInputParseRegex = new Regex("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");

        public static int CountValidPolicy1Passwords(IEnumerable<(string password, char charToCheck, int minOccurences, int maxOccurences)> inputs)
        => inputs
            .Where(x => x.password.IsPasswordPolicy1Valid(x.charToCheck, x.minOccurences, x.maxOccurences))
            .Count();

        public static int CountValidPolicy2Passwords(IEnumerable<(string password, char charToCheck, int position1, int position2)> inputs)
        => inputs
            .Where(x => x.password.IsPasswordPolicy2Valid(x.charToCheck, x.position1, x.position2))
            .Count();

        public static bool IsPasswordPolicy1Valid(this string password, char charToCheck, int minOccurences, int maxOccurences)
        => password
              .ToCharArray()
              .Where(x => x == charToCheck)
              .Count()
              .IsInRange(minOccurences, maxOccurences);

        public static bool IsPasswordPolicy2Valid(this string password, char charToCheck, int position1, int position2)
        => (password.Length >= position2)
            && (password[position1 - 1] == charToCheck ^ password[position2 - 1] == charToCheck);

        public static IEnumerable<(string password, char charToCheck, int minOccurences, int maxOccurences)> ToPuzzle2Input(this string input)
        => input
            .Trim()
            .Split("\r\n")
            .Select(x => TaskInputParseRegex.Match(x))
            .Select(x => (x.Groups[4].Value, char.Parse(x.Groups[3].Value), Convert.ToInt32(x.Groups[1].Value), Convert.ToInt32(x.Groups[2].Value)));

        private static bool IsInRange(this int number, int lowerInclusiveLimit, int upperInclusiveLimit)
        => number >= lowerInclusiveLimit && number <= upperInclusiveLimit;
    }
}