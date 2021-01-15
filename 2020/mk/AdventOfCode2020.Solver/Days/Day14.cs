using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Solver.Models;

namespace AdventOfCode2020.Solver.Days
{
    public class Day14 : PuzzleDay<long>
    {
        public override int Day => 14;

        public override long First()
        {
            var lines = GetInputLines();
            var memory = new Dictionary<long, long>();
            var mask = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(" = ")[1];
                }
                else if (line.StartsWith("mem"))
                {
                    var location = int.Parse(line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    var value = int.Parse(line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries)[1]);

                    memory[location] = ApplyMask(mask, value);
                }
            }

            return memory.Sum(a => a.Value);
        }

        long ApplyMask(string mask, int value)
        {
            var valueBinary = Convert.ToString(value, 2).PadLeft(36, '0');
            var maskedValueBinary = "";

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    maskedValueBinary += valueBinary[i];
                }
                else
                {
                    maskedValueBinary += mask[i];
                }
            }

            return Convert.ToInt64(maskedValueBinary, 2);
        }

        long[] ApplyMemMask(string mask, int value)
        {
            var valueBinary = Convert.ToString(value, 2).PadLeft(36, '0');
            var maskedValueBinary = "";

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    maskedValueBinary += 'X';
                else if(mask[i] == '1')
                    maskedValueBinary += '1';
                else
                    maskedValueBinary += valueBinary[i];
            }

            return GetPermutations(maskedValueBinary).ToArray();
        }

        List<long> GetPermutations(string maskedValueBinary)
        {
            var firstX = maskedValueBinary.IndexOf('X');
            var sb = new StringBuilder(maskedValueBinary);

            var lists = new List<long>();

            if (firstX > -1)
            {
                sb[firstX] = '1';
                lists.AddRange(GetPermutations(sb.ToString()));
                sb[firstX] = '0';
                lists.AddRange(GetPermutations(sb.ToString()));
            }
            else
            {
                lists.Add(Convert.ToInt64(maskedValueBinary, 2));
            }

            return lists;
        }

        public override long Second()
        {
            var lines = GetInputLines();
            var memory = new Dictionary<long, long>();
            var mask = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(" = ")[1];
                }
                else if (line.StartsWith("mem"))
                {
                    var location = int.Parse(line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    var value = int.Parse(line.Split(new[] { "mem[", "] = " }, StringSplitOptions.RemoveEmptyEntries)[1]);

                    var floatingLocations = ApplyMemMask(mask, location);
                    foreach (var floatingLocation in floatingLocations)
                    {
                        memory[floatingLocation] = value;
                    }
                }
            }

            return memory.Sum(a => a.Value);
        }
    }
}
