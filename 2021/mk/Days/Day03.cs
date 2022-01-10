using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day03 : DayBase<int>
    {
        public override int Day => 3;

        //2967914
        //7041258

        public override int First()
        {
            var numbers = GetInputLines();
            
            var num1 = "";

            for (int i = 0; i < numbers[0].Length; i++)
            {
                if (numbers.Select(n => n[i]).Count(n => n == '1') > (numbers.Length / 2))
                {
                    num1 += "1";
                }
                else
                {
                    num1 += "0";
                }
            }

            var num2 = string.Join("", num1.Select(n => n == '1' ? '0' : '1'));

            return Convert.ToInt32(num1, 2) * Convert.ToInt32(num2, 2);
        }

        public override int Second()
        {
            var numbersraw = GetInputLines();
            var numbers = numbersraw;

            var num1 = "";

            for (int i = 0; i < numbers[0].Length; i++)
            {
                if (numbers.Select(n => n[i]).Count(n => n == '1') >= (numbers.Length / 2.0))
                {
                    numbers = numbers.Where(n => n[i] == '1').ToArray();
                }
                else
                {
                    numbers = numbers.Where(n => n[i] == '0').ToArray();
                }
                if (numbers.Length == 1)
                {
                    num1 = numbers[0];
                    break;
                }
            }
            numbers = numbersraw;

            var num2 = "";

            for (int i = 0; i < numbers[0].Length; i++)
            {
                if (numbers.Select(n => n[i]).Count(n => n == '1') < (numbers.Length / 2.0))
                {
                    numbers = numbers.Where(n => n[i] == '1').ToArray();
                }
                else
                {
                    numbers = numbers.Where(n => n[i] == '0').ToArray();
                }
                if (numbers.Length == 1)
                {
                    num2 = numbers[0];
                    break;
                }
            }

            return Convert.ToInt32(num1, 2) * Convert.ToInt32(num2, 2);
        }
    }
}
