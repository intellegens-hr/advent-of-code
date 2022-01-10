using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day03Tests : DayTests<Day03, int>
    {
        protected override string Input => @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(198, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(3923414, day.CalculatePart1(Day03Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(230, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(5852595, day.CalculatePart2(Day03Input.Input));
        }
    }
}