using AdventOfCode2025.Days;

namespace AdventOfCode2025.Tests;

public class TestDay01 : PuzzleTest<Day01, int>
{
    [Theory]
    [InlineData("example1.txt", 3)]
    [InlineData("input.txt", 980)]
    public void First(string path, int expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 6)]
    [InlineData("input.txt", 5961)]
    public void Second(string path, int expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay02 : PuzzleTest<Day02, long>
{
    [Theory]
    [InlineData("example1.txt", 1227775554)]
    [InlineData("input.txt", 13108371860)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 4174379265)]
    [InlineData("input.txt", 22471660255)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay03 : PuzzleTest<Day03, long>
{
    [Theory]
    [InlineData("example1.txt", 357)]
    [InlineData("input.txt", 17613)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 3121910778619)]
    [InlineData("input.txt", 175304218462560)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay04 : PuzzleTest<Day04, int>
{
    [Theory]
    [InlineData("example1.txt", 13)]
    [InlineData("input.txt", 1384)]
    public void First(string path, int expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 43)]
    [InlineData("input.txt", 8013)]
    public void Second(string path, int expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay05 : PuzzleTest<Day05, int, long>
{
    [Theory]
    [InlineData("example1.txt", 3)]
    [InlineData("input.txt", 623)]
    public void First(string path, int expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 14)]
    [InlineData("input.txt", 353507173555373)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay06 : PuzzleTest<Day06, long>
{
    [Theory]
    [InlineData("example1.txt", 4277556)]
    [InlineData("input.txt", 4693159084994)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 3263827)]
    [InlineData("input.txt", 11643736116335)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay07 : PuzzleTest<Day07, int, long>
{
    [Theory]
    [InlineData("example1.txt", 21)]
    [InlineData("input.txt", 1581)]
    public void First(string path, int expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 40)]
    [InlineData("input.txt", 73007003089792)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay08 : PuzzleTest<Day08, long>
{
    [Theory]
    [InlineData("example1.txt", 40, 10)]
    [InlineData("input.txt", 32103, 1000)]
    public void First(string path, long expectedResult, int limit) => RunFirst(path, expectedResult, limit);

    [Theory]
    [InlineData("example1.txt", 25272)]
    [InlineData("input.txt", 8133642976)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay09 : PuzzleTest<Day09, long>
{
    [Theory]
    [InlineData("example1.txt", 50)]
    [InlineData("input.txt", 4729332959)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 24)]
    [InlineData("input.txt", 1474477524)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay10 : PuzzleTest<Day10, long>
{
    [Theory]
    [InlineData("example1.txt", 7)]
    [InlineData("input.txt", 475)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 33)]
    [InlineData("input.txt", 1474477524)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay11 : PuzzleTest<Day11, long>
{
    [Theory]
    [InlineData("example1.txt", 5)]
    [InlineData("input.txt", 523)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example2.txt", 2)]
    [InlineData("input.txt", 517315308154944)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}

public class TestDay12 : PuzzleTest<Day12, long>
{
    [Theory]
    [InlineData("example1.txt", 2)]
    [InlineData("input.txt", 422)]
    public void First(string path, long expectedResult) => RunFirst(path, expectedResult);

    [Theory]
    [InlineData("example1.txt", 0)]
    [InlineData("input.txt", 0)]
    public void Second(string path, long expectedResult) => RunSecond(path, expectedResult);
}
