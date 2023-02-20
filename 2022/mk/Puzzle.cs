namespace AdventOfCode2022
{
    public enum PuzzleNumber
    {
        First,
        Second
    }

    public abstract class Puzzle<T>
    {
        public abstract int Day { get; }
        public abstract T First();
        public abstract T Second();

        public string FilePath => $"../../../Inputs/Day{Day:D2}.txt";

        public T GetResult(PuzzleNumber puzzleNumber)
        {
            return puzzleNumber switch
            {
                PuzzleNumber.First => First(),
                PuzzleNumber.Second => Second(),
                _ => default!,
            };
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