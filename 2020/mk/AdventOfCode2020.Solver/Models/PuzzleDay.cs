using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Solver.Models
{
    public enum PuzzleNumber
    {
        First,
        Second
    }

    public abstract class PuzzleDay<T>
    {
        public abstract int Day { get; }

        public abstract T First();
        public abstract T Second();

        public T GetResult(PuzzleNumber puzzleNumber)
        {
            switch (puzzleNumber)
            {
                case PuzzleNumber.First:
                    return First();
                case PuzzleNumber.Second:
                    return Second();
                default:
                    return default(T);
            }
        }

        public string[] GetInputLines()
        {
            return File.ReadAllLines($"Inputs/{Day}.txt");
        }
        public string GetInputText()
        {
            return File.ReadAllText($"Inputs/{Day}.txt");
        }
        public int[] GetInputInts()
        {
            return File.ReadAllLines($"Inputs/{Day}.txt").Select(l => Int32.Parse(l)).ToArray();
        }
    }
}