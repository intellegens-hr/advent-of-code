using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle12Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"F10
N3
F7
R90
R90
R90
R180
R270
L270
F11";

            var number = Puzzle12.Task1(stringInput.ToPuzzle12Input());
            Assert.Equal(25, number);
        }

        [Fact]
        public void Task2_Element()
        {
            var stringInput = @"F10
N3
F7
R90
N3
S3
W3
E3
F5
F6
R180
F5
R180
F5
R180
F5
F5
R180
F5
F5";

            var number = Puzzle12.Task2(stringInput.ToPuzzle12Input());
            Assert.Equal(286, number);
        }
    }
}