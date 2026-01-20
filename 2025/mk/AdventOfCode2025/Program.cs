using System.Diagnostics;
using AdventOfCode2025.Days;

namespace AdventOfCode2025;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Advent of Code 2025 - Benchmark Results");
        Console.WriteLine("========================================");
        Console.WriteLine();

        var benchmarks = new List<BenchmarkResult>
        {
            // Day 01
            RunBenchmark<Day01, int, int>("Day01", 980, 5961),

            // Day 02
            RunBenchmark<Day02, long, long>("Day02", 13108371860, 22471660255),

            // Day 03
            RunBenchmark<Day03, long, long>("Day03", 17613, 175304218462560),

            // Day 04
            RunBenchmark<Day04, int, int>("Day04", 1384, 8013),

            // Day 05
            RunBenchmark<Day05, int, long>("Day05", 623, 353507173555373),

            // Day 06
            RunBenchmark<Day06, long, long>("Day06", 4693159084994, 11643736116335),

            // Day 07
            RunBenchmark<Day07, int, long>("Day07", 1581, 73007003089792),

            // Day 08 (with param)
            RunBenchmarkWithParam<Day08, long, long>("Day08", 32103, 8133642976, 1000),

            // Day 09
            RunBenchmark<Day09, long, long>("Day09", 4729332959, 1474477524),

            // Day 10
            RunBenchmark<Day10, long, long>("Day10", 475, 0),

            // Day 11
            RunBenchmark<Day11, long, long>("Day11", 523, 517315308154944)
        };

        //// Day 12
        //benchmarks.Add(RunBenchmark<Day12, long, long>("Day12", 422, 0));

        // Print results
        PrintResults(benchmarks);

        Console.ReadLine();
    }

    private static BenchmarkResult RunBenchmark<TPuzzle, TFirst, TSecond>(
        string name,
        TFirst expectedFirst,
        TSecond expectedSecond)
        where TPuzzle : Puzzle<TFirst, TSecond>, new()
    {
        var puzzle = new TPuzzle();
        var inputPath = $"../../../../AdventOfCode2025/Inputs/{name}/input.txt";
        var input = File.ReadAllText(inputPath);

        Console.Write($"Running {name}...");

        // Warmup
        puzzle.First(input);
        puzzle.Second(input);

        // Benchmark Part 1
        var sw = Stopwatch.StartNew();
        var resultFirst = puzzle.First(input);
        sw.Stop();
        var part1Micros = sw.Elapsed.TotalMicroseconds;

        // Verify Part 1
        if (!EqualityComparer<TFirst>.Default.Equals(resultFirst, expectedFirst))
        {
            Console.WriteLine($" Part 1 FAILED! Expected {expectedFirst}, got {resultFirst}");
        }

        // Benchmark Part 2
        sw.Restart();
        var resultSecond = puzzle.Second(input);
        sw.Stop();
        var part2Micros = sw.Elapsed.TotalMicroseconds;

        // Verify Part 2
        if (!EqualityComparer<TSecond>.Default.Equals(resultSecond, expectedSecond))
        {
            Console.WriteLine($" Part 2 FAILED! Expected {expectedSecond}, got {resultSecond}");
        }

        Console.WriteLine(" Done");

        return new BenchmarkResult
        {
            Day = name,
            Part1Microseconds = part1Micros,
            Part2Microseconds = part2Micros,
            TotalMicroseconds = part1Micros + part2Micros
        };
    }

    private static BenchmarkResult RunBenchmarkWithParam<TPuzzle, TFirst, TSecond>(
        string name,
        TFirst expectedFirst,
        TSecond expectedSecond,
        int param)
        where TPuzzle : Puzzle<TFirst, TSecond>, new()
    {
        var puzzle = new TPuzzle();
        var inputPath = $"../../../../AdventOfCode2025/Inputs/{name}/input.txt";
        var input = File.ReadAllText(inputPath);

        Console.Write($"Running {name}...");

        // Warmup
        puzzle.First(input, param);
        puzzle.Second(input);

        // Benchmark Part 1 with param
        var sw = Stopwatch.StartNew();
        var resultFirst = puzzle.First(input, param);
        sw.Stop();
        var part1Micros = sw.Elapsed.TotalMicroseconds;

        // Verify Part 1
        if (!EqualityComparer<TFirst>.Default.Equals(resultFirst, expectedFirst))
        {
            Console.WriteLine($" Part 1 FAILED! Expected {expectedFirst}, got {resultFirst}");
        }

        // Benchmark Part 2
        sw.Restart();
        var resultSecond = puzzle.Second(input);
        sw.Stop();
        var part2Micros = sw.Elapsed.TotalMicroseconds;

        // Verify Part 2
        if (!EqualityComparer<TSecond>.Default.Equals(resultSecond, expectedSecond))
        {
            Console.WriteLine($" Part 2 FAILED! Expected {expectedSecond}, got {resultSecond}");
        }

        Console.WriteLine(" Done");

        return new BenchmarkResult
        {
            Day = name,
            Part1Microseconds = part1Micros,
            Part2Microseconds = part2Micros,
            TotalMicroseconds = part1Micros + part2Micros
        };
    }

    private static void PrintResults(List<BenchmarkResult> results)
    {
        Console.WriteLine();
        Console.WriteLine("Results:");
        Console.WriteLine("--------");
        Console.WriteLine($"{"Day",-8} {"Part 1 (μs)",15} {"Part 2 (μs)",15} {"Total (μs)",15}, {"Part 1 (ms)",15} {"Part 2 (ms)",15} {"Total (ms)",15}");
        Console.WriteLine(new string('-', 110));

        foreach (var result in results)
        {
            Console.WriteLine($"{result.Day,-8} {result.Part1Microseconds,15:N2} {result.Part2Microseconds,15:N2} {result.TotalMicroseconds,15:N2} {result.Part1Microseconds/1000,15:N2} {result.Part2Microseconds/1000,15:N2} {result.TotalMicroseconds/1000,15:N2}");
        }

        Console.WriteLine(new string('-', 110));

        var totalPart1 = results.Sum(r => r.Part1Microseconds);
        var totalPart2 = results.Sum(r => r.Part2Microseconds);
        var grandTotal = results.Sum(r => r.TotalMicroseconds);

        Console.WriteLine($"{"Total",-8} {totalPart1,15:N2} {totalPart2,15:N2} {grandTotal,15:N2} {totalPart1/1000,15:N2} {totalPart2 / 1000,15:N2} {grandTotal / 1000,15:N2} ");
        Console.WriteLine();
        Console.WriteLine($"Total execution time: {grandTotal / 1000:N2} ms ({grandTotal / 1_000_000:N3} seconds)");
    }

    private class BenchmarkResult
    {
        public string Day { get; set; } = "";
        public double Part1Microseconds { get; set; }
        public double Part2Microseconds { get; set; }
        public double TotalMicroseconds { get; set; }
    }
}