using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle09Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

            var number = Puzzle09.FirstNonSummableElement(stringInput.ToPuzzle9Input(), 5);
            Assert.Equal(127, number);
        }

        [Fact]
        public void Task2_Sum()
        {
            var stringInput = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

            var sum = Puzzle09.FindRangeSum(stringInput.ToPuzzle9Input(), 5);
            Assert.Equal(62, sum);
        }
    }
}