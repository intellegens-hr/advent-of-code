using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle06Tests
    {
        [Fact]
        public void Task1_Count()
        {
            var stringInput = @"abc

a
b
c

ab
ac

a
a
a
a

b";

            var maxId = Puzzle06.CountTask1(stringInput.ToPuzzle6Input());
            Assert.Equal(11, maxId);
        }

        [Fact]
        public void Task2_Count()
        {
            var stringInput = @"abc

a
b
c

ab
ac

a
a
a
a

b";

            var maxId = Puzzle06.CountTask2(stringInput.ToPuzzle6Input());
            Assert.Equal(6, maxId);
        }
    }
}