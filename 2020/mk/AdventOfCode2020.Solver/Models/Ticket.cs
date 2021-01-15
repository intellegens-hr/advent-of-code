using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solver.Models
{
    internal class Ticket
    {
        public Ticket(string input)
        {
            this.Values = input.Split(",").Select(a => int.Parse(a)).ToList();
        }

        public List<int> Values { get; init; }

        public bool Valid(TicketDefinition definition)
        {
            return this.Values.All(v => definition.Validate(v));
        }
        public bool Valid(List<TicketDefinition> definitions)
        {
            return this.Values.All(v => definitions.Any(d => d.Validate(v)));
        }

        internal int GetErrorRate(List<TicketDefinition> definitions)
        {
            return this.Values.Where(v => definitions.All(d => !d.Validate(v))).Sum();
        }

        public int GetErrorRate(TicketDefinition definition)
        {
            return this.Values.Where(v => !definition.Validate(v)).Sum();
        }
    }
}