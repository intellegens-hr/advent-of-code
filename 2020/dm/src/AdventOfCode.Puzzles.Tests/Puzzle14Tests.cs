using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle14Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

            var number = Puzzle14.Task1(stringInput.ToPuzzle14Input());
            Assert.Equal(165, number);
        }

        [Fact]
        public void Task2_Element()
        {
            var stringInput = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

            var number = Puzzle14.Task2(stringInput.ToPuzzle14Input());
            Assert.Equal(208, number);
        }

    }
}