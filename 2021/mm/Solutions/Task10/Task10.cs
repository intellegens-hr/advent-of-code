using AdventOfCode2021.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions
{
    public static class Task10
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task10\\Input.txt");
            string line = string.Empty;

            var open = new char[] { '(', '{', '[', '<' };
            var close = new char[] { ')', '}', ']', '>' };

            int sum = 0;

            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    List<char> stack = new List<char>();
                    var chars = line.ToCharArray();
                    foreach (var c in chars)
                    {
                        // if new opening, just add to stack
                        if (open.Contains(c))
                        {
                            stack.Add(c);
                        }
                        else if (close.Contains(c))
                        {
                            var onStack = stack.Last();
                            if (Closes(onStack, c))
                            {
                                // closed, remove onstack
                                stack.RemoveAt(stack.Count() - 1);
                            } else
                            {
                                // ilegal character
                                sum += GetPoints(c);
                                break;
                            }
                        }
                        else
                        {
                            throw new System.Exception("Illegal character!");
                        }
                    }
                }
            }

            reader.Close();
            return sum;
        }

        public static long Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task10\\Input.txt");
            string line = string.Empty;

            var open = new char[] { '(', '{', '[', '<' };
            var close = new char[] { ')', '}', ']', '>' };

            List<long> sums = new List<long>();

            while (line != null)
            {
                long sum = 0;
                line = reader.ReadLine();
                if (line != null)
                {
                    List<char> stack = new List<char>();
                    var chars = line.ToCharArray();
                    foreach (var c in chars)
                    {
                        // if new opening, just add to stack
                        if (open.Contains(c))
                        {
                            stack.Add(c);
                        }
                        else if (close.Contains(c))
                        {
                            var onStack = stack.Last();
                            if (Closes(onStack, c))
                            {
                                // closed, remove onstack
                                stack.RemoveAt(stack.Count() - 1);
                            }
                            else
                            {
                                // ilegal character, clear stack, break;
                                stack = new List<char>();
                                break;
                            }
                        }
                        else
                        {
                            throw new System.Exception("Illegal character!");
                        }
                    }

                    while (stack.Count() != 0)
                    {
                        var last = stack.Last();
                        sum = sum * 5 + ClosePoints(CloseWith(last));
                        stack.RemoveAt(stack.Count() - 1);
                    }

                    if (sum != 0) sums.Add(sum);

                }
            }

            sums = sums.OrderBy(a => a).ToList();

            reader.Close();
            return sums[(sums.Count / 2)];
        }

        private static bool Closes(char openChar, char closeChar)
        {
            if (openChar == '(' && closeChar == ')') return true;
            if (openChar == '[' && closeChar == ']') return true;
            if (openChar == '<' && closeChar == '>') return true;
            if (openChar == '{' && closeChar == '}') return true;

            return false;
        }

        public static int GetPoints(char c)
        {
            if (c == ')') return 3;
            if (c == ']') return 57;
            if (c == '}') return 1197;
            if (c == '>') return 25137;

            throw new System.Exception("Cannot get sum for this char!");
        }

        public static char CloseWith(char open)
        {
            if (open == '(') return ')';
            if (open == '[') return ']';
            if (open == '<') return '>';
            if (open == '{') return '}';

            throw new System.Exception("Cannot close this char");
        }

        public static int ClosePoints(char closingChar)
        {
            if (closingChar == ')') return 1;
            if (closingChar == ']') return 2;
            if (closingChar == '}') return 3;
            if (closingChar == '>') return 4;

            throw new System.Exception("Can't calculate points for this char");
        }
    }
}
