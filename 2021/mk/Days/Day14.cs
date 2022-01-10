using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day14 : DayBase<long>
    {
        public override int Day => 14;

        public override long First()
        {
            var input = GetInputText();
            var template = input.Split("\r\n\r\n")[0];

            var rules = input.Split("\r\n\r\n")[1].Split("\r\n").Select(a => a.Split(" -> ")).ToDictionary(a => a[0], b => b[1]);

            for (int d = 0; d < 10; d++)
            {
                var newTemplate = "";
                for (int i = 0; i < template.Length - 1; i++)
                {
                    var key = $"{template[i]}{template[i + 1]}";
                    newTemplate += template[i];

                    if (rules.ContainsKey(key))
                    {
                        newTemplate += rules[key];
                    }
                }
                newTemplate += template[template.Length - 1];

                template = newTemplate;
            }

            var occurences = new Dictionary<char, int>();
            foreach (var polymer in template)
            {
                if (!occurences.ContainsKey(polymer))
                    occurences[polymer] = 0;

                occurences[polymer]++;
            }

            return occurences.Max(a => a.Value) - occurences.Min(a => a.Value);
        }

        public override long Second()
        {
            string input = GetInputText();
            var template = input.Split("\r\n\r\n")[0];

            var rules = input.Split("\r\n\r\n")[1].Split("\r\n").Select(a => a.Split(" -> ")).ToDictionary(a => a[0], b => b[1]);
            var rulesFw = rules.ToDictionary(a => a.Key, v => new[] { v.Key[0] + v.Value, v.Value + v.Key[1] });

            var cnt = rules.ToDictionary(a => a.Key, b => (long) 0);

            for (int i = 0; i < template.Length - 1; i++)
            {
                cnt[template.Substring(i, 2)]++;
            }

            var cnts = new List<Dictionary<string, long>>();
            cnts.Add(cnt);


            for (int d = 1; d <= 40; d++)
            {
                var curCnt = cnts.Last();
                var nextCnt = rules.ToDictionary(a => a.Key, b => (long)0);

                foreach (var item in curCnt)
                {
                    foreach (var rulefw in rulesFw[item.Key])
                    {
                        nextCnt[rulefw] += curCnt[item.Key];
                    }
                }

                cnts.Add(nextCnt);
            }

            var totalCnt = cnts.Last();

            var totals = new Dictionary<char, long>();
            foreach (var item in totalCnt)
            {
                if (!totals.ContainsKey(item.Key[0]))
                    totals[item.Key[0]] = 0;

                totals[item.Key[0]] += item.Value;
            }

            totals[template.Last()]++;

            var totalsOrdered = totals.OrderByDescending(a => a.Value);

            return totalsOrdered.First().Value - totalsOrdered.Last().Value;
        }
    }
}
