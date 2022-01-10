using AoC2021.Solutions.Models.Day04;

namespace AoC2021.Solutions
{
    public class Day04 : DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            var (numbersDrawn, boards) = ParseInput(input);

            foreach (var number in numbersDrawn)
            {
                foreach (var board in boards)
                {
                    board.FlagNumber(number);
                    if (board.HasBingo())
                    {
                        return board.Measure;
                    }
                }
            }

            return -1;
        }

        public override int CalculatePart2(string input)
        {
            var (numbersDrawn, boards) = ParseInput(input);
            Board lastBoard = null;

            foreach (var number in numbersDrawn)
            {
                foreach (var board in boards)
                {
                    board.FlagNumber(number);
                }

                boards.RemoveAll(x => x.HasBingo());

                if (boards.Count == 1)
                    lastBoard = boards[0];
                else if (boards.Count == 0)
                    break;
            }

            return lastBoard.Measure;
        }

        private static (IEnumerable<int> numbersDrawn, List<Board> boards) ParseInput(string input)
        {
            var split = input
                .Split("\r\n")
                .Select(x => x.Trim().Replace("  ", " "));

            var numbersDrawn = split.First().Split(",").Select(x => int.Parse(x));
            var boards = new List<Board>();

            var boardRows = new List<int[]>();
            foreach (var row in split.Skip(2))
            {
                if (string.IsNullOrEmpty(row))
                {
                    boards.Add(new Board(boardRows));
                    boardRows = new List<int[]>();
                }
                else
                {
                    boardRows.Add(row.Split(" ").Select(x => int.Parse(x)).ToArray());
                }
            }
            boards.Add(new Board(boardRows));

            return (numbersDrawn, boards);
        }
    }
}