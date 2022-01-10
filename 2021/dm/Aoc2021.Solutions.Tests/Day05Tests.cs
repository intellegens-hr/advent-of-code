using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day05Tests : DayTests<Day05, int>
    {
        protected override string Input => @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(5, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(6666, day.CalculatePart1(Day05Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(12, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(19081, day.CalculatePart2(Day05Input.Input));
        }
    }
}