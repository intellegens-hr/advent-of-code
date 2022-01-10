using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day01Tests : DayTests<Day01, int>
    {
        protected override string Input => @"199
200
208
210
200
207
240
269
260
263";

        public Day01Tests() : base()
        {
        }

        [Fact]
        public override void Part1()
        {
            Assert.Equal(7, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(1154, day.CalculatePart1(Day01Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(5, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.Equal(1127, day.CalculatePart2(Day01Input.Input));
        }
    }
}