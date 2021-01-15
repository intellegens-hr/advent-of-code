using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day15 : PuzzleDay<int>
    {
        public override int Day => 15;

        Dictionary<int, int> lastTimeSpoken = new Dictionary<int, int>();
        Dictionary<int, int> firstTimeSpoken = new Dictionary<int, int>();

        public override int First()
        {
            var startingNumbers = GetInputText().Split(',').Select(a => int.Parse(a)).ToList();

            var i = 0;
            for (i = 0; i < startingNumbers.Count - 1; i++)
            {
                Speak(startingNumbers[i], i);
            }

            int next = Speak(startingNumbers[startingNumbers.Count - 1], i);

            while (i < 2020 - 2)
            {
                i++;
                next = Speak(next, i);
            }

            return next;
        }


        public override int Second()
        {
            var startingNumbers = GetInputText().Split(',').Select(a => int.Parse(a)).ToList();

            var i = 0;
            for (i = 0; i < startingNumbers.Count - 1; i++)
            {
                Speak(startingNumbers[i], i);
            }

            int next = Speak(startingNumbers[startingNumbers.Count - 1], i);

            while (i < 30000000 - 2)
            {
                i++;
                next = Speak(next, i);
            }

            return next;
        }

        int Speak(int v, int index)
        {
            int next;
            if (!firstTimeSpoken.ContainsKey(v))
            {
                next = 0;
            }
            else
            {
                next = index - lastTimeSpoken[v];
            }

            if (!firstTimeSpoken.ContainsKey(v))
                firstTimeSpoken[v] = index;

            lastTimeSpoken[v] = index;


            return next;
        }

    }
}
