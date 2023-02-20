using Microsoft.Win32;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day10 : Puzzle<int>
{
    public override int Day => 10;

    public override int First()
    {
        var lines = GetInputLines();
        var registerByCycles = new List<int> { 1 };

        foreach (var line in lines)
        {
            if (line == "noop")
            {
                registerByCycles.Add(registerByCycles.Last());
            }
            else
            {
                var inc = int.Parse(line.Split(" ")[1]);
                registerByCycles.Add(registerByCycles.Last());
                registerByCycles.Add(registerByCycles.Last() + inc);
            }
        }

        var sum = 0;

        for (int i = 20; i <= 220; i += 40)
        {
            sum += (i * registerByCycles[i - 1]);
        }
        return sum;
    }

    public override int Second()
    {
        var lines = GetInputLines();
        var pixels = "";
        var screen = "";
        var i = 0;
        var register = 1;

        foreach (var line in lines)
        {
            if (line == "noop")
            {
                Cycle(ref pixels, ref screen, ref i, register);
            }
            else
            {
                var inc = int.Parse(line.Split(" ")[1]);

                Cycle(ref pixels, ref screen, ref i, register);
                Cycle(ref pixels, ref screen, ref i, register);

                register += inc;
            }
        }

        for (int m = 0; m < 6; m++)
        {
            for (int n = 0; n < 40; n++)
            {
                Console.Write(screen[m * 40 + n]);
            }
            Console.WriteLine();
        }

        return 0;
    }

    private void Cycle(ref string pixels, ref string screen, ref int i, int register)
    {
        pixels = DrawPixel(pixels, i, register);
        i++;
        if (i == 40)
        {
            i = 0;
            screen += pixels;
            pixels = "";
        }
    }

    private string DrawPixel(string pixels, int i, int register)
    {
        if (i >= register - 1 && i <= register + 1)
        {
            return pixels + "#";
        }
        else
        {
            return pixels + ".";
        }
    }
}
