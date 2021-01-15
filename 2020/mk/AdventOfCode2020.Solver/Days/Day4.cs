using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day4 : PuzzleDay<int>
    {
        public override int Day => 4;

        public override int First()
        {
            var input = GetInputText().Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            var passports = input.Select(i => new Passport(i));

            return passports.Count(p => p.HasRequiredFields());
        }

        public override int Second()
        {
            var input = GetInputText().Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            var passports = input.Select(i => new Passport(i));

            return passports.Count(p => p.IsValid());
        }
    }
}
