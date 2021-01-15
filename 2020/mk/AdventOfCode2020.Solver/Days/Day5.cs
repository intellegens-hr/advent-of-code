using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day5 : PuzzleDay<int>
    {
        public override int Day => 5;

        public override int First()
        {
            var boardingpasses = GetInputLines().Select(b => new BoardingPass(b));
            return boardingpasses.Max(b => b.ID);
        }

        public override int Second()
        {
            var boardingpasses = GetInputLines().Select(b => new BoardingPass(b)).OrderBy(b => b.ID).ToList();

            for (int i = 1; i < boardingpasses.Count - 1; i++)
            {
                if (boardingpasses[i].ID != boardingpasses[i + 1].ID - 1)
                {
                    return boardingpasses[i].ID + 1;
                }
            }

            return -1;
        }
    }
}
