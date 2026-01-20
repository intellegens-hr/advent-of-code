namespace AdventOfCode2025;

public abstract class Puzzle<TResultFirst, TResultSecond>
{
    public virtual TResultFirst First(string input) => default!;
    public virtual TResultFirst First(string input, int param) => default!;
    public virtual TResultSecond Second(string input) => default!;
    public virtual TResultSecond Second(string input, int param) => default!;
}

public abstract class Puzzle<TResult> : Puzzle<TResult, TResult>
{
}