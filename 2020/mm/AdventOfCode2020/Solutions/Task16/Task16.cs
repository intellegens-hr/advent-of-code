using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task16
{
    public class Task16
    {
        public static long FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int ruleLines = lines.IndexOf("your ticket:") - 1;
            int otherTickets = lines.IndexOf("nearby tickets:") + 1;

            List<Rule> rules = CreateRules(lines, ruleLines);

            // Dictionary for controlling columns, which rules can be for which column
            Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
            for (int i = 0; i < rules.Count; i++)
            {
                dict.Add(i, rules.Select(r => r.Name).ToList());
            }

            List<string> validLines = new List<string>();

            int sum = 0;

            for (int i = otherTickets; i < lines.Count; i++)
            {
                var parts = lines[i].Split(',').Select(l => Convert.ToInt32(l)).ToList();
                bool isLineValid = true;
                foreach (var n in parts)
                {
                    bool isValidForRules = false;
                    foreach (var rule in rules)
                    {
                        if (rule.IsValid(n))
                        {
                            isValidForRules = true;
                            break;
                        }
                    }
                    
                    if (!isValidForRules)
                    {
                        sum += n;
                        isLineValid = false;
                        break;
                    }
                }

                if (isLineValid) validLines.Add(lines[i]);
            }


            for (int i = 0; i < rules.Count; i++)
            {
                for (int j = 0; j < validLines.Count; j++)
                {
                    var parts = validLines[j].Split(',').Select(l => Convert.ToInt32(l)).ToList();
                    
                    foreach (var rule in rules)
                    {
                        if (!rule.IsValid(parts[i]))
                        {
                            dict[i].Remove(rule.Name);
                        }
                    }
                }
            }

            long product = 1;
            var myTicketParts = lines[ruleLines + 2].Split(',').Select(l => Convert.ToInt32(l)).ToList();
            KeyValuePair<int, List<string>> kvPair = dict.FirstOrDefault(d => d.Value.Count == 1);
            while (kvPair.Value != null && kvPair.Value.Count > 0)
            {
                // We know for sure that index KEY is field VALUE[0].
                if(kvPair.Value[0].StartsWith("departure"))
                {
                    product *= myTicketParts[kvPair.Key];
                }

                // Remove the condition from all others
                string condition = kvPair.Value[0];
                foreach (var item in dict)
                {
                    item.Value.Remove(condition);
                }

                kvPair = dict.FirstOrDefault(d => d.Value.Count == 1);
            }

            return product;
        }

        private static List<Rule> CreateRules(List<string> lines, int ruleLines)
        {
            List<Rule> rules = new List<Rule>();
            for (int i = 0; i < ruleLines; i++)
            {
                rules.Add(new Rule(lines[i]));
                
            }
            return rules;
        }
    }
}
