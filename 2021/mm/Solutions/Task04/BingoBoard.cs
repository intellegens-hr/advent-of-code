using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2021.Solutions.Task4
{
    public class BingoBoard
    {
        private int[,] _board = null;
        private int _sizeX;
        private int _sizeY;
        public int SumOfUnmarked { get; set; }

        public BingoBoard(int sizeX, int sizeY)
        {
            _board = new int[sizeY, sizeX];
            _sizeX = sizeX;
            _sizeY = sizeY;
            SumOfUnmarked = 0;
        }

        /// <summary>
        /// Method receives the number drawn. Checks for it in the board. If the number is on board, it changes it to -1. If victory, returns true;
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool DrawNumber(int n)
        {
            for (int i = 0; i < _sizeY; i++)
            {
                for (int j = 0; j < _sizeX; j++)
                {
                    if (_board[i, j] == n)
                    {
                        // Match found.
                        // 1. Move it to -1 and remove it from sum
                        SumOfUnmarked -= _board[i, j];
                        _board[i, j] = -1;

                        // 2. Check if victory
                        // 3. Return isVictory - no need to go further.
                        return CheckIfVictoy(i, j);
                    }
                }
            }

            return false;
        }

        private bool CheckIfVictoy(int y, int x)
        {
            bool isVictory = true;
            for (int i = 0; i < _sizeY; i++)
            {
                if (_board[y, i] != -1)
                {
                    isVictory = false;
                    break;
                }
            }

            if (isVictory) return true;

            for (int i = 0; i < _sizeX; i++)
            {
                if (_board[i, x] != -1) return false;
            }

            return true;
        }


        public bool ReadBoardFromFile(StreamReader reader)
        {
            for (var i = 0; i < _sizeY; i++)
            {
                var line = reader.ReadLine();
                if (line != null) {
                    var temp = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (var j = 0; j < temp.Length; j++)
                    {
                        _board[i, j] = Convert.ToInt32(temp[j]);
                        SumOfUnmarked += _board[i, j];
                    }
                }
            }

            return true;
        }
    }
}
