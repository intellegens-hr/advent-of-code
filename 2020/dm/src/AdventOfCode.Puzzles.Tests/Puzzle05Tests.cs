using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle05Tests
    {
        [Fact]
        public void Task1_MaxId1()
        {
            var stringInput = @"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL";

            var maxId = Puzzle05.MaxSeatId(stringInput.ToPuzzle5Input());
            Assert.Equal(820, maxId);
        }

        [Fact]
        public void Task1_MaxId2()
        {
            var stringInput = @"BFFFBBFRRR";

            var maxId = Puzzle05.MaxSeatId(stringInput.ToPuzzle5Input());
            Assert.Equal(567, maxId);
        }

        [Fact]
        public void Task1_MaxId3()
        {
            var stringInput = @"BBFFBBFRLL";

            var maxId = Puzzle05.MaxSeatId(stringInput.ToPuzzle5Input());
            Assert.Equal(820, maxId);
        }
    }
}