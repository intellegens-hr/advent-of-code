using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day10 : PuzzleDay<long>
    {
        public override int Day => 10;

        public override long First()
        {
            var input = GetInputInts().Append(0);

            var diff = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 } };
            var ordered = input.OrderBy(a => a).Aggregate((a, b) =>
            {
                diff[b - a]++;
                return b;
            });

            // final adapter
            diff[3]++;

            return diff[3] * diff[1];
            
        }

        public override long Second()
        {
            var input = GetInputInts().OrderBy(a => a).Prepend(0).ToArray();
            input = input.Append(input.Last() + 3).ToArray();

            var memoValues = new Dictionary<int, long>();


            return PathsFrom(input, 0, memoValues);
        }

        long PathsFrom(int[] input, int i, Dictionary<int, long> memoValues)
        {

            if (memoValues.ContainsKey(input[i]))
                return memoValues[input[i]];

            long pathsFrom = 0;

            if (input.Length - 1 == i)
            {
                pathsFrom = 1;
            }
            else
            {
                for (int j = 1; j <= 3 && i + j < input.Length; j++)
                {
                    if (input[i + j] - input[i] <= 3)
                    {
                        pathsFrom += PathsFrom(input, i + j, memoValues);
                    }
                }
            }

            memoValues[input[i]] = pathsFrom;

            return pathsFrom;
        }
    }
}
