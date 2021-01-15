using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle12
    {
        public enum Sides
        {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,
            WEST = 3
        }

        public static int Task1(IEnumerable<(char side, int moves)> input)
        {
            var sideFacing = Sides.EAST;
            (int x, int y) currentPosition = (0, 0);

            foreach (var (side, moves) in input)
            {
                if (side == 'R' || side == 'L')
                {
                    int steps = (side == 'R' ? 1 : -1) * (moves / 90);
                    sideFacing = (Sides)((4 + (int)sideFacing + steps) % 4);
                }
                else
                {
                    var sideToMove = side == 'F' ? sideFacing : side.ToSide();
                    currentPosition = Move(currentPosition.x, currentPosition.y, sideToMove, moves);
                }
            }

            return Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);
        }

        public static long Task2(IEnumerable<(char side, int moves)> input)
        {
            (int x, int y) currentPosition = (0, 0);
            (int x, int y) currentWaypointPosition = (10, 1);

            foreach (var (side, moves) in input)
            {
                var (x, y) = currentPosition;
                var (xw, yw) = currentWaypointPosition;

                if (side == 'R' || side == 'L')
                {
                    if (moves == 0)
                        continue;

                    // Always right turn
                    var rotationSteps = (moves / 90) % 4;
                    if (side == 'L')
                        rotationSteps = 4 - rotationSteps;

                    for (int i = 1; i <= rotationSteps; i++)
                    {
                        var coordSignX = yw >= 0 ? 1 : -1;
                        var coordSignY = xw >= 0 ? -1 : 1;

                        (xw, yw) = (Math.Abs(yw) * coordSignX, Math.Abs(xw) * coordSignY);
                    }
                }
                else if (side == 'F')
                {
                    x += xw * moves;
                    y += yw * moves;
                }
                else
                {
                    var sideToMove = side.ToSide();

                    if (sideToMove == Sides.EAST || sideToMove == Sides.WEST)
                    {
                        var sign = sideToMove == Sides.EAST ? 1 : -1;
                        xw += sign * moves;
                    }
                    else if (sideToMove == Sides.NORTH || sideToMove == Sides.SOUTH)
                    {
                        var sign = sideToMove == Sides.NORTH ? 1 : -1;
                        yw += sign * moves;
                    }
                }

                currentPosition = (x, y);
                currentWaypointPosition = (xw, yw);
            }

            return Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);
        }

        public static IEnumerable<(char side, int moves)> ToPuzzle12Input(this string input)
        => input
            .Split("\r\n")
            .Select(x => (x[0], Convert.ToInt32(x[1..])));

        private static (int x, int y) Move(int x, int y, Sides side, int moves)
        {
            if (side == Sides.EAST || side == Sides.WEST)
            {
                var sign = side == Sides.EAST ? 1 : -1;
                x += sign * moves;
            }
            else if (side == Sides.NORTH || side == Sides.SOUTH)
            {
                var sign = side == Sides.NORTH ? 1 : -1;
                y += sign * moves;
            }

            return (x, y);
        }

        private static Sides ToSide(this char input)
        => input == 'N'
            ? Sides.NORTH
            : input == 'S'
            ? Sides.SOUTH
            : input == 'E'
            ? Sides.EAST
            : input == 'W'
            ? Sides.WEST
            : throw new ArgumentException();
    }
}