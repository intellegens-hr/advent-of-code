namespace AdventOfCode2024;

public abstract class Puzzle<TResultFirst, TResultSecond>
{
    public abstract TResultFirst First(string input);
    public abstract TResultSecond Second(string input);
}

public abstract class Puzzle<TResult> : Puzzle<TResult, TResult>
{
}