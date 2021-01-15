using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day7 : PuzzleDay<int>
    {
        public override int Day => 7;

        public List<BagRule> bagRules = new List<BagRule>();
        public Dictionary<string, int> canHold = new Dictionary<string, int>();
        public Dictionary<string, int> holdsBags = new Dictionary<string, int>();

        public override int First()
        {
            bagRules = GetInputLines().Select(l => new BagRule(l)).ToList();
            
            foreach (var bagRule in bagRules)
            {
                canHold[bagRule.Name] = GetCount(bagRule);
            }

            return canHold.Count(a => a.Value > 0);
        }

        public override int Second()
        {
            bagRules = GetInputLines().Select(l => new BagRule(l)).ToList();

            foreach (var bagRule in bagRules)
            {
                holdsBags[bagRule.Name] = HoldsBags(bagRule);
            }

            return holdsBags["shiny gold"];
        }

        int HoldsBags(BagRule bagRule)
        {
            if (holdsBags.ContainsKey(bagRule.Name))
                return holdsBags[bagRule.Name];

            if (bagRule.Contains.Count == 0)
            {
                holdsBags[bagRule.Name] = 0;
                return 0;
            }
            else
            {
                var cnt = 0;
                foreach (var contains in bagRule.Contains)
                {
                    var targetBagRule = bagRules.FirstOrDefault(b => b.Name == contains.Item1);
                    cnt += (contains.Item2 + contains.Item2 * HoldsBags(targetBagRule));
                }
                holdsBags[bagRule.Name] = cnt;
                return cnt;
            }
        }

        int GetCount(BagRule bagRule)
        {
            if (canHold.ContainsKey(bagRule.Name))
                return canHold[bagRule.Name];

            if (bagRule.Contains.Count == 0)
            {
                canHold[bagRule.Name] = 0;
                return 0;
            }
            else
            {
                var cnt = 0;
                foreach (var contains in bagRule.Contains)
                {
                    if (contains.Item1 == "shiny gold")
                    {
                        cnt += contains.Item2 * 1;
                    }
                    else
                    {
                        var targetBagRule = bagRules.FirstOrDefault(b => b.Name == contains.Item1);
                        cnt += contains.Item2 * GetCount(targetBagRule);
                    }
                }
                canHold[bagRule.Name] = cnt;
                return cnt;
            }
        }
    }
}
