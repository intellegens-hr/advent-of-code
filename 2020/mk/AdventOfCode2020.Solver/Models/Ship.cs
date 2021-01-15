using System;

namespace AdventOfCode2020.Solver.Models
{
    internal enum Direction
    {
        North,
        East,
        South,
        West
    }

    internal class Ship
    {
        public (int X, int Y) Position { get; private set; }

        Direction _direction = Direction.East;

        public Ship()
        {
            this.Position = (0, 0);
        }

        public int ManhattanDistance { get { return Math.Abs(Position.X) + Math.Abs(Position.Y); } }

        internal void Turn(int degrees)
        {
            var places = degrees / 90;

            _direction = (Direction)(((int)_direction + places + 4) % 4);
        }

        internal void Move(Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.North:
                    Position = (Position.X, Position.Y + value);
                    break;
                case Direction.South:
                    Position = (Position.X, Position.Y - value);
                    break;
                case Direction.East:
                    Position = (Position.X + value, Position.Y);
                    break;
                case Direction.West:
                    Position = (Position.X - value, Position.Y);
                    break;
                default:
                    break;
            }
        }

        internal void Action(string actionInput)
        {
            var action = actionInput[0].ToString();
            var value = int.Parse(actionInput.Substring(1));

            switch (action)
            {
                case "N":
                    Move(Direction.North, value);
                    break;
                case "S":
                    Move(Direction.South, value);
                    break;
                case "E":
                    Move(Direction.East, value);
                    break;
                case "W":
                    Move(Direction.West, value);
                    break;
                case "L":
                    Turn(-value);
                    break;
                case "R":
                    Turn(value);
                    break;
                case "F":
                    Move(_direction, value);
                    break;
                default:
                    break;
            }

            //Console.WriteLine(actionInput);
            //Console.WriteLine(Position);
            //Console.WriteLine(_direction);
            //Console.WriteLine();
        }
    }
}