using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Solutions.Task4
{
    public static class Task4
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task4\\Input.txt");
            string line = string.Empty;
            bool succesfullyRead = true;
            List<BingoBoard> boards = new List<BingoBoard>();

            // Read first line and the empty line;
            int[] numbers = reader.ReadLine().Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();
            line = reader.ReadLine();


            while (succesfullyRead == true && line != null)
            {
                var newBoard = new BingoBoard(5, 5);
                if (!newBoard.ReadBoardFromFile(reader)) succesfullyRead = false;

                if (succesfullyRead)
                {
                    boards.Add(newBoard);
                }

                line = reader.ReadLine();
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                foreach (var board in boards)
                {
                    if (board.DrawNumber(numbers[i]))
                    {
                        return numbers[i] * board.SumOfUnmarked;
                    }
                }
            }

            return 0;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task4\\Input.txt");
            string line = string.Empty;
            bool succesfullyRead = true;
            List<BingoBoard> boards = new List<BingoBoard>();

            // Read first line and the empty line;
            int[] numbers = reader.ReadLine().Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();
            line = reader.ReadLine();


            while (succesfullyRead == true && line != null)
            {
                var newBoard = new BingoBoard(5, 5);
                if (!newBoard.ReadBoardFromFile(reader)) succesfullyRead = false;

                if (succesfullyRead)
                {
                    boards.Add(newBoard);
                }

                line = reader.ReadLine();
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < boards.Count; j++)
                {
                    var board = boards[j];
                    if (board.DrawNumber(numbers[i]))
                    {
                        if (boards.Count == 1)
                            return numbers[i] * board.SumOfUnmarked;
                        else
                        {
                            boards.Remove(board);
                            j--;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
