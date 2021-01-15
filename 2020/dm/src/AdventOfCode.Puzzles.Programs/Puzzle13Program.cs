using System;

namespace AdventOfCode.Puzzles.Runner
{
    internal class Puzzle13Program
    {
        private static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result1 = Puzzle13.Task1(PuzzleInput.ToPuzzle13Input());
            watch.Stop();
            var elapsedMs = watch.Elapsed;
            Console.WriteLine($"Element: {result1} (elapsed: {elapsedMs})");

            watch = System.Diagnostics.Stopwatch.StartNew();
            var result2 = Puzzle13.Task2(PuzzleInput.ToPuzzle13Input().buses);
            watch.Stop();
            elapsedMs = watch.Elapsed;
            Console.WriteLine($"Element: {result2} (elapsed: {elapsedMs})");
        }

        private static readonly string PuzzleInput = @"1007153
29,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,433,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,19,x,x,x,23,x,x,x,x,x,x,x,977,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41";
    }
}