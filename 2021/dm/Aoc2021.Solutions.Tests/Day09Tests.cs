using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day09Tests : DayTests<Day09, int>
    {
        protected override string Input => @"2199943210
3987894921
9856789892
8767896789
9899965678";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(15, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(554, day.CalculatePart1(Day09Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(1134, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(1017792, day.CalculatePart2(Day09Input.Input));
        }
    }
}