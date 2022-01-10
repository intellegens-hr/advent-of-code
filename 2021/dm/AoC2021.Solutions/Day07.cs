namespace AoC2021.Solutions
{
    public class Day07 : DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            var crabs = ParseInput(input);
            var maxPosition = crabs.Max();
            var minFuel = int.MaxValue;

            for (var position = 0; position <= maxPosition; position++)
            {
                var fuelRequired = crabs.Select(x => Math.Abs(x - position)).Sum();
                if (fuelRequired < minFuel)
                    minFuel = fuelRequired;
            }

            return minFuel;
        }

        public override int CalculatePart2(string input)
        {
            var crabs = ParseInput(input);
            var maxPosition = crabs.Max();
            var minFuel = int.MaxValue;

            for (var position = 0; position <= maxPosition; position++)
            {
                var fuelRequired = crabs
                    .Select(x => Math.Abs(x - position))
                    .Select(x => x*(x + 1)/2)
                    .Sum();

                if (fuelRequired < minFuel)
                    minFuel = fuelRequired;
            }

            return minFuel;
        }
        private static IEnumerable<int> ParseInput(string input)
        {
            return input.Split(",").Select(x => int.Parse(x));
        }

    }
}