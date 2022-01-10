namespace AoC2021.Solutions
{
    public class Day03: DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            var inputArray = ParseInput(input);

            var digits = GetSignificantDigits(inputArray.ToArray(), Modes.Most);

            var gamaRate = IntFromBinary(digits);
            var epsilonRate = IntFromBinary(digits.Select(x => !x));

            return gamaRate * epsilonRate;
        }

        private static int IntFromBinary(IEnumerable<bool> digits)
        {
            return Convert.ToInt32(string.Join("", digits.Select(x => x ? 1 : 0)), 2);
        }

        private enum Modes { Least, Most }
        private static IEnumerable<bool> GetSignificantDigits(IList<bool[]> inputArray, Modes mode, int? column = null)
        {
            Func<int, double, bool> modeFunction = mode == Modes.Least
                ? (sum, threshold) => !(sum >= threshold)
                : (sum, threshold) => sum >= threshold;

            var rows = inputArray.Count;
            var cols = inputArray[0].Length;

            var threshold = rows / 2.0;

            var columnFrom = column ?? 0;
            var columnTo = column == null ? cols : column + 1;

            for (var i = columnFrom; i < columnTo; i++)
            {
                var sum = inputArray.Count(x => x[i]);
                yield return modeFunction(sum, threshold);
            }
        }

        public override int CalculatePart2(string input)
        {
            var inputArray = ParseInput(input);

            var inputListOxygen = inputArray.ToList();
            var inputListCo2 = inputArray.ToList();

            var oxygenIndex = 0;
            while (inputListOxygen.Count > 1)
            {
                var significantDigitOxygen = GetSignificantDigits(inputListOxygen, Modes.Most, oxygenIndex).First();
                inputListOxygen.RemoveAll(x => x[oxygenIndex] == significantDigitOxygen);
                oxygenIndex++;
            }

            var co2Index = 0;
            while (inputListCo2.Count > 1)
            {
                var significantDigitCo2 = GetSignificantDigits(inputListCo2, Modes.Least, co2Index).First();
                inputListCo2.RemoveAll(x => x[co2Index] == significantDigitCo2);
                co2Index++;
            }

            var oxygenRate = IntFromBinary(inputListOxygen[0]);
            var co2Rate = IntFromBinary(inputListCo2[0]);

            return oxygenRate * co2Rate;
        }

        private static IEnumerable<bool[]> ParseInput(string input)
        {
            return input
                .Split("\r\n")
                .Select(x => x.Select(y => y == '1').ToArray());
        }
    }
}