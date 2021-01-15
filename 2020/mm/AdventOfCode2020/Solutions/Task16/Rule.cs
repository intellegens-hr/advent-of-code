using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task16
{
    public class Rule
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }

        public Rule(string line)
        {
            var parts = line.Split(':');
            this.Name = parts[0].Trim();
            var conditions = parts[1].Split(" or ");

            Conditions = conditions.Select(c => new Condition
            {
                From = Convert.ToInt32(c.Substring(0, c.IndexOf('-'))),
                To = Convert.ToInt32(c.Substring(c.IndexOf('-') + 1))
            }).ToList();

        }

        /// <summary>
        /// Returns if ticket is valid for number N.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsValid(int n)
        {
            return Conditions.Exists(c => c.From <= n && c.To >= n);
        }
    }

    public class Condition
    {
        public int From { get; set; }
        public int To { get; set; }
    }
}
