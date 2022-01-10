using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Solutions.Task6
{
    public class Fish
    {
        public int Days { get; set; }
        public int TotalDays { get; set; }

        public Fish(int days)
        {
            Days = days;
        }

        public Fish (int days, int totalDays)
        {
            Days = days;
            TotalDays = totalDays;
        }

        
        
    }
}
