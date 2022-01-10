using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day02Tests: DayTests<Day02, int>
    {
        protected override string Input => @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(150, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(1815044, day.CalculatePart1(Day02Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(900, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(1739283308, day.CalculatePart2(Day02Input.Input));
        }
    }
}