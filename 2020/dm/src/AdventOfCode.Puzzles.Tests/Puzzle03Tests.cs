using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle03Tests
    {
        [Fact]
        public void TreesEncounteredTask1()
        {
            var stringInput = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

            var count = Puzzle03.CountTreesEncountered(stringInput.ToPuzzle3Input(), (3, 1));
            Assert.Equal(7, count);
        }

        [Fact]
        public void TreesEncounteredTask2()
        {
            var stringInput = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

            var steps = new (int down, int right)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1,2) };
            var count = Puzzle03.CountTreesEncounteredMultiplyByRows(stringInput.ToPuzzle3Input(), steps);
            Assert.Equal(336, count);
        }

    }
}