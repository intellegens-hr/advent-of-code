using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle14
    {
        public static long Task1(List<InputData> input)
        {
            var arrayList = new List<(int memoryAddress, bool[] array)>();

            foreach (var inp in input)
            {
                foreach (var (position, number) in inp.Steps)
                {
                    if (!arrayList.Where(x => x.memoryAddress == position).Any())
                        arrayList.Add((position, new bool[36]));

                    var array = arrayList.First(x => x.memoryAddress == position).array;

                    bool[] bin = Convert.ToString(number, 2).Select(x => x == '1').Reverse().ToArray();

                    for (var i = inp.Mask.Length - 1; i >= 0; i--)
                    {
                        array[i] = inp.Mask[i] ?? (i < bin.Length && bin[i]);
                    }
                }
            }

            return arrayList.Select(x => x.array.Select((y, i) => y ? (long)Math.Pow(2, i) : 0).Sum()).Sum();
        }

        public static long Task2(List<InputData> input)
        {
            var mems = input.SelectMany(x => x.Steps.Select(y => y.position)).Distinct();
            var values = new Dictionary<long, long>();

            foreach (var inp in input)
            {
                foreach (var (position, number) in inp.Steps)
                {
                    bool?[] memoryAddressBinary = Convert.ToString(position, 2).PadLeft(36, '0')
                        .Select(x => (bool?)(x == '1'))
                        .Reverse()
                        .ToArray();

                    for (var i = inp.Mask.Length - 1; i >= 0; i--)
                    {
                        memoryAddressBinary[i] = inp.Mask[i] == true
                            ? true
                            : inp.Mask[i] == false
                            ? memoryAddressBinary[i]
                            : null;
                    }

                    var xxx = new List<bool?[]> { memoryAddressBinary };
                    while (xxx.Where(y => y.Any(x => x == null)).Any())
                    {
                        var (array, index) = xxx.Select((array, index) => (array, index))
                           .Where(y => y.array.Any(bit => bit == null))
                           .First();

                        xxx.RemoveAt(index);

                        var floatingPointIndex = array
                            .Select((bit, index) => (bit, index))
                            .Where(x => x.bit == null)
                            .Select(x => x.index)
                            .First();

                        xxx.Add(array.Take(floatingPointIndex).Append(true).Concat(array.Skip(floatingPointIndex + 1)).ToArray());
                        xxx.Add(array.Take(floatingPointIndex).Append(false).Concat(array.Skip(floatingPointIndex + 1)).ToArray());
                    }

                    foreach (var address in xxx)
                    {
                        var newAddress = address
                            .Select((x, i) => (x, i)).Where(x => x.x == true).Select(y => (long)Math.Pow(2, y.i))
                            .Sum();
                        values[newAddress] = number;
                    }
                }
            }

            return values.Select(x => x.Value).Sum();
        }

        public static List<InputData> ToPuzzle14Input(this string input)
        {
            var returnData = new List<InputData>();

            var split = input.Split("\r\n");
            foreach (var row in split)
            {
                if (row.StartsWith("mask"))
                {
                    returnData.Add(new InputData
                    {
                        Mask = row
                        .Replace("mask = ", "")
                        .Select(x => x == '1' ? true : x == '0' ? false : null as bool?)
                        .Reverse()
                        .ToArray()
                    });
                }
                else
                {
                    var data = row
                        .Replace("mem", "")
                        .Replace("[", string.Empty)
                        .Replace("]", string.Empty)
                        .Split(" = ")
                        .Select(x => Convert.ToInt32(x))
                        .ToArray();

                    returnData.Last().Steps.Add((data[0], data[1]));
                }
            }

            return returnData;
        }

        public class InputData
        {
            public bool?[] Mask { get; set; }
            public List<(int position, int number)> Steps { get; set; } = new();
        }
    }
}