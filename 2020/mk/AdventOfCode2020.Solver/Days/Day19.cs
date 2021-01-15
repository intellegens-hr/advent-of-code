using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day19 : PuzzleDay<int>
    {
        public override int Day => 19;

        public Dictionary<int,string> Rules { get; set; }
        public override int First()
        {
            var input = GetInputText().Split("\r\n\r\n");
            Rules = input[0].Split("\r\n").Select(r => (int.Parse(r.Split(": ")[0]), r.Split(": ")[1])).OrderBy(a => a.Item1).ToDictionary(a => a.Item1, a => a.Item2);

            var regex = "^" + GetRegexFor(Rules[0]) + "$";
            var matches = 0;

            foreach (var line in input[1].Split("\r\n"))
            {
                if (Regex.IsMatch(line, regex))
                    matches++;
            }

            return matches;
        }

        Dictionary<string, string> regexMemo = new Dictionary<string, string>();

        string GetRegexFor(string rule)
        {
            //Console.WriteLine(rule);
            var regex = "";

            if (regexMemo.ContainsKey(rule))
            {
                regex = regexMemo[rule];
            }
            else
            {
                if (rule == "\"a\"")
                    regex = "a";
                else if (rule == "\"b\"")
                    regex = "b";
                else if (rule.Contains(" | "))
                {
                    regex = $"({GetRegexFor(rule.Split(" | ")[0])}|{GetRegexFor(rule.Split(" | ")[1])})";
                }
                else
                {
                    foreach (var next in rule.Split(" "))
                    {
                        var nxt = int.Parse(next);
                        //if (nxt == 8)
                        //{
                        //    if (occuredEight == false)
                        //    {
                        //        occuredEight = true;
                        //        regex += $"(?<eight>{GetRegexFor(Rules[nxt])})";
                        //    }
                        //    else
                        //    {
                        //        regex += "(\\g<eight>)";
                        //        continue;
                        //    }
                        //}
                        //else if (nxt == 11)
                        //{
                        //    if (occuredEleven == false)
                        //    {
                        //        occuredEleven = true;
                        //        regex += $"(?<eleven>{GetRegexFor(Rules[nxt])})";
                        //    }
                        //    else
                        //    {
                        //        regex += "(\\g<eleven>)";
                        //        continue;
                        //    }
                        //}
                        //else
                        {
                            regex += GetRegexFor(Rules[nxt]);
                        }
                    }
                }
            }

            regexMemo[rule] = regex;

            return regex;
        }

        bool occuredEight = false;
        bool occuredEleven = false;

        public override int Second()
        {
            var input = GetInputText().Split("\r\n\r\n");
            Rules = input[0].Split("\r\n").Select(r => (int.Parse(r.Split(": ")[0]), r.Split(": ")[1])).OrderBy(a => a.Item1).ToDictionary(a => a.Item1, a => a.Item2);

            //8: 42 | 42 8
            //11: 42 31 | 42 11 31

            Rules[8] = "42 | 42 8";
            Rules[11] = "42 31 | 42 11 31";

            var regex = "^" + GetRegexFor(Rules[0]) + "$";
            var matches = 0;

            foreach (var line in input[1].Split("\r\n"))
            {
                if (Regex.IsMatch(line, regex))
                    matches++;
            }

            return matches;
        }
    }
}
