namespace AdventOfCode2025.Tests;

public class PuzzleTest<TPuzzleDay, TResult> : PuzzleTest<TPuzzleDay, TResult, TResult> where TPuzzleDay : Puzzle<TResult, TResult>, new()
{
}

public class PuzzleTest<TPuzzleDay, TResultFirst, TResultSecond> where TPuzzleDay : Puzzle<TResultFirst, TResultSecond>, new()
{
    private readonly TPuzzleDay _puzzle;

    public PuzzleTest()
    {
        _puzzle = new TPuzzleDay();
    }

    public void RunFirst(string fileName, TResultFirst expectedResult) => Run(_puzzle.First, fileName, expectedResult);

    public void RunSecond(string fileName, TResultSecond expectedResult) => Run(_puzzle.Second, fileName, expectedResult);

    public void RunFirst(string fileName, TResultFirst expectedResult, int param) => RunWithParam(_puzzle.First, fileName, expectedResult, param);

    public void RunSecond(string fileName, TResultSecond expectedResult, int param) => RunWithParam(_puzzle.Second, fileName, expectedResult, param);

    private void Run<TResult>(Func<string, TResult> func, string fileName, TResult expectedResult)
    {
        var path = $"../../../../AdventOfCode2025/Inputs/{typeof(TPuzzleDay).Name}/{fileName}";
        var input = File.ReadAllText(path);
        var result = func(input);

        Assert.Equal(expectedResult, result);
    }

    private void RunWithParam<TResult>(Func<string, int, TResult> func, string fileName, TResult expectedResult, int param)
    {
        var path = $"../../../../AdventOfCode2025/Inputs/{typeof(TPuzzleDay).Name}/{fileName}";
        var input = File.ReadAllText(path);
        var result = func(input, param);

        Assert.Equal(expectedResult, result);
    }
}