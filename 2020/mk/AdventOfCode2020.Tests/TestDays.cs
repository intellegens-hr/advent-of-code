using AdventOfCode2020.Solver.Days;
using AdventOfCode2020.Solver.Models;
using NUnit.Framework;

namespace AdventOfCode2020.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }


        [TestCase(PuzzleNumber.First, 121396)]
        [TestCase(PuzzleNumber.Second, 73616634)]
        public void Day1(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day1().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 536)]
        [TestCase(PuzzleNumber.Second, 558)]
        public void Day2(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day2().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 151)]
        [TestCase(PuzzleNumber.Second, 7540141059)]
        public void Day3(PuzzleNumber puzzleNumber, long expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day3().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 247)]
        [TestCase(PuzzleNumber.Second, 145)]
        public void Day4(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day4().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 801)]
        [TestCase(PuzzleNumber.Second, 597)]
        public void Day5(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day5().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 6768)]
        [TestCase(PuzzleNumber.Second, 3489)]
        public void Day6(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day6().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 355)]
        [TestCase(PuzzleNumber.Second, 5312)]
        public void Day7(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day7().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 1654)]
        [TestCase(PuzzleNumber.Second, 833)]
        public void Day8(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day8().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 675280050)]
        [TestCase(PuzzleNumber.Second, 96081673)]
        public void Day9(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day9().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 2368)]
        [TestCase(PuzzleNumber.Second, 1727094849536)]
        public void Day10(PuzzleNumber puzzleNumber, long expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day10().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 2316)]
        [TestCase(PuzzleNumber.Second, 2128)]
        public void Day11(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day11().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 2879)]
        [TestCase(PuzzleNumber.Second, 178986)]
        public void Day12(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day12().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 3035)]
        [TestCase(PuzzleNumber.Second, 725169163285238)]
        public void Day13(PuzzleNumber puzzleNumber, long expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day13().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 6513443633260)]
        [TestCase(PuzzleNumber.Second, 3442819875191)]
        public void Day14(PuzzleNumber puzzleNumber, long expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day14().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 694)]
        [TestCase(PuzzleNumber.Second, 21768614)]
        public void Day15(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day15().GetResult(puzzleNumber));
        }

        [TestCase(PuzzleNumber.First, 21996)]
        [TestCase(PuzzleNumber.Second, 650080463519)]
        public void Day16(PuzzleNumber puzzleNumber, long expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day16().GetResult(puzzleNumber));
        }


        [TestCase(PuzzleNumber.First, 113)]
        public void Day19(PuzzleNumber puzzleNumber, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day19().GetResult(puzzleNumber));
        }
    }
}