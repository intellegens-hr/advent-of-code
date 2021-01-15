using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day6 : PuzzleDay<int>
    {
        public override int Day => 6;

        public override int First()
        {
            var answers = GetInputText().Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            var cnt = 0;

            foreach (var answer in answers)
            {
                var distinctAnswers = answer.Replace("\r\n", "").Distinct().Count();
                cnt += distinctAnswers;
            }

            return cnt;
        }

        public override int Second()
        {
            var answers = GetInputText().Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            var cnt = 0;

            foreach (var answer in answers)
            {
                var distinctAnswers = answer
                    .Split(new[] { "\r\n" }, StringSplitOptions.None)
                    .Aggregate((a, b) => new string(a.Intersect(b).ToArray()))
                    .Count();

                cnt += distinctAnswers;
            }

            return cnt;
        }
    }
}
