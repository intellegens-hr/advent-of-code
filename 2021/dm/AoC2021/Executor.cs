namespace AoC2021.Program;

internal static class Executor
{
    internal static void Execute<T>(int day, string input, Func<string, T> part1, Func<string, T> part2)
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        var result1 = part1(input);
        watch.Stop();
        var part1Elapsed = watch.ElapsedMilliseconds;

        watch.Reset();

        watch.Start();
        var result2 = part2(input);
        watch.Stop();
        var part2Elapsed = watch.ElapsedMilliseconds;

        Console.WriteLine($"Day {day}:");
        Console.WriteLine($"Result: {result1} ({part1Elapsed}ms)");
        Console.WriteLine($"Result: {result2} ({part2Elapsed}ms)");
        Console.WriteLine($"");
    }
}
