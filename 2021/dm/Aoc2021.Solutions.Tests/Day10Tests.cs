using AoC2021.Program.Inputs;
using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public class Day10Tests : DayTests<Day10, long>
    {
        protected override string Input => @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

        [Fact]
        public override void Part1()
        {
            Assert.Equal(26397, day.CalculatePart1(Input));
        }

        [Fact]
        public override void Part1_FullInput()
        {
            Assert.Equal(290691, day.CalculatePart1(Day10Input.Input));
        }

        [Fact]
        public override void Part2()
        {
            Assert.Equal(288957, day.CalculatePart2(Input));
        }

        [Fact]
        public override void Part2_FullInput()
        {
            Assert.True(day.CalculatePart2(Day10Input.Input) > 233576613);

            Assert.Equal(2768166558, day.CalculatePart2(Day10Input.Input));
        }
    }
}