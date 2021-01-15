using AdventOfCode2020.Solver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Days
{
    public class Day8 : PuzzleDay<int>
    {
        public override int Day => 8;

        public override int First()
        {
            var input = GetInputLines();
            var gameConsole = new HandheldConsole(input);

            gameConsole.Run();
            return gameConsole.Accumlator;
        }

        public override int Second()
        {
            var input = GetInputLines();
            var gameConsole = new HandheldConsole(input);

            for (int i = 0; i < gameConsole.Instructions.Count; i++)
            {
                // Switch
                if (gameConsole.Instructions[i].Item1 == Operation.jmp)
                {
                    gameConsole.Instructions[i] = (Operation.nop, gameConsole.Instructions[i].Item2);
                }
                else if (gameConsole.Instructions[i].Item1 == Operation.nop)
                {
                    gameConsole.Instructions[i] = (Operation.jmp, gameConsole.Instructions[i].Item2);
                }
                else
                {
                    continue;
                }

                // Run
                gameConsole.Run();
                if (gameConsole.Index == gameConsole.Instructions.Count)
                {
                    return gameConsole.Accumlator;
                }

                // Switch back
                if (gameConsole.Instructions[i].Item1 == Operation.jmp)
                {
                    gameConsole.Instructions[i] = (Operation.nop, gameConsole.Instructions[i].Item2);
                }
                else if (gameConsole.Instructions[i].Item1 == Operation.nop)
                {
                    gameConsole.Instructions[i] = (Operation.jmp, gameConsole.Instructions[i].Item2);
                }
            }

            return -1;

        }
    }
}
