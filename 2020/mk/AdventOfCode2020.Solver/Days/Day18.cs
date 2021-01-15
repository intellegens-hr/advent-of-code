using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day18 : PuzzleDay<long>
    {
        public override int Day => 18;

        public override long First()
        {
            var input = GetInputLines();

            var dt = new DataTable();

            return (long)input.Sum(i => (long)((int)dt.Compute(i, "")));

        }

        public override long Second()
        {
            return -1;
        }
    }
}
