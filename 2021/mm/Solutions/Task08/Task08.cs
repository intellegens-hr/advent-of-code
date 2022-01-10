using AdventOfCode2021.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task08
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task8\\Input.txt");
            string line = string.Empty;
            int count = 0;

            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var parts = line.Split(" | ");
                    var digits = parts[1].Split(" ");
                    for (int i = 0; i < digits.Length; i++)
                    {
                        if (digits[i].Length == 2 || digits[i].Length == 3 || digits[i].Length == 4 || digits[i].Length == 7) count++;
                    }
                }
            }

            reader.Close();
            return count;
        }

        public static long Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task8\\Input.txt");
            string line = string.Empty;
            long count = 0;

            while (line != null)
            {
                // Map all display items a certain signal can be
                Dictionary<char, char[]> map = new Dictionary<char, char[]>();
                map.Add('a', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('b', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('c', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('d', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('e', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('f', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
                map.Add('g', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });

                var signalsAssigned = new List<char>();

                line = reader.ReadLine();
                if (line != null)
                {
                    var parts = line.Split(" | ");
                    var digits = parts[1].Split(" ");
                    var signals = parts[0].Split(" ").ToList();

                    var one = signals.First(s => s.Length == 2);
                    var seven = signals.First(s => s.Length == 3);
                    var four = signals.First(s => s.Length == 4);
                    var eight = signals.First(s => s.Length == 7);

                    signals.Remove(one);
                    signals.Remove(seven);
                    signals.Remove(four);
                    signals.Remove(eight);

                    // Difference between one and seven gives us the only possible top and the two possible for ones.
                    var oneArray = one.ToCharArray();
                    var sevenArray = seven.ToCharArray();
                    char top = 'X';
                    for (int i = 0; i < sevenArray.Length; i++)
                    {
                        if (!oneArray.Contains(sevenArray[i]))
                        {
                            top = sevenArray[i];
                            break;
                        }
                    }

                    map['a'] = new char[1];
                    map['a'][0] = top;
                    signalsAssigned.Add(top);

                    map['c'] = oneArray;
                    map['f'] = oneArray;

                    // 9, 6, 0 have 6 leds on. But only 6 doesn't have both that 1 does.
                    // So, we can identify 6, and then we can identify digits for c and f correctly!
                    var sixLeds = signals.Where(s => s.Length == 6).ToList();
                    foreach (var sixLed in sixLeds)
                    {
                        var sixLedArray = sixLed.ToCharArray();
                        if (sixLedArray.Intersect(oneArray).Count() != 2)
                        {
                            // we found six!
                            var common = sixLedArray.Intersect(oneArray).First();
                            map['f'] = new char[1];
                            map['f'][0] = common;

                            map['c'] = new char[1];
                            map['c'][0] = oneArray[0] == common ? oneArray[1] : oneArray[0];

                            signalsAssigned.Add(map['f'][0]);
                            signalsAssigned.Add(map['c'][0]);

                        }
                        else
                        {
                            // We found a 9 or a 0
                            // If we intersect them with 4 - if number contains all leds that the 4 does - it is a 9
                            // Else it is a zero
                            // We then intersect them with 8 and get d and e values
                            if (sixLedArray.Intersect(four.ToCharArray()).Count() == 4)
                            {
                                // Full intersect, we have a 9
                                var remains = eight.ToCharArray().Except(sixLed).ToArray();
                                map['e'] = new char[1];
                                map['e'][0] = remains[0];
                                signalsAssigned.Add(remains[0]);
                            }
                            else
                            {
                                // Semi intersect, we have a 0
                                var remains = eight.ToCharArray().Except(sixLed).ToArray();
                                map['d'] = new char[1];
                                map['d'][0] = remains[0];
                                signalsAssigned.Add(remains[0]);
                            }
                        }
                    }

                    // We have a, c, d, f defined - based on 4 we can define b
                    map['b'] = four.ToCharArray().Except(signalsAssigned.ToArray()).ToArray();
                    signalsAssigned.Add(map['b'][0]);

                    // Now we have signals for a, b, c, d, e, f
                    // The remaining one is G
                    map['g'] = map['g'].Except(signalsAssigned.ToArray()).ToArray();



                    // Now we have the exact map
                    // Lets reverse it
                    Dictionary<char, char> reverseMap = new Dictionary<char, char>();
                    foreach (var item in map)
                    {
                        reverseMap.Add(item.Value[0], item.Key);
                    }

                    // For each number in part 2 get the digit value and add it to a string
                    string number = string.Empty;
                    foreach (var digit in  digits)
                    {
                        number += GetDigit(digit, reverseMap);
                    }

                    var numberValue = long.Parse(number);
                    count += numberValue;
                }
            }

            reader.Close();
            return count;
        }

        private static int GetDigit(string digit, Dictionary<char, char> reverseMap)
        {
            var digitArray = digit.ToCharArray();
            var translatedDigitArray = digitArray.Select(a => reverseMap[a]).OrderBy(a => a).ToArray();
            var translatedString = string.Join("", translatedDigitArray);

            switch(translatedString)
            {
                case "abcefg":
                    return 0;
                    break;
                case "cf":
                    return 1;
                    break;
                case "acdeg":
                    return 2;
                    break;
                case "acdfg":
                    return 3;
                    break;
                case "bcdf":
                    return 4;
                    break;
                case "abdfg":
                    return 5;
                    break;
                case "abdefg":
                    return 6;
                    break;
                case "acf": 
                    return 7; 
                    break;
                case "abcdefg":
                    return 8;
                    break;
                case "abcdfg":
                    return 9;
                    break;
                default:
                    throw new System.Exception("WTF?");
                    break;
            }
        }
    }
}
