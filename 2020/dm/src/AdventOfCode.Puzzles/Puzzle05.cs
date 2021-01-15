using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle05
    {
        public static int FindSantasSeatId(IEnumerable<(IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps)> takenSeats)
        {
            var seats = takenSeats
                .Select(x => GetSeat(x))
                .Where(x => x.row != 0)
                .Where(x => x.row != 127)
                .Select(x => GetSeatId(x.row, x.column))
                .OrderBy(x => x);

            return seats
                .Where((x, i) => i > 0 && (x - 1) != (seats.ElementAt(i - 1)))
                .Select(x => x - 1)
                .First();
        }

        public static int MaxSeatId(IEnumerable<(IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps)> input)
        => input
            .Select(x => GetSeatId(x))
            .Max();

        public static IEnumerable<(IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps)> ToPuzzle5Input(this string input)
        => input.Split("\r\n").Select(x => x.ProcessPuzzleInputRow());

        private static (int row, int column) GetSeat((IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps) input)
        => (Reduce(input.verticalSteps, 128), Reduce(input.horizontalSteps, 8));

        private static int GetSeatId(int row, int column)
        => row * 8 + column;

        private static int GetSeatId((IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps) input)
        {
            var (row, column) = GetSeat(input);
            return GetSeatId(row, column);
        }

        private static (IEnumerable<bool> verticalSteps, IEnumerable<bool> horizontalSteps) ProcessPuzzleInputRow(this string input)
        {
            var verticalSteps = input
                .ToCharArray()
                .Where(x => x == 'F' || x == 'B')
                .Select(x => x == 'B');

            var horizontalSteps = input
                .ToCharArray()
                .Where(x => x == 'L' || x == 'R')
                .Select(x => x == 'R');

            return (verticalSteps, horizontalSteps);
        }

        private static int Reduce(IEnumerable<bool> steps, int length)
        {
            var rows = Enumerable.Range(0, length);
            foreach (var step in steps)
            {
                var middle = rows.Count() / 2;
                rows = rows.Skip(step ? middle : 0).Take(middle);
            }

            return rows.First();
        }
    }
}