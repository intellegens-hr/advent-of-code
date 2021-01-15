using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task5
{
    public class Task5
    {
        /// <summary>
        /// Task 5 - find the seat with the highest ID
        /// </summary>
        public static int FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int max = 0;

            var seats = new bool[817];

            foreach (var line in lines)
            {
                var rowBin = line.Substring(0, 7).Replace("F", "0").Replace("B", "1");
                var colBin = line.Substring(7).Replace("R", "1").Replace("L", "0");

                int row = Convert.ToInt32(rowBin, 2);
                int col = Convert.ToInt32(colBin, 2);

                int seatId = row * 8 + col;
                seats[seatId] = true;

                if (seatId > max) max = seatId;
            }

            // 1st part solution
            // return max;

            for (var i = 1; i < seats.Length - 1; i++)
            {
                if (seats[i] == false && seats[i - 1] && seats[i + 1]) return i;
            }

            return -1;
        }
    }
}
