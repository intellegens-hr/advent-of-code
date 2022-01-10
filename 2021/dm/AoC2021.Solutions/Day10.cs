using System.Text.RegularExpressions;

namespace AoC2021.Solutions
{
    public class Day10 : DayAbstract<long>
    {
        private readonly Regex regex = new ("(\\(\\))|(\\[\\])|(<>)|({})", RegexOptions.Compiled);
        private readonly Dictionary<char, char> bracketsMap = new()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
            { '<', '>' }
        };

        private string CleanupInput(string input)
        {
            while (regex.IsMatch(input))
                input = regex.Replace(input, "");

            return input;
        }

        public override long CalculatePart1(string input)
        {
            input = CleanupInput(input);

            var lines = input.Split("\r\n");
            var points = 0;

            foreach (var line in lines)
            {
                // If there are no invalid brackets - skip line
                var ignore = !line.Where(x => bracketsMap.ContainsValue(x)).Any();
                if (ignore)
                    continue;

                var invalidBracket = line[line.IndexOfAny(bracketsMap.Values.ToArray())];
                points += invalidBracket == ')'
                    ? 3
                    : invalidBracket == ']'
                    ? 57
                    : invalidBracket == '}'
                    ? 1197
                    : 25137;
            }

            return points;
        }

        public override long CalculatePart2(string input)
        {
            input = CleanupInput(input);

            var lines = input.Split("\r\n");
            var scores = new List<long>();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                // If there are no invalid brackets - skip line
                var ignore = line.Where(x => bracketsMap.ContainsValue(x)).Any();
                if (ignore)
                    continue;

                long score = 0;

                while (line.Length > 0)
                {
                    var lastBracket = line.Last();
                    score = score * 5 + (lastBracket == '(' ? 1 : lastBracket == '[' ? 2 : lastBracket == '{' ? 3 : 4);
                    line = line.Substring(0, line.Length - 1);
                }

                scores.Add(score);
            }

            scores.Sort();
            return scores[scores.Count / 2];
        }
    }
}