using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day01 : DayBase<int>
    {
        public override int Day => 1;

        //1709
        public override int First()
        {
            var numbers = GetInputInts();
            var counter = 0;

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    counter++;
                }
            }

            return counter;
        }


        //1761
        public override int Second()
        {
            var numbers = GetInputInts();
            var counter = 0;

            int? lastSum = null;
            for (int i = 2; i < numbers.Length; i++)
            {
                var currentSum = numbers[i] + numbers[i - 1] + numbers[i - 2];

                if (lastSum.HasValue && currentSum > lastSum)
                {
                    counter++;
                }

                lastSum = currentSum;
            }

            return counter;
        }
    }
}
