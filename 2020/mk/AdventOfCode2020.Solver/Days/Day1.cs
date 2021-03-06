﻿using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day1 : PuzzleDay<int>
    {
        public override int Day => 1;

        public override int First()
        {
            var input = GetInputInts();

            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        return input[i] * input[j];
                    }
                }
            }

            return -1;
        }

        public override int Second()
        {
            var input = GetInputInts();

            for (int i = 0; i < input.Length - 2; i++)
            {
                for (int j = i + 1; j < input.Length - 1; j++)
                {
                    for (int k = j + 2; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }

            return -1;
        }
    }
}
