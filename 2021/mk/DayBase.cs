using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public enum PuzzleNumber
    {
        First,
        Second
    }

    public abstract class DayBase<T>
    {
        public abstract int Day { get; }

        public string FilePath => $"Inputs/{Day.ToString("D2")}.txt";

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
            return File.ReadAllLines(FilePath);
        }
        public string GetInputText()
        {
            return File.ReadAllText(FilePath);
        }
        public int[] GetInputInts()
        {
            return File.ReadAllLines(FilePath).Select(l => Int32.Parse(l)).ToArray();
        }
    }
}