using Microsoft.Win32;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static AdventOfCode2022.Days.Day9;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days;

public class Day17 : Puzzle<int>
{
    public override int Day => 17;

    public override int First()
    {
        var instructions = GetInputText();
        var instructionsCount = instructions.Count();
        var instructionIndex = 0;
        var shapes = new List<Func<Shape>> {
            () => new Shape { Points = new() { (0,0), (1,0), (2,0), (3,0) } },
            () => new Shape { Points = new() { (0,1), (1,0), (1,1),  (1,2), (2, 1)} },
            () => new Shape { Points = new() { (0,0), (1,0), (2,0), (2,1), (2,2) } },
            () => new Shape { Points = new() { (0,0), (0,1), (0,2), (0,3) } },
            () => new Shape { Points = new() { (0,0), (0,1), (1,0), (1,1) } }
        };
        var shapeIndex = 0;
        var board = new int[7, 100000];
        for (int i = 0; i < 7; i++)
        {
            board[i, 0] = 1;
        }

        var boardHeight = new int[7];
        //for (int i = 0; i < 7; i++)
        //{
        //    boardHeight[i] = 1;
        //}


        while (shapeIndex < 2022)
        {
            //Print(board);
            var newShape = shapes[shapeIndex++ % 5].Invoke();

            newShape.Add(2, boardHeight.Max() + 4);
            while (true)
            {
                var newInstruction = instructions[instructionIndex++ % instructionsCount];
                var sideways = newInstruction == '>' ? 1 : -1;

                if (newShape.CanMoveSideways(sideways, board))
                {
                    newShape.Add(sideways, 0);
                }

                if (newShape.CanMoveDown(board))
                {
                    newShape.Add(0, -1);
                    continue;
                }

                foreach (var point in newShape.Points)
                {
                    board[point.x, point.y] = 1;

                    if (boardHeight[point.x] < point.y)
                        boardHeight[point.x] = point.y;
                }

                break;
                // if nextPosition legal
                // move to next position

                // if down legal
                // move down
                // else
                // add position to board

            }

        }
        return boardHeight.Max();
    }

    private void Print(int[,] board)
    {
        for (int j = 20; j >= 0; j--)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(board[i, j] == 1 ? "#" : ".");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public override int Second()
    {
        var instructions = GetInputText();
        var instructionsCount = instructions.Count();
        var instructionIndex = 0;
        var shapes = new List<Func<Shape>> {
            () => new Shape { Points = new() { (0,0), (1,0), (2,0), (3,0) } },
            () => new Shape { Points = new() { (0,1), (1,0), (1,1),  (1,2), (2, 1)} },
            () => new Shape { Points = new() { (0,0), (1,0), (2,0), (2,1), (2,2) } },
            () => new Shape { Points = new() { (0,0), (0,1), (0,2), (0,3) } },
            () => new Shape { Points = new() { (0,0), (0,1), (1,0), (1,1) } }
        };
        var shapeIndex = 0;
        var board = new int[7, 10000000];
        for (int i = 0; i < 7; i++)
        {
            board[i, 0] = 1;
        }

        var boardHeight = new int[7];

        var shapeIndexLast = 0;
        var instructionIndexLast = 0;

        while (shapeIndex < 10000000)
        {
            if (boardHeight.All(a => a == boardHeight[0]))
            {
                Console.WriteLine(instructionIndex - instructionIndexLast);
                Console.WriteLine(shapeIndex - shapeIndexLast);



                shapeIndexLast = shapeIndex;
                instructionIndexLast = instructionIndex;
            }
            //Print(board);
            var newShape = shapes[shapeIndex++ % 5].Invoke();

            newShape.Add(2, boardHeight.Max() + 4);
            while (true)
            {
                var newInstruction = instructions[instructionIndex++ % instructionsCount];
                var sideways = newInstruction == '>' ? 1 : -1;

                if (newShape.CanMoveSideways(sideways, board))
                {
                    newShape.Add(sideways, 0);
                }

                if (newShape.CanMoveDown(board))
                {
                    newShape.Add(0, -1);
                    continue;
                }

                foreach (var point in newShape.Points)
                {
                    board[point.x, point.y] = 1;

                    if (boardHeight[point.x] < point.y)
                        boardHeight[point.x] = point.y;
                }

                break;

            }

        }
        return boardHeight.Max();
    }

}

public class Shape
{
    public List<(int x, int y)> Points { get; set; } = new();

    public void Add(int x, int y)
    {
        for (int i = 0; i < Points.Count; i++)
        {
            Points[i] = (Points[i].x + x, Points[i].y + y);
        }
    }

    internal bool CanMoveSideways(int x, int[,] board)
    {
        if (Points[0].x + x < 0)
            return false;

        if (Points.Last().x + x > 6)
            return false;

        for (int i = 0; i < Points.Count; i++)
        {
            if (board[Points[i].x + x, Points[i].y] == 1)
                return false;
        }

        return true;
    }

    internal bool CanMoveDown(int[,] board)
    {
        for (int i = 0; i < Points.Count; i++)
        {
            if (Points[i].y - 1 < 0)
                return false;

            if (board[Points[i].x, Points[i].y - 1] == 1)
                return false;
        }
        return true;
    }
}
