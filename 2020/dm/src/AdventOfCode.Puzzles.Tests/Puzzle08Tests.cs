using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle08Tests
    {
        [Fact]
        public void Task1_Count()
        {
            var stringInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

            var count = Puzzle08.CountTask1(stringInput.ToPuzzle8Input());
            Assert.Equal(5, count);
        }

        [Fact]
        public void Task2_Count()
        {
            var stringInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

            var count = Puzzle08.CountTask2(stringInput.ToPuzzle8Input());
            Assert.Equal(8, count);
        }
    }
}