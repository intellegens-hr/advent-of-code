namespace AoC2021.Solutions
{
    public class Day01: DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            int[] inputArray = ParseInput(input);

            int count = 0;
            for (var i = 1; i < inputArray.Length; i++)
                if (inputArray[i - 1] < inputArray[i])
                    count++;

            return count;
        }

        public override int CalculatePart2(string input)
        {
            var inputList = ParseInput(input);
            var endIndex = inputList.Length - 2;

            int? previousWindow = null;

            int count = 0;
            for (var i = 0; i < endIndex; i++) { 
                var sum = inputList[i] + inputList[i+1] + inputList[i+2];
                if (sum > previousWindow)
                    count++;

                previousWindow = sum;
            }

            return count;
        }

        private static int[] ParseInput(string input)
        {
            return input.Split("\r\n")
                .Select(x => int.Parse(x))
                .ToArray();
        }
    }
}