using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day04 : DayBase<int>
    {
        public override int Day => 4;

        public override int First()
        {
            var lines = GetInputText().Split("\r\n\r\n").ToList();
            var numbers = lines[0].Split(',').Select(x => Int32.Parse(x)).ToList();
            var boards = new List<int[,]>();

            for (int x = 1; x < lines.Count; x++)
            {
                var line = lines[x];

                var board = new int[5, 5];

                var boardLines = line.Split("\r\n").Select(a => a.Split(new string[] { " ", "  " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray()).ToArray();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = boardLines[i][j];
                    }

                }
                boards.Add(board);

            }

            var boardRows = new List<Dictionary<int, int>>();
            var boardCols = new List<Dictionary<int, int>>();
            foreach (var number in numbers)
            {
                for (int b = 0; b < boards.Count; b++)
                {
                    var board = boards[b];
                    boardRows.Add(new Dictionary<int, int>());
                    boardCols.Add(new Dictionary<int, int>());
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (!boardRows[b].ContainsKey(i))
                                boardRows[b][i] = 0;
                            if (!boardCols[b].ContainsKey(j))
                                boardCols[b][j] = 0;

                            if (board[i, j] == number)
                            {
                                boardRows[b][i]++;
                                boardCols[b][j]++;
                                board[i, j] = -1;

                                if (boardRows[b][i] == 5 || boardCols[b][j] == 5)
                                {
                                    var sum = 0;
                                    for (int k = 0; k < 5; k++)
                                    {
                                        for (int l = 0; l < 5; l++)
                                        {
                                            if (board[k, l] != -1)
                                                sum += board[k, l];
                                        }

                                    }
                                    return sum * number;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public override int Second()
        {
            var lines = GetInputText().Split("\r\n\r\n").ToList();
            var numbers = lines[0].Split(',').Select(x => Int32.Parse(x)).ToList();
            var boards = new List<int[,]>();

            for (int x = 1; x < lines.Count; x++)
            {
                var line = lines[x];

                var board = new int[5, 5];

                var boardLines = line.Split("\r\n").Select(a => a.Split(new string[] { " ", "  " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray()).ToArray();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        board[i, j] = boardLines[i][j];
                    }

                }
                boards.Add(board);

            }

            var boardRows = new List<Dictionary<int, int>>();
            var boardCols = new List<Dictionary<int, int>>();
            var boardWins = boards.ToDictionary(a => boards.IndexOf(a), b => 0);
            foreach (var number in numbers)
            {
                for (int b = 0; b < boards.Count; b++)
                {
                    var board = boards[b];
                    boardRows.Add(new Dictionary<int, int>());
                    boardCols.Add(new Dictionary<int, int>());
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (!boardRows[b].ContainsKey(i))
                                boardRows[b][i] = 0;
                            if (!boardCols[b].ContainsKey(j))
                                boardCols[b][j] = 0;

                            if (board[i, j] == number)
                            {
                                boardRows[b][i]++;
                                boardCols[b][j]++;
                                board[i, j] = -1;

                                if (boardRows[b][i] == 5 || boardCols[b][j] == 5)
                                    boardWins[b] = 1;

                                if (boardWins.All(a => a.Value == 1))
                                {
                                    var sum = 0;
                                    for (int k = 0; k < 5; k++)
                                    {
                                        for (int l = 0; l < 5; l++)
                                        {
                                            if (board[k, l] != -1)
                                                sum += board[k, l];
                                        }

                                    }
                                    return sum * number;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

    }
}
