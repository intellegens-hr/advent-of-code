using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public enum TileTypes
    {
        FLOOR = 0,
        SEAT = 1,
        SEAT_OCCUPIED = 2
    }

    public static class Puzzle11
    {
        public static long Task1(List<List<TileTypes>> input)
        {
            while (true)
                if (!input.ProcessRules(4, false))
                    break;

            return input
                .Select(x => x.Where(y => y == TileTypes.SEAT_OCCUPIED).Count())
                .Sum();
        }

        public static long Task2(List<List<TileTypes>> input)
        {
            while (true)
                if (!input.ProcessRules(5, true))
                    break;

            return input
                .Select(x => x.Where(y => y == TileTypes.SEAT_OCCUPIED).Count())
                .Sum();
        }

        public static List<List<TileTypes>> ToPuzzle11Input(this string input)
        => input
            .Split("\r\n")
            .Select(x => x.ToCharArray()
                        .Select(y => y == 'L'
                                    ? TileTypes.SEAT
                                    : y == '#'
                                    ? TileTypes.SEAT_OCCUPIED
                                    : y == '.'
                                    ? TileTypes.FLOOR
                                    : throw new System.Exception())
                        .ToList())
            .ToList();

        private static (int matches, int maxMatches) FindMatches(this List<List<TileTypes>> input, int x, int y, TileTypes match, bool processAllVisible = false)
        {
            var maxMatches = 0;
            var matches = 0;

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    TileTypes? element = processAllVisible ? GetFirstSeat(input, x, y, i, j) : GetFirstSeatAdjacent(input, x, y, i, j);

                    if (element == TileTypes.FLOOR || element == null)
                        continue;

                    maxMatches++;
                    matches += (element == match) ? 1 : 0;
                }

            return (matches, maxMatches);
        }

        private static TileTypes? GetFirstSeat(this List<List<TileTypes>> input, int x, int y, int xIncrement, int yIncrement)
        {
            var row = x + xIncrement;
            var col = y + yIncrement;
            TileTypes? element = null;

            while (row >= 0 && row < input.Count && col >= 0 && col < input[x].Count)
            {
                element = input[row][col];
                if (element != TileTypes.FLOOR)
                    return element;

                row += xIncrement;
                col += yIncrement;
            }

            return element;
        }

        private static TileTypes? GetFirstSeatAdjacent(this List<List<TileTypes>> input, int x, int y, int xIncrement, int yIncrement)
        {
            var row = x + xIncrement;
            var col = y + yIncrement;
            TileTypes? element = null;

            if (row >= 0 && row < input.Count && col >= 0 && col < input[x].Count )
            {
                element = input[row][col];
                if (element != TileTypes.FLOOR)
                    return element;
            }

            return element;
        }

        private static bool ProcessRules(this List<List<TileTypes>> input, int maxOccupied, bool processAllVisible = false)
        {
            var copy = input.Select(x => x.Select(y => y).ToList()).ToList();

            var rows = input.Count;
            var cols = input[0].Count;
            var changed = false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var element = input[i][j];

                    if (element == TileTypes.SEAT)
                    {
                        var (matches, maxMatches) = copy.FindMatches(i, j, TileTypes.SEAT, processAllVisible);
                        if (matches == maxMatches)
                        {
                            input[i][j] = TileTypes.SEAT_OCCUPIED;
                            changed = true;
                        }
                    }

                    if (element == TileTypes.SEAT_OCCUPIED)
                    {
                        var (matches, maxMatches) = copy.FindMatches(i, j, TileTypes.SEAT_OCCUPIED, processAllVisible);
                        if (matches >= maxOccupied)
                        {
                            input[i][j] = TileTypes.SEAT;
                            changed = true;
                        }
                    }
                }
            }

            return changed;
        }
    }
}