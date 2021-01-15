using System;

namespace AdventOfCode2020.Solver.Models
{
    internal class WaypointShip
    {
        public (int, int) Position { get; private set; }
        public (int, int) WaypointPosition { get; private set; }

        public WaypointShip()
        {
            this.Position = (0, 0);
            this.WaypointPosition = (10, 1);
        }

        public int ManhattanDistance { get { return Math.Abs(Position.Item1) + Math.Abs(Position.Item2); } }

        internal void TurnWaypoint(int degrees)
        {
            var times = degrees / 90;

            for (int i = 0; i < Math.Abs(times); i++)
            {
                if (times > 0)
                {
                    WaypointPosition = (WaypointPosition.Item2, -WaypointPosition.Item1);
                }
                else
                {
                    WaypointPosition = (-WaypointPosition.Item2, WaypointPosition.Item1);
                }
            }

            //R 10, 2 => 2, -10 => -10, -2, => -2, 10
            //L 10, 2 => -2, 10 => -10, -2 => 2, -10

            //_direction = (Direction)(((int)_direction + places + 4) % 4);
        }

        internal void Move(int value)
        {
            this.Position = (this.Position.Item1 + this.WaypointPosition.Item1 * value, this.Position.Item2 + this.WaypointPosition.Item2 * value);
        }

        internal void MoveWaypoint(Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.North:
                    WaypointPosition = (WaypointPosition.Item1, WaypointPosition.Item2 + value);
                    break;
                case Direction.South:
                    WaypointPosition = (WaypointPosition.Item1, WaypointPosition.Item2 - value);
                    break;
                case Direction.East:
                    WaypointPosition = (WaypointPosition.Item1 + value, WaypointPosition.Item2);
                    break;
                case Direction.West:
                    WaypointPosition = (WaypointPosition.Item1 - value, WaypointPosition.Item2);
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
                    MoveWaypoint(Direction.North, value);
                    break;
                case "S":
                    MoveWaypoint(Direction.South, value);
                    break;
                case "E":
                    MoveWaypoint(Direction.East, value);
                    break;
                case "W":
                    MoveWaypoint(Direction.West, value);
                    break;
                case "L":
                    TurnWaypoint(-value);
                    break;
                case "R":
                    TurnWaypoint(value);
                    break;
                case "F":
                    Move(value);
                    break;
                default:
                    break;
            }

            //Console.WriteLine(actionInput);
            //Console.WriteLine(Position);
            //Console.WriteLine(WaypointPosition);
            //Console.WriteLine();
        }
    }
}