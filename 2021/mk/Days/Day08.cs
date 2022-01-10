using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day08 : DayBase<long>
    {
        public override int Day => 8;

        // 473


        public override long First()
        {
            return GetInputLines().SelectMany(a => a.Split(" | ")[1].Split(' ')).Count(a => a.Length is 2 or 3 or 4 or 7);
        }

        public override long Second()
        {
            var lines = GetInputLines();
            long total = 0;

            foreach (var line in lines)
            {
                var segments = line.Split(" | ")[0].Split(" ").Select(a => string.Join("", a.OrderBy(x => x))).GroupBy(a => a.Length).ToDictionary(a => a.Key, b => b.ToList());
                var digits = line.Split(" | ")[1].Split(" ").Select(a => string.Join("", a.OrderBy(x => x)));

                var decoded = new Dictionary<int, string>();
                decoded.Add(1, segments[2][0]);
                decoded.Add(7, segments[3][0]);
                decoded.Add(4, segments[4][0]);
                decoded.Add(8, segments[7][0]);
                decoded.Add(3, segments[5].FirstOrDefault(a => decoded[1].All(x => a.Contains(x))));
                decoded.Add(5, segments[5].FirstOrDefault(a => decoded[4].Except(decoded[1]).All(x => a.Contains(x))));
                decoded.Add(2, segments[5].FirstOrDefault(a => a != decoded[3] && a != decoded[5]));
                decoded.Add(6, segments[6].FirstOrDefault(a => !decoded[1].All(x => a.Contains(x))));
                decoded.Add(9, segments[6].FirstOrDefault(a => decoded[4].All(x => a.Contains(x))));
                decoded.Add(0, segments[6].FirstOrDefault(a => a != decoded[6] && a != decoded[9]));

                var decodedInv = decoded.ToDictionary(a => a.Value, b => b.Key.ToString());

                total += Int32.Parse(string.Join("", digits.Select(d => decodedInv[d])));

                // UNSORT

            }

            return total;
        }
    }
}
