using Microsoft.Win32;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static AdventOfCode2022.Days.Day9;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days;

public class Day13 : Puzzle<int>
{
    public override int Day => 13;

    public override int First()
    {
        var lines = GetInputLines();
        var pairs = new List<(PacketList, PacketList)>();

        for (int i = 0; i < lines.Length; i += 3)
        {
            var pair1 = Parse(lines[i]);
            var pair2 = Parse(lines[i + 1]);

            if (pair1.ToString() != lines[i])
            {
                throw new Exception();
            }
            if (pair2.ToString() != lines[i + 1])
            {
                throw new Exception();
            }

            pairs.Add((pair1, pair2));
        }

        var sum = 0;
        for (int i = 0; i < pairs.Count; i++)
        {
            (PacketList, PacketList) pair = pairs[i];
            var rightOrder = Compare(pair.Item1, pair.Item2);

            if (rightOrder == true)
                sum += (i + 1);
        }


        return sum;
    }

    private bool? Compare(IPacketItem left, IPacketItem right)
    {
        if (left is PacketList leftList && right is PacketList rightList)
        {
            for (int i = 0; i < Math.Max(leftList.Count, rightList.Count); i++)
            {
                if (i == leftList.Count)
                {
                    return true;
                }
                else if (i == rightList.Count)
                {
                    return false;
                }
                else
                {
                    var result = Compare(leftList[i], rightList[i]);
                    if (result.HasValue)
                        return result.Value;
                }
            }
        }
        else if (left is PacketInteger leftInt && right is PacketInteger rightInt)
        {
            if (leftInt.Value == rightInt.Value)
                return null;

            return leftInt.Value < rightInt.Value;
        }
        else
        {
            if (left is PacketInteger leftInt1 && right is PacketList rightList1)
            {
                return Compare(new PacketList { leftInt1 }, rightList1);
            }
            else if (left is PacketList leftList1 && right is PacketInteger rightInt1)
            {
                return Compare(leftList1, new PacketList { rightInt1 });
            }
        }

        return null;
    }

    private static PacketList Parse(string v)
    {
        var lastPop = new PacketList();
        var stack = new Stack<PacketList>();

        var current = "";
        for (int i = 0; i < v.Length; i++)
        {
            char token = v[i];

            if (token == '[')
            {
                var newList = new PacketList();

                if (stack.Any())
                    stack.Peek().Add(newList);

                stack.Push(newList);
            }
            else if (token == ']')
            {
                if (current != "")
                {
                    stack.Peek().Add(new PacketInteger { Value = Int32.Parse(current) });
                    current = "";
                }
                lastPop = stack.Pop() as PacketList;
            }
            else if (token == ',')
            {
                if (current != "")
                {
                    stack.Peek().Add(new PacketInteger { Value = Int32.Parse(current) });
                    current = "";
                }
            }
            else
            {
                current += token;
            }
        }
        return lastPop;
    }

    public override int Second()
    {
        var lines = GetInputLines();
        var packets = new List<PacketList>();
        var divider1 = new PacketList { new PacketList { new PacketInteger { Value = 2 } } };
        var divider2 = new PacketList { new PacketList { new PacketInteger { Value = 6 } } };

        packets.Add(divider1);
        packets.Add(divider2);

        for (int i = 0; i < lines.Length; i += 3)
        {
            var pair1 = Parse(lines[i]);
            var pair2 = Parse(lines[i + 1]);

            packets.Add(pair1);
            packets.Add(pair2);
        }


        packets.Sort((a, b) =>
        {
            var compare = Compare(a, b);
            if (compare.HasValue)
            {
                return compare.Value ? -1 : 1;
            }
            else
            {
                return 0;
            }
        });


        return (packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1);
    }

}


interface IPacketItem
{
}

class PacketInteger : IPacketItem
{
    public int Value { get; set; } = 0;

    public override string ToString() => Value.ToString();
}

class PacketList : List<IPacketItem>, IPacketItem
{
    public override string ToString() => $"[{string.Join(",", this.Select(a => a.ToString()))}]";
}