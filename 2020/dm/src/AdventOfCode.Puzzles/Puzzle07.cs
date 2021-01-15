using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle07
    {
        public static Regex bagRegex = new Regex("^([0-9]*) (\\w+ \\w+)$");
        public static Regex inputRegex = new Regex("^(\\w+ \\w+) bag[s]{0,1} contain (?:(?:([0-9]{1} \\w+ \\w+) bag[s]{0,1})[,]{0,1}[ ]{0,1})*.");

        public static int CountTask1(Dictionary<string, IEnumerable<(int count, string bag)>> input, string bagToFind = "shiny gold")
        => BagsWithGivenBag(input, bagToFind).Count();

        public static int CountTask2(Dictionary<string, IEnumerable<(int count, string bag)>> input, string bagToFind = "shiny gold")
        => BagsWithinBag(input, bagToFind);

        public static Dictionary<string, IEnumerable<(int count, string bag)>> ToPuzzle7Input(this string input)
        => input
            .Split("\r\n")
            .Select(x => inputRegex.Match(x))
            .Select(x => new
            {
                container = x.Groups[1].Value,
                items = x.Groups[2].Captures.Any() ? x.Groups[2].Captures.Select(x => bagRegex.Match(x.Value)) : null
            })
            .Select(x => new
            {
                x.container,
                items = x.items?.Select(y => new
                {
                    count = Convert.ToInt32(y.Groups[1].Value),
                    bag = y.Groups[2].Value
                })
                .Select(x => (x.count, x.bag)) ?? new List<(int, string)>()
            })
            .ToDictionary(x => x.container, x => x.items);

        private static IEnumerable<string> BagsWithGivenBag(Dictionary<string, IEnumerable<(int count, string bag)>> bagMap, string bagToFind)
        {
            var bagsWithGivenBag = bagMap
                .Where(x => x.Value.Select(y => y.bag).Contains(bagToFind))
                .Select(x => x.Key)
                .ToList();

            var match = true;
            while (match)
            {
                var matchBags = bagMap
                    .Where(x => !bagsWithGivenBag.Contains(x.Key))
                    .Where(x => x.Value.Select(y => y.bag).Where(y => bagsWithGivenBag.Contains(y)).Any())
                    .Select(x => x.Key)
                    .ToList();

                match = matchBags.Any();
                bagsWithGivenBag.AddRange(matchBags);
            }

            return bagsWithGivenBag;
        }

        private static int BagsWithinBag(Dictionary<string, IEnumerable<(int count, string bag)>> bagMap, string bagToFind)
        {
            IEnumerable<(int parentCount, IEnumerable<(int count, string bag)> bags)> bagsToSearch = bagMap[bagToFind].Select(y => (y.count, bagMap[y.bag]));
            int matchesFound = bagsToSearch.Select(x => x.parentCount).Sum();

            while (bagsToSearch.Any())
            {
                bagsToSearch = bagsToSearch
                    .SelectMany(x => x.bags.Select(y => (y.count * x.parentCount, bagMap[y.bag])));

                matchesFound += bagsToSearch.Select(x => x.parentCount).Sum();
            }

            return matchesFound;
        }
    }
}