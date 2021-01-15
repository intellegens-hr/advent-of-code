using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Models
{
    class BoardingPass
    {
        public const int ROWS = 128;
        public const int COLUMNS = 8;

        public int ID { get; set; }

        public BoardingPass(string line)
        {
            var start = 0;
            var end = ROWS;

            for (int i = 0; i < 7; i++)
            {
                var range = end - start;
                if (line[i] == 'F')
                {
                    end -= (range / 2);
                }
                else
                {
                    start += (range / 2);
                }
            }
            var row = start;

            start = 0;
            end = COLUMNS;
            for (int i = 7; i < 10; i++)
            {
                var range = end - start;
                if (line[i] == 'L')
                {
                    end -= (range / 2);
                }
                else
                {
                    start += (range / 2);
                }
            }
            var col = start;

            this.ID = row * COLUMNS + col;

        }
    }
}
