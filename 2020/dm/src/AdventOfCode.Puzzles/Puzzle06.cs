using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle06
    {
        // Count distinct answers within a group.
        public static int CountTask1(IEnumerable<IEnumerable<IEnumerable<char>>> input)
        => input
            .Select(x => x.CountDistinctAnswersInGroup())
            .Sum();

        // Count answers which entire group answered.
        public static int CountTask2(IEnumerable<IEnumerable<IEnumerable<char>>> input)
        => input
            .Select(x => x.CountAnswersEntireGroupAnswered())
            .Sum();

        public static IEnumerable<IEnumerable<IEnumerable<char>>> ToPuzzle6Input(this string input)
        => input
            .Split("\r\n\r\n")
            .Select(x => x.Split("\r\n").Select(y => y.ToCharArray()));

        // Group by answers, take only answers with count equal to group size.
        private static int CountAnswersEntireGroupAnswered(this IEnumerable<IEnumerable<char>> groupAnswers)
        => groupAnswers
            .SelectMany(y => y)
            .GroupBy(y => y)
            .Select(y => new { y.Key, Cnt = y.Count() })
            .Where(y => y.Cnt == groupAnswers.Count())
            .Select(x => x.Key)
            .Count();

        // Select all answers in group, take distincts.
        private static int CountDistinctAnswersInGroup(this IEnumerable<IEnumerable<char>> groupAnswers)
        => groupAnswers
            .SelectMany(y => y)
            .Distinct()
            .Count();
    }
}