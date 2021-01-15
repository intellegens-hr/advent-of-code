using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle11Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(37, number);
        }




        [Fact]
        public void Task1_Input_002()
        {
            var stringInput = @"L";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(1, number);
        }

        [Fact]
        public void Task1_Input_003()
        {
            var stringInput = @"L..L";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(2, number);
        }

        [Fact]
        public void Task1_Input_004()
        {
            var stringInput = @"LLL
LLL
LLL";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(4, number);
        }

        [Fact]
        public void Task1_Input_005()
        {
            var stringInput = @"LLL
LL.
LLL";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(4, number);
        }

        [Fact]
        public void Task1_Input_006()
        {
            var stringInput = @".";

            var number = Puzzle11.Task1(stringInput.ToPuzzle11Input());
            Assert.Equal(0, number);
        }

        [Fact]
        public void Task2_Element()
        {
            var stringInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

            var number = Puzzle11.Task2(stringInput.ToPuzzle11Input());
            Assert.Equal(26, number);
        }
    }
}