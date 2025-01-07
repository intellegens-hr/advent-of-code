using System.Runtime.InteropServices;

namespace AdventOfCode2024.Days;

public class Day17 : Puzzle<string>
{
    public class Computer
    {
        public long A { get; set; }
        public long B { get; set; }
        public long C { get; set; }

        private long _instructionPointer = 0;

        public void Adv(long operand) => A = (long)(A / Math.Pow(2, Combo(operand)));
        public void Bxl(long operand) => B = BitwiseXor(B, Literal(operand));
        public void Bst(long operand) => B = Combo(operand) % 8;
        public void Jnz(long operand) => _instructionPointer = A == 0 ? _instructionPointer : Literal(operand) - 2;
        public void Bxc(long operand) => B = BitwiseXor(B, C);
        public long Out(long operand) => Combo(operand) % 8;
        public void Bdv(long operand) => B = (long)(A / Math.Pow(2, Combo(operand)));
        public void Cdv(long operand) => C = (long)(A / Math.Pow(2, Combo(operand)));

        private long Literal(long operand) => operand;

        private long BitwiseXor(long b, long operand) => b ^ operand;

        private long Combo(long operand) => operand switch
        {
            (>= 0) and (<= 3) => operand,
            4 => A,
            5 => B,
            6 => C,
            _ => throw new ArgumentException()
        };

        public IEnumerable<long> GetOutput(long[] input)
        {
            while (_instructionPointer < input.Length - 1)
            {
                var opcode = input[_instructionPointer];
                var operand = input[_instructionPointer + 1];

                switch (opcode)
                {
                    case 0:
                        Adv(operand);
                        break;
                    case 1:
                        Bxl(operand);
                        break;
                    case 2:
                        Bst(operand);
                        break;
                    case 3:
                        Jnz(operand);
                        break;
                    case 4:
                        Bxc(operand);
                        break;
                    case 5:
                        yield return Out(operand);
                        break;
                    case 6:
                        Bdv(operand);
                        break;
                    case 7:
                        Cdv(operand);
                        break;
                    default:
                        break;
                }

                _instructionPointer += 2;
            }
        }
    }

    public override string First(string input)
    {
        var map = input.ToLines();
        var computer = new Computer
        {
            A = long.Parse(map[0].Split(": ")[1]),
            B = long.Parse(map[1].Split(": ")[1]),
            C = long.Parse(map[2].Split(": ")[1]),
        };
        var instructions = map[4].Split(": ")[1].Split(",").Select(x => long.Parse(x)).ToArray();

        return string.Join(",", computer.GetOutput(instructions));
    }



    public override string Second(string input)
    {
        var map = input.ToLines();

        var b = long.Parse(map[1].Split(": ")[1]);
        var c = long.Parse(map[2].Split(": ")[1]);
        var program = map[4].Split(": ")[1];
        var instructions = program.Split(",").Select(x => long.Parse(x)).ToArray();

        var start = (long)Math.Pow(8, instructions.Length - 1);
        var end = (long)Math.Pow(8, instructions.Length);
        var increment = (long)Math.Pow(8, instructions.Length - 3);

        var tailCount = 2;
        var tail = instructions.TakeLast(tailCount);

        for (long a = start; a < end; a += increment)
        {
            var computer = new Computer
            {
                A = a,
                B = b,
                C = c,
            };

            var computerOutput = computer.GetOutput(instructions);

            if (instructions.SequenceEqual(computerOutput))
                return a.ToString();

            if (increment > 1 && computerOutput.TakeLast(tailCount).SequenceEqual(tail))
            {
                tailCount += 1;
                tail = instructions.TakeLast(tailCount);
                increment /= 8;
            }
        }

        return "-1";
    }
}

