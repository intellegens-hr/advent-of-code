using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day9 : PuzzleDay<long>
    {
        public override int Day => 9;

        int PREAMBLE_LENGTH = 25;

        public override long First()
        {
            var input = GetInputLines().Select(a => long.Parse(a)).ToArray();
            for (int i = PREAMBLE_LENGTH; i < input.Length; i++)
            {
                if (!Occurs(input, i))
                    return input[i];
            }

            return -1;
        }

        bool Occurs(long[] input, int i)
        {
            for (int m = i - PREAMBLE_LENGTH; m < i - 1; m++)
            {
                for (int n = m + 1; n < i; n++)
                {
                    if (input[m] + input[n] == input[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override long Second()
        {
            var input = GetInputLines().Select(a => long.Parse(a)).ToArray();
            var wantedSum = 675280050;
            var start = 0;
            var end = 1;
            var sum = input[start] + input[end];

            while (sum != wantedSum)
            {
                if (sum < wantedSum)
                {
                    end++;
                    sum += input[end];
                }
                else
                {
                    while (sum > wantedSum)
                    {
                        sum -= input[start];
                        start++;
                    }
                }
            }

            return input.ToList().Skip(start).Take(end - start).Min() + input.ToList().Skip(start).Take(end - start).Max();

        }
    }
}
