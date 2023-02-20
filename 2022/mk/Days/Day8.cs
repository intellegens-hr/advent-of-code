using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day8 : Puzzle<int>
{
    public override int Day => 8;

    public override int First()
    {
        var lines = GetInputLines();
        var matrix = GetMatrix(lines);
        var visibleTrees = new int[matrix.GetLength(0), matrix.GetLength(1)];
        var visibleTreesCount = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int? lastVisibleTree = null;

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                var currentTree = matrix[i, j];
                if (!lastVisibleTree.HasValue || currentTree > lastVisibleTree)
                {
                    lastVisibleTree = currentTree;

                    if (visibleTrees[i, j] == 0)
                    {
                        visibleTreesCount++;
                        visibleTrees[i, j] = 1;
                    }
                }
            }
        }

        Print(visibleTrees);

        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            int? lastVisibleTree = null;

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                var currentTree = matrix[j, i];
                if (!lastVisibleTree.HasValue || currentTree > lastVisibleTree)
                {
                    lastVisibleTree = currentTree;

                    if (visibleTrees[j, i] == 0)
                    {
                        visibleTreesCount++;
                        visibleTrees[j, i] = 1;
                    }
                }
            }
        }

        Print(visibleTrees);

        for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
        {
            int? lastVisibleTree = null;

            for (int j = matrix.GetLength(1) - 1; j >= 0; j--)
            {
                var currentTree = matrix[i, j];
                if (!lastVisibleTree.HasValue || currentTree > lastVisibleTree)
                {
                    lastVisibleTree = currentTree;

                    if (visibleTrees[i, j] == 0)
                    {
                        visibleTreesCount++;
                        visibleTrees[i, j] = 1;
                    }
                }
            }
        }

        Print(visibleTrees);

        for (int i = matrix.GetLength(1) - 1; i >= 0; i--)
        {
            int? lastVisibleTree = null;

            for (int j = matrix.GetLength(0) - 1; j >= 0; j--)
            {
                var currentTree = matrix[j, i];
                if (!lastVisibleTree.HasValue || currentTree > lastVisibleTree)
                {
                    lastVisibleTree = currentTree;

                    if (visibleTrees[j, i] == 0)
                    {
                        visibleTreesCount++;
                        visibleTrees[j, i] = 1;
                    }
                }
            }
        }

        Print(visibleTrees);

        return visibleTreesCount;
    }

    private void Print(int[,] array)
    {
        return;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private int[,] GetMatrix(string[] lines)
    {
        return lines.Select(l => l.Select(n => int.Parse(n.ToString()))).To2DArray<int>();
    }

    public override int Second()
    {
        var lines = GetInputLines();
        var matrix = GetMatrix(lines);
        var scenicScores = new int[matrix.GetLength(0), matrix.GetLength(1)];
        var maxScenicScore = 0;

        var directions = new List<(int x, int y)> { (0, 1), (0, -1), (-1, 0), (1, 0) };

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                var currentTree = matrix[i, j];
                var scenicScore = 1;

                foreach (var direction in directions)
                {
                    var (x, y) = (i + direction.x, j + direction.y);
                    var score = 0;

                    while (IsValidIndex(matrix, x, y))
                    {
                        score++;
                        var nextTree = matrix[x, y];

                        if (nextTree >= currentTree)
                        {
                            break;
                        }
                        else
                        {
                            (x, y) = (x + direction.x, y + direction.y);
                        }
                    }

                    scenicScore *= score;
                }

                scenicScores[i, j] = scenicScore;
                if (scenicScore > maxScenicScore)
                {
                    maxScenicScore = scenicScore;
                }
            }
        }

        Print(scenicScores);

        return maxScenicScore;

    }

    public bool IsValidIndex(int[,] array, int x, int y)
    {
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
    }

}
