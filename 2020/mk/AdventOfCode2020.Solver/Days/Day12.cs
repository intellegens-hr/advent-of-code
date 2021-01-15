using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day12 : PuzzleDay<int>
    {
        public override int Day => 12;

        public override int First()
        {
            var actions = GetInputLines();
            var ship = new Ship();

            foreach (var action in actions)
            {
                ship.Action(action);
            }

            return ship.ManhattanDistance;
        }

        public override int Second()
        {
            var actions = GetInputLines();
            var ship = new WaypointShip();

            foreach (var action in actions)
            {
                ship.Action(action);
            }

            return ship.ManhattanDistance;
        }
    }
}
