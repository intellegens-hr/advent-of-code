using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day07 : DayBase<long>
    {
        public override int Day => 7;

        public override long First()
        {
            var numbers = GetInputText().Split(',').Select(x => Int32.Parse(x)).ToList();
            var range = new { min = numbers.Min(), max = numbers.Max() };

            var minFuel = long.MaxValue;

            for (int i = range.min; i < range.max; i++)
            {
                var fuel = numbers.Select(n => Math.Abs(n - i)).Sum();
                if (fuel < minFuel)
                    minFuel = fuel;

            }

            return minFuel;
        }

        public override long Second()
        {
            var numbers = GetInputText().Split(',').Select(x => Int32.Parse(x)).ToList();
            var range = new { min = numbers.Min(), max = numbers.Max() };

            var minFuel = long.MaxValue;

            for (int i = range.min; i <= range.max; i++)
            {
                var fuel = 0;
                foreach (var n in numbers)
                {
                    var dist = Math.Abs(n - i);
                    fuel += dist * (dist + 1) / 2;
                }

                if (fuel < minFuel)
                    minFuel = fuel;

            }

            return minFuel;
        }
    }
}
