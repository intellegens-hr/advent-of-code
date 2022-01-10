using AoC2021.Solutions.Models.Day05;
using System.Text.RegularExpressions;

namespace AoC2021.Solutions
{
    public class Day05 : DayAbstract<int>
    {
        private static readonly Regex parseInputRegex = new("([0-9]*),([0-9]*) -> ([0-9]*),([0-9]*)", RegexOptions.Compiled);

        public override int CalculatePart1(string input)
        {
            return Calculate(input, true);
        }

        public override int CalculatePart2(string input)
        {
            return Calculate(input, false);
        }

        private int Calculate(string input, bool filterStraight = false)
        {
            var inputData = ParseInput(input);

            if (filterStraight)
                inputData = inputData.Where(x => x.IsStraight);

            var cols = inputData.Select(x => x.X2).Union(inputData.Select(y => y.X1)).Max() + 1;
            var rows = inputData.Select(x => x.Y2).Union(inputData.Select(y => y.Y1)).Max() + 1;

            var grid = new Grid(rows, cols);

            foreach (var line in inputData)
            {
                grid.AddLine(line);
            }

            return grid.CountFieldsWithAtLeastCount(2);
        }


        private static IEnumerable<Line> ParseInput(string input)
        {
            return input
                .Split("\r\n")
                .Select(x => parseInputRegex.Match(x).Groups)
                .Select(x => new Line(int.Parse(x[1].Value), int.Parse(x[2].Value), int.Parse(x[3].Value), int.Parse(x[4].Value)));
        }
    }
}