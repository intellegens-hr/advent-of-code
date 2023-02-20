using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._05
{
    public class Task_05
    {
        public static string PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\05\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\04\Example.txt");
            string line = String.Empty;
            List<string> stacks = new();
            bool readingStacks = true;

            List<Stack<char>> myStacks = new();


            int s = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (readingStacks)
                {
                    stacks.Add(line);

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // End of reading stacks - parse stacks
                        readingStacks = false;

                        // Remove last 2 lines
                        stacks.RemoveRange(stacks.Count - 2, 2);

                        // Remove shit
                        for (int i = 0; i < stacks.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int j = 0; j < stacks[i].Length; j += 4)
                            {
                                var part = stacks[i].Length > j + 4 ? stacks[i].Substring(j, 4) : stacks[i].Substring(j);
                                if (string.IsNullOrWhiteSpace(part))
                                {
                                    part = " ";
                                }
                                else
                                {
                                    part = part.Substring(1, 1);
                                }
                                sb.Append(part);
                            }
                            stacks[i] = sb.ToString();
                        }

                        for (int i = 0; i < stacks[0].Length; i++)
                        {
                            myStacks.Add(new Stack<char>());
                        }

                        for (int i = 0; i < stacks[stacks.Count - 1].Length; i++)
                        {
                            for (int j = stacks.Count - 1; j >= 0; j--)
                            {
                                if (stacks[j][i] != ' ') myStacks[i].Push(stacks[j][i]);
                            }
                        }
                    }
                }

                else
                {
                    // now we're reading other lines
                    line = line.Replace("move ", "").Replace(" from ", "-").Replace(" to ", "-");
                    var numbers = line.Split('-');

                    var n = Convert.ToInt32(numbers[0]);
                    var fromStack = Convert.ToInt32(numbers[1]);
                    var toStack = Convert.ToInt32(numbers[2]);

                    for (int i = 0; i < n; i++)
                    {
                        var myChar = myStacks[fromStack - 1].Pop();
                        myStacks[toStack - 1].Push(myChar);
                    }
                }

            }

            StringBuilder finalString = new StringBuilder();
            foreach(var stack in myStacks)
            {
                finalString.Append(stack.Pop());
            }

            return finalString.ToString();
        }

        public static string PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\05\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\04\Example.txt");
            string line = String.Empty;
            List<string> stacks = new();
            bool readingStacks = true;

            List<Stack<char>> myStacks = new();


            int s = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (readingStacks)
                {
                    stacks.Add(line);

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // End of reading stacks - parse stacks
                        readingStacks = false;

                        // Remove last 2 lines
                        stacks.RemoveRange(stacks.Count - 2, 2);

                        // Remove shit
                        for (int i = 0; i < stacks.Count; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int j = 0; j < stacks[i].Length; j += 4)
                            {
                                var part = stacks[i].Length > j + 4 ? stacks[i].Substring(j, 4) : stacks[i].Substring(j);
                                if (string.IsNullOrWhiteSpace(part))
                                {
                                    part = " ";
                                }
                                else
                                {
                                    part = part.Substring(1, 1);
                                }
                                sb.Append(part);
                            }
                            stacks[i] = sb.ToString();
                        }

                        for (int i = 0; i < stacks[0].Length; i++)
                        {
                            myStacks.Add(new Stack<char>());
                        }

                        for (int i = 0; i < stacks[stacks.Count - 1].Length; i++)
                        {
                            for (int j = stacks.Count - 1; j >= 0; j--)
                            {
                                if (stacks[j][i] != ' ') myStacks[i].Push(stacks[j][i]);
                            }
                        }
                    }
                }

                else
                {
                    // now we're reading other lines
                    line = line.Replace("move ", "").Replace(" from ", "-").Replace(" to ", "-");
                    var numbers = line.Split('-');

                    var n = Convert.ToInt32(numbers[0]);
                    var fromStack = Convert.ToInt32(numbers[1]);
                    var toStack = Convert.ToInt32(numbers[2]);

                    var tempStack = new Stack<char>();
                    for (int i = 0; i < n; i++)
                    {
                        var myChar = myStacks[fromStack - 1].Pop();
                        tempStack.Push(myChar);
                    }
                    for (int i = 0; i < n; i++)
                    {
                        var myChar = tempStack.Pop();
                        myStacks[toStack - 1].Push(myChar);
                    }
                }

            }

            StringBuilder finalString = new StringBuilder();
            foreach (var stack in myStacks)
            {
                finalString.Append(stack.Pop());
            }

            return finalString.ToString();
        }
    }
}
