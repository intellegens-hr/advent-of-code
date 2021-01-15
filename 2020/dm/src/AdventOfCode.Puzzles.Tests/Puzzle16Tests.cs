using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle16Tests
    {
        [Fact]
        public void Task1_1()
        {
            var stringInput = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

            var number = Puzzle16.Task1(stringInput.ToPuzzle16Input());
            Assert.Equal(71, number);
        }

        [Fact]
        public void Task1_2()
        {
            var stringInput = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";

            var number = Puzzle16.Task2(stringInput.ToPuzzle16Input());
            Assert.Equal(71, number);
        }

    }
}