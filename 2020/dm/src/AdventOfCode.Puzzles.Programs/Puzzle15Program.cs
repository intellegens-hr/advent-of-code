using System;

namespace AdventOfCode.Puzzles.Runner
{
    internal class Puzzle15Program
    {
        private static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result1 = Puzzle15.Task1(PuzzleInput.ToPuzzle15Input());
            watch.Stop();
            var elapsedMs = watch.Elapsed;
            Console.WriteLine($"Element: {result1} (elapsed: {elapsedMs})");

            watch = System.Diagnostics.Stopwatch.StartNew();
            var result2 = Puzzle15.Task1(PuzzleInput.ToPuzzle15Input(), 30000000);
            watch.Stop();
            elapsedMs = watch.Elapsed;
            Console.WriteLine($"Element: {result2} (elapsed: {elapsedMs})");
        }

        private static readonly string PuzzleInput = @"8,13,1,0,18,9";
    }
}