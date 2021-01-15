using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public enum Puzzle3MapElements
    {
        Tree,
        Square
    }

    public static class Puzzle03
    {
        public static int CountTreesEncountered(IEnumerable<IEnumerable<Puzzle3MapElements>> map, (int right, int down) step)
        => map
            .Where((x, i) => i % step.down == 0)
            .Select((x, i) => x.ElementAt((i * step.right) % x.Count()))
            .Where(x => x == Puzzle3MapElements.Tree)
            .Count();

        public static IEnumerable<int> CountTreesEncountered(IEnumerable<IEnumerable<Puzzle3MapElements>> map, IEnumerable<(int right, int down)> steps)
        => steps.Select(step => CountTreesEncountered(map, step));

        public static long CountTreesEncounteredMultiplyByRows(IEnumerable<IEnumerable<Puzzle3MapElements>> map, IEnumerable<(int right, int down)> steps)
        => CountTreesEncountered(map, steps)
            .Select(x => (long)x)
            .Aggregate((x, y) => x * y);

        public static IEnumerable<IEnumerable<Puzzle3MapElements>> ToPuzzle3Input(this string input)
        => input
            .Trim()
            .Split("\r\n")
            .Select(x => x.ToCharArray().Select(x => x == '#' ? Puzzle3MapElements.Tree : Puzzle3MapElements.Square));
    }
}