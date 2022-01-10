using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day10 : DayBase<long>
    {
        public override int Day => 10;

        
        public override long First()
        {
            var lines = GetInputLines();
            var score = 0;
            var points = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            var closes = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

            bool opening(char symbol) => symbol is '(' or '[' or '<' or '{';

            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                foreach (var symbol in line)
                {
                    if (opening(symbol))
                    {
                        stack.Push(symbol);
                    }
                    else
                    {
                        if (symbol == closes[stack.Pop()])
                        {
                            continue;
                        }
                        else
                        {
                            score += points[symbol];
                            break;
                        }
                    }
                }
            }

            return score;

        }

        public override long Second()
        {
            var lines = GetInputLines();
            var scores = new List<long>();
            var points = new Dictionary<char, int>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
            var closes = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

            bool opening(char symbol) => symbol is '(' or '[' or '<' or '{';

            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                var incorrect = false;
                foreach (var symbol in line)
                {
                    if (opening(symbol))
                    {
                        stack.Push(symbol);
                    }
                    else
                    {
                        if (symbol == closes[stack.Pop()])
                        {
                            continue;
                        }
                        else
                        {
                            incorrect = true;
                            break;
                        }
                    }
                }
                if (!incorrect)
                {
                    long score = 0;
                    while (stack.Any())
                    {
                        score = score * 5 + points[closes[stack.Pop()]];
                    }
                    scores.Add(score);
                }
            }

            return scores.OrderBy(a => a).ElementAt(scores.Count / 2);
        }
    }
}
