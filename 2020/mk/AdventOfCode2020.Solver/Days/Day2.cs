using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day2 : PuzzleDay<int>
    {
        public override int Day => 2;

        public override int First()
        {
            var input = GetInputLines();
            var valid = 0;

            foreach (var item in input)
            {
                var pass = Regex.Matches(item, "(\\d+)-(\\d+) ([a-z]): ([a-z]+)");
                var min = Int32.Parse(pass[0].Groups[1].Value);
                var max = Int32.Parse(pass[0].Groups[2].Value);
                var character = pass[0].Groups[3].Value[0];
                var password = pass[0].Groups[4].Value;

                if (ValidatePasswordOccurences(min, max, character, password))
                {
                    valid++;
                }
            }

            return valid;
        }

        public override int Second()
        {
            var input = GetInputLines();
            var valid = 0;

            foreach (var item in input)
            {
                var pass = Regex.Matches(item, "(\\d+)-(\\d+) ([a-z]): ([a-z]+)");
                var first = Int32.Parse(pass[0].Groups[1].Value);
                var second = Int32.Parse(pass[0].Groups[2].Value);
                var character = pass[0].Groups[3].Value[0];
                var password = pass[0].Groups[4].Value;

                if (ValidatePasswordPositions(first, second, character, password))
                {
                    valid++;
                }
            }

            return valid;
        }

        bool ValidatePasswordOccurences(int min, int max, char character, string password)
        {
            var cnt = password.Count(c => c == character);

            if (cnt >= min && cnt <= max)
            {
                return true;
            }

            return false;
        }

        bool ValidatePasswordPositions(int first, int second, char character, string password)
        {
            if ((password[first - 1] == character) != (password[second - 1] == character))
            {
                return true;
            }

            return false;
        }
    }
}
