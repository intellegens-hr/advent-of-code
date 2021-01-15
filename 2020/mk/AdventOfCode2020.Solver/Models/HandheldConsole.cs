using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Models
{
    public enum Operation
    {
        acc,
        jmp,
        nop
    }

    public class HandheldConsole
    {
        public int Accumlator { get; private set; }
        public int Index { get; private set; }

        public List<(Operation, int)> Instructions { get; set; }
        public  Dictionary<int,int> InstructionsVisited { get; set; }

        public HandheldConsole(string[] instructions)
        {
            this.Instructions = instructions.Select(i => (Enum.Parse<Operation>(i.Split(' ')[0]), Int32.Parse(i.Split(' ')[1]))).ToList();
        }

        public void Run()
        {
            this.Accumlator = 0;
            this.Index = 0;
            this.InstructionsVisited = Enumerable.Range(0, Instructions.Count).ToDictionary(a => a, a => 0);

            while (Index < Instructions.Count && InstructionsVisited[Index] < 1)
            {
                InstructionsVisited[Index]++;
                Index = Execute(Index, Instructions[Index]);
            }
        }

        int Execute(int currentIndex, (Operation, int) p)
        {
            switch (p.Item1)
            {
                case Operation.acc:
                    Accumlator += p.Item2;
                    currentIndex += 1;
                    break;
                case Operation.jmp:
                    currentIndex += p.Item2;
                    break;
                case Operation.nop:
                    currentIndex += 1;
                    break;
                default:
                    break;
            }

            return currentIndex;
        }
    }
}
