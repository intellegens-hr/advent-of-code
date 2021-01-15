using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task7
{
    public class Node
    {
        public class ChildColor
        {
            public string Color { get; set; }
            public int Amount { get; set; }
        }


        public string Color { get; set; }
        public List<string> Parents { get; set; }
        public Dictionary<string, ChildColor> Children { get; set; }
    }
}
