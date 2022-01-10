using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day06 : DayBase<long>
    {
        public override int Day => 6;

        //361169
        //1634946868992


        public override long First()
        {
            return FishCountAfterDays(80);
        }

        public override long Second()
        {
            return FishCountAfterDays(256);
        }

        private long FishCountAfterDays(int limit)
        {
            var numbers = GetInputText().Split(',').Select(x => Int32.Parse(x)).ToList();
            var day = 0;
            var daycount = new List<(int, int)>() { (0, numbers.Count) };

            var additions = new List<long>() { 0, 0, 0, 0, 0, 0, 1, 0, 1 };
            var totals = new List<long>() { 2 };

            while (day <= limit)
            {
                additions.Add(additions[day] + additions[day + 2]);
                totals.Add(totals.Last() + additions[day]);
                day++;
            }

            long totalSum = 0;
            for (var i = 0; i < numbers.Count; i++)
            {
                totalSum += totals[limit - 1 - numbers[i]];
            }

            return totalSum;
        }
    }
}
