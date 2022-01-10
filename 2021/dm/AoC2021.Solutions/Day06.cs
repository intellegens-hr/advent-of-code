using AoC2021.Solutions.Models.Day06;

namespace AoC2021.Solutions
{
    public class Day06 : DayAbstract<long>
    {
        public long Calculate(string input, int days)
        {
            var data = ParseInput(input);
            var statePairs = new Dictionary<int, Fish>()
            {
                {0, new Fish(0)},
                {1, new Fish(1)},
                {2, new Fish(2)},
                {3, new Fish(3)},
                {4, new Fish(4)},
                {5, new Fish(5)},
                {6, new Fish(6)},
                {7, new Fish(7)},
                {8, new Fish(8)}
            };

            foreach (var fish in data)
            {
                statePairs[fish].Count++;
            }

            for (var i = 0; i < days; i++)
            {
                long newFish = 0;

                foreach (var pair in statePairs)
                    newFish += pair.Value.NextDay();

                var nextDayValues = statePairs.Values
                    .GroupBy(x => x.State)
                    .Select(g => new { Count = g.Select(x => x.Count).Sum(), State = g.Key })
                    .ToArray();

                foreach (var pair in statePairs)
                {
                    pair.Value.State = pair.Key;
                    pair.Value.Count = nextDayValues.FirstOrDefault(x => x.State == pair.Key)?.Count ?? 0;
                }

                statePairs[8].Count = newFish;
            }

            return statePairs.Select(x => x.Value.Count).Sum();
        }

        public override long CalculatePart1(string input)
        {
            return Calculate(input, 80);
        }

        public override long CalculatePart2(string input)
        {
            return Calculate(input, 256);
        }

        private static IEnumerable<int> ParseInput(string input)
        {
            return input.Split(",").Select(x => int.Parse(x));
        }
    }
}