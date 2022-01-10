namespace AoC2021.Solutions
{
    public class Day02: DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            IEnumerable<(int command, int steps)> inputArray = ParseInput(input);

            int horizontal = 0;
            int vertical = 0;
            foreach (var (command, steps) in inputArray)
            {
                switch (command)
                {
                    case 3:
                        horizontal += steps;
                        break;
                    case 2:
                        vertical += steps;
                        break;
                    case 1:
                        vertical -= steps;
                        break;
                }
            }

            return horizontal * vertical;
        }

        public override int CalculatePart2(string input)
        {
           IEnumerable<(int command, int steps)> inputArray = ParseInput(input);

            int horizontal = 0;
            int vertical = 0;
            int aim = 0;
            foreach (var (command, steps) in inputArray)
            {
                switch (command)
                {
                    case 3:
                        horizontal += steps;
                        vertical += aim * steps;
                        break;
                    case 2:
                        aim += steps;
                        break;
                    case 1:
                        aim -= steps;
                        break;
                }
            }

            return horizontal * vertical;
        }

        private static IEnumerable<(int command, int steps)> ParseInput(string input)
        {
            return input.Split("\r\n")
                .Select(x => x.Split(" "))
                .Select(x => ((int command, int steps))(x[0][0] == 'u' ? 1 : x[0][0] == 'd' ? 2 : 3, int.Parse(x[1])));
        }
    }
}