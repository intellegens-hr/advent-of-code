using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Models
{
    public class BagRule
    {
        public string Name { get; set; }
        public List<(string,int)> Contains { get; set; }

        public BagRule(string input)
        {
            var reg = Regex.Matches(input, "(?<source>\\w+ \\w+) bags contain (?:(?<num>\\d+) (?<dest>\\w+ \\w+) bags?(?:, |.))*");

            this.Name = reg[0].Groups["source"].Value;

            this.Contains = reg[0].Groups["dest"].Captures.Select((s, i) => (s.Value, Int32.Parse(reg[0].Groups["num"].Captures[i].Value))).ToList();
        }
    }
}
