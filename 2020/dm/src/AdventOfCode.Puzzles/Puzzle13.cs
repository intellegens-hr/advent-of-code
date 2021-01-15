using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    // https://rosettacode.org/wiki/Chinese_remainder_theorem
    public static class ChineseRemainderTheorem
    {
        public static long Solve(long[] primes, long[] remainders)
        {
            long product = primes.Aggregate((i, j) => i * j);
            long p;
            long sm = 0;
            for (int i = 0; i < primes.Length; i++)
            {
                p = product / primes[i];
                sm += remainders[i] * ModularMultiplicativeInverse(p, primes[i]) * p;
            }
            return sm % product;
        }

        private static int ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }

    public static class Puzzle13
    {
        public static int Task1((int timeStart, List<(int index, int number)> buses) input)
        {
            var buses = input.buses.Select(x => x.number).ToList();

            var i = input.timeStart;
            while (true)
            {
                var candidates = buses.Where(x => (i % x) == 0);
                if (candidates.Any())
                    return candidates.First() * (i - input.timeStart);

                i++;
            }
        }

        public static long Task2(List<(int index, int number)> buses)
        {
            var (maxBusIndex, maxBusNumber) = buses
                .Where(x => buses.Select(x => x.number).Max() == x.number)
                .First();

            var busesToCheck = buses
                .Where(x => x.number != maxBusNumber)
                .Select(x => new
                {
                    x.index,
                    x.number
                })
                .OrderByDescending(x => x.number)
                .ToArray();

            return ChineseRemainderTheorem.Solve(buses.Select(x => (long)x.number).ToArray(), buses.Select(x => (long)(x.number- x.index % x.number) ).ToArray());

            long i = 1;
            while (true)
            {
                var timestamp = i * maxBusNumber - maxBusIndex;
                if (busesToCheck.All(x => (timestamp % x.number) == (x.number - x.index)))
                {
                    return timestamp;
                }

                i++;
            }
        }

        public static (int timeStart, List<(int index, int number)> buses) ToPuzzle13Input(this string input)
        {
            var split = input.Split("\r\n");
            var timeStart = Convert.ToInt32(split[0]);

            var buses = split[1]
                .Split(',')
                .Select((x, i) => (x, i))
                .Where(x => x.x != "x")
                .Select(x => (x.i, Convert.ToInt32(x.x)))
                .ToList();

            return (timeStart, buses);
        }
    }
}