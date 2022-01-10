using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day07Tests : DayTests<Day07, int>
    {
        protected override string Input => @"16,1,2,0,4,2,7,1,2,14";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(37, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(340987, day.CalculatePart1(Day07Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(168, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(96987874, day.CalculatePart2(Day07Input.Input));
        }
    }
}