using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle02Tests
    {
        [Fact]
        public void PasswordPolicy1Tests()
        {
            var stringInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc
10-13 d: dddddddddddd
10-13 d: dddd
10-13 d: dddddddddddddd";

            var count = Puzzle02.CountValidPolicy1Passwords(stringInput.ToPuzzle2Input());
            Assert.Equal(3, count);
        }

        [Fact]
        public void PasswordPolicy2Tests()
        {
            var stringInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";

            var count = Puzzle02.CountValidPolicy2Passwords(stringInput.ToPuzzle2Input());
            Assert.Equal(1, count);
        }

    }
}