using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle01Tests
    {
        [Fact]
        public void TuplesLength2()
        {
            var data = new int[] { 1721, 979, 366, 299, 675, 1456 };
            var requiredSum = 2020;

            var product = Puzzle01.TuplesProductForGivenSum(data, requiredSum, 2);
            Assert.Equal(514579, product);
        }

        [Fact]
        public void TuplesLength3()
        {
            var data = new int[] { 1721, 979, 366, 299, 675, 1456 };
            var requiredSum = 2020;

            var product = Puzzle01.TuplesProductForGivenSum(data, requiredSum, 3);
            Assert.Equal(241861950, product);
        }
    }
}