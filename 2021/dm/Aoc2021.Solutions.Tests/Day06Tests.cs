using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day06Tests : DayTests<Day06, long>
    {
        protected override string Input => @"3,4,3,1,2";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(5934, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(355386, day.CalculatePart1(Day06Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(26984457539, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(1613415325809, day.CalculatePart2(Day06Input.Input));
        }
    }
}