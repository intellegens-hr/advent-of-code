using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task14
{
    public class Task14
    {
        public static long FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            Dictionary<long, long> memory = new Dictionary<long, long>();
            string currentMask = string.Empty;

            List<long> addresses = new List<long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    currentMask = line.Substring("mask = ".Length);

                }
                else
                {
                    var parts = line.Split("] = ");
                    long value = Convert.ToInt64(parts[1]);

                    // Task 1:
                    // addresses = new List<long>() { Convert.ToInt64(parts[0].Substring(4)) };

                    // Task 2:
                    addresses = GetPossibleAddresses(Convert.ToInt64(parts[0].Substring(4)), currentMask);

                    foreach (var address in addresses)
                    {
                        // Task 1
                        // var newValue = MaskValue(value, currentMask);

                        // Task 2
                        var newValue = value;

                        if (!memory.ContainsKey(address))
                        {
                            memory.Add(address, newValue);
                        }
                        else
                        {
                            memory[address] = newValue;
                        }

                        Console.WriteLine("Writing " + newValue + " to memory address " + address);
                    }
                }
            }

            long sum = 0;
            foreach (var item in memory)
            {
                sum += item.Value;
            }

            return sum;
        }

        private static List<long> GetPossibleAddresses(long address, string currentMask)
        {
            var numberOfZerosToPad = currentMask.Length - Convert.ToString(address, 2).ToCharArray().Length;
            var addressArray = Convert.ToString(address, 2).ToCharArray();
            char[] initialValue = new char[36];
            for (int k = 0; k < 36; k++)
            {
                if (k < numberOfZerosToPad) initialValue[k] = '0';
                else initialValue[k] = addressArray[k - numberOfZerosToPad];
            }
            List<char[]> addresses = new List<char[]>()
            {
                initialValue
            };

            var mask = currentMask.ToCharArray();
            int i = 0;

            string outputString = string.Empty;

            while (i < mask.Length)
            {
                if (mask[mask.Length - i - 1] == '1')
                {
                    // In all combinations substitute given bit with this char!
                    for (int j = 0; j < addresses.Count; j++)
                    {
                        addresses[j][addresses[j].Length - i - 1] = '1';
                    }
                }
                else if (mask[mask.Length - i - 1] == 'X')
                {
                    // X - add both combinations to new list.
                    // In all combinations substitute given bit with 0 and add a combination with 1!
                    for (int j = addresses.Count - 1; j >= 0; j--)
                    {
                        char[] addressToAdd = (char[])addresses[j].Clone();
                        addressToAdd[addressToAdd.Length - i - 1] = '1';
                        addresses.Add(addressToAdd);
                        addresses[j][addresses[j].Length - i - 1] = '0';
                    }
                }

                i++;
            }

            // Convert values back to list of long
            List<long> toReturn = addresses.Select(a => Convert.ToInt64(new string(a), 2)).ToList();
            return toReturn;
        }

        private static long MaskValue(long value, string currentMask)
        {
            var binaryValue = Convert.ToString(value, 2).ToCharArray();
            var mask = currentMask.ToCharArray();

            int i = 0;
            long returnValue = 0;

            string outputString = string.Empty;

            while (i < mask.Length)
            {
                char myBit = '0';
                if (mask[mask.Length - i - 1] != 'X')
                {
                    myBit = mask[mask.Length - i - 1];
                }
                else if (i < binaryValue.Length)
                {
                    myBit = binaryValue[binaryValue.Length - i - 1];
                }

                outputString = myBit.ToString() + outputString;
                returnValue += Convert.ToInt64(Math.Pow(2, i)) * Convert.ToInt32(myBit.ToString());
                i++;
            }

            //long returnValue = Convert.ToInt64(new string(binaryValue), 2);
            return returnValue;
        }
    }
}
