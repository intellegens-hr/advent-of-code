using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle15Tests
    {
        [Fact]
        public void Task1_1()
        {
            var stringInput = @"1,3,2";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(1, number);
        }

        [Fact]
        public void Task1_2()
        {
            var stringInput = @"2,1,3";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(10, number);
        }

        [Fact]
        public void Task1_3()
        {
            var stringInput = @"1,2,3";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(27, number);
        }

        [Fact]
        public void Task1_4()
        {
            var stringInput = @"2,3,1";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(78, number);
        }

        [Fact]
        public void Task1_5()
        {
            var stringInput = @"3,2,1";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(438, number);
        }

        [Fact]
        public void Task1_6()
        {
            var stringInput = @"3,1,2";

            var number = Puzzle15.Task1(stringInput.ToPuzzle15Input());
            Assert.Equal(1836, number);
        }

    }
}