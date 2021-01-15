using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle13Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"939
7,13,x,x,59,x,31,19";

            var number = Puzzle13.Task1(stringInput.ToPuzzle13Input());
            Assert.Equal(295, number);
        }

        [Fact]
        public void Task2_1()
        {
            var stringInput = @"0
67,7,59,61";

            var number = Puzzle13.Task2(stringInput.ToPuzzle13Input().buses);
            Assert.Equal(754018, number);
        }

        [Fact]
        public void Task2_2()
        {
            var stringInput = @"0
67,x,7,59,61";

            var number = Puzzle13.Task2(stringInput.ToPuzzle13Input().buses);
            Assert.Equal(779210, number);
        }

        [Fact]
        public void Task2_3()
        {
            var stringInput = @"0
67,7,x,59,61";

            var number = Puzzle13.Task2(stringInput.ToPuzzle13Input().buses);
            Assert.Equal(1261476, number);
        }

        [Fact]
        public void Task2_4()
        {
            var stringInput = @"0
1789,37,47,1889";

            var number = Puzzle13.Task2(stringInput.ToPuzzle13Input().buses);
            Assert.Equal(1202161486, number);
        }
    }
}