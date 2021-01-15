using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle16
    {
        private static bool MatchesAny(this int value, List<((int lower, int upper) r1, (int lower, int upper) r2)> ranges)
        => ranges.Any(x => value.Between(x.r1) || value.Between(x.r2));

        private static bool Matches(this int value, (int lower, int upper) r1, (int lower, int upper) r2)
        => value.Between(r1) || value.Between(r2);


        public static int Task1(Puzzle16Data input)
        {
            return input
                .NearbyTickets
                .Select(x => x.Where(x => !x.MatchesAny(input.Params)).Sum())
                .Sum();
        }

        public static long Task2(Puzzle16Data input)
        {
            var validTickets = input
                .NearbyTickets
                .Where(x => x.All(x => x.MatchesAny(input.Params)))
                .Select(x => x.ToArray())
                .Append(input.MyTicket)
                .ToArray();

            var ticketIndexToParamIndexMapping = new Dictionary<int, int>();

            var paramsNotMapped = input
                .Params
                .Select((x, i) => (x, i))
                .ToList();

            var ticketIndexesNotMapped = input
                .MyTicket
                .Select((x, i) => i)
                .ToList();

            // parameter indexes required in task are 0-5 (all departure parameters)
            while (paramsNotMapped[0].i < 6)
            {
                foreach (var param in paramsNotMapped)
                {
                    int matchedIndex = 0;
                    int matchCount = 0;
                    var (range1, range2) = param.x;

                    foreach (var ticketIndex in ticketIndexesNotMapped)
                    {
                        var matched = validTickets
                            .All(x => x[ticketIndex].Matches(range1, range2));

                        if (matched)
                        {
                            matchCount++;

                            if (matchCount > 1)
                            {
                                break;
                            }

                            matchedIndex = ticketIndex;
                        }
                    }

                    if (matchCount == 1)
                    {
                        ticketIndexToParamIndexMapping[matchedIndex] = param.i;
                        ticketIndexesNotMapped.Remove(matchedIndex);
                        paramsNotMapped.Remove(param);
                        break;
                    }
                }
            }

            return ticketIndexToParamIndexMapping
                .Where(x => x.Value < 6)
                .Select(x => (long)input.MyTicket[x.Key])
                .Aggregate((a, b) => a * b);
        }

        public static Puzzle16Data ToPuzzle16Input(this string input)
        {
            var data = new Puzzle16Data();

            var sections = input.Split("\r\n\r\n").ToList();

            data.Params = sections[0]
                .Split("\r\n")
                .Select(x => x[(x.IndexOf(": ") + 2)..])
                .Select(x => x.Split(" or "))
                .Select(x => x.Select(y => y.Split("-").ToList()).ToList())
                .Select(x => ((x[0][0].ToInt32(), x[0][1].ToInt32()), (x[1][0].ToInt32(), x[1][1].ToInt32())))
                .ToList();

            data.MyTicket = sections[1].Split("\r\n").Last().Split(',').Select(x => x.ToInt32()).ToArray();

            data.NearbyTickets = sections[2].Split("\r\n").Skip(1).Select(x => x.Split(',').Select(x => x.ToInt32()));

            return data;
        }

        public class Puzzle16Data
        {
            public int[] MyTicket { get; set; }
            public List<((int from, int to) range1, (int from, int to) range2)> Params { get; set; }
            public IEnumerable<IEnumerable<int>> NearbyTickets { get; set; }

        }
    }
}