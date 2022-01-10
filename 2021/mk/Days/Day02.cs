using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day02 : DayBase<int>
    {
        public override int Day => 2;

        //2117664
        public override int First()
        {
            var numbers = GetInputLines().Select(n => new
            {
                direction = (Direction)Enum.Parse(typeof(Direction), n.Split(' ')[0]),
                magnitude = Int32.Parse(n.Split(' ')[1])
            }).ToArray();

            var position = (0, 0);

            foreach (var pos in numbers)
            {
                if (pos.direction == Direction.forward)
                {
                    position = (position.Item1 + pos.magnitude, position.Item2);
                }
                else if (pos.direction == Direction.down)
                {
                    position = (position.Item1, position.Item2 + pos.magnitude);
                }
                else if (pos.direction == Direction.up)
                {
                    position = (position.Item1, position.Item2 - pos.magnitude);
                }
            }

            return position.Item1 * position.Item2;
        }

        //2073416724
        public override int Second()
        {
            var numbers = GetInputLines().Select(n => new
            {
                direction = (Direction)Enum.Parse(typeof(Direction), n.Split(' ')[0]),
                magnitude = Int32.Parse(n.Split(' ')[1])
            }).ToArray();

            var position = (0, 0);
            var aim = 0;

            foreach (var pos in numbers)
            {
                if (pos.direction == Direction.forward)
                {
                    position = (position.Item1 + pos.magnitude, position.Item2 + aim * pos.magnitude);
                }
                else if (pos.direction == Direction.down)
                {
                    aim += pos.magnitude;
                }
                else if (pos.direction == Direction.up)
                {
                    aim -= pos.magnitude;
                }
            }

            return position.Item1 * position.Item2;
        }
    }
}
