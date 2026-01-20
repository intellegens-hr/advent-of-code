try
{
    var input = File.ReadLines("input.txt").ToList();
    var mathSimbols = input[input.Count - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var grid = input
        .Where(line => line.Any(char.IsDigit))
        .Select(line => line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToList())
        .ToList();
    int numberfOfItems = grid.Count;
    var results = new List<double>();
    var results2 = new List<double>();

    for (int col = 0; col < grid[0].Count; col++)
    {
        // PART 1
        double result = double.Parse(grid[0][col]);
        for (int row = 1; row < numberfOfItems; row++)
        {
            var currentNumber = double.Parse(grid[row][col]);
            var mathSimbol = mathSimbols[col];
            switch (mathSimbol)
            {
                case "+":
                    result += currentNumber;
                    break;
                case "-":
                    result -= currentNumber;
                    break;
                case "*":
                    result *= currentNumber;
                    break;
                case "/":
                    result /= currentNumber;
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported operator: {mathSimbol}");
            }
        }
        results.Add(result);
    }

    // PART 2 - Cephalopod math (right-to-left, columns form numbers top->bottom)
        var allLines = input;
        if (allLines.Count >= 1)
        {
            var operatorLineRaw = allLines[allLines.Count - 1];
            var digitLinesRaw = allLines.Take(allLines.Count - 1).ToList();

            if (digitLinesRaw.Count > 0)
            {
                int maxLen = Math.Max(operatorLineRaw.Length, digitLinesRaw.Max(l => l.Length));
                for (int i = 0; i < digitLinesRaw.Count; i++) digitLinesRaw[i] = digitLinesRaw[i].PadRight(maxLen);
                var operatorLine = operatorLineRaw.PadRight(maxLen);

                var currentNumbers = new List<double>();
                int lastNonEmptyColumn = -1;

                for (int c = maxLen - 1; c >= 0; c--)
                {
                    var colChars = digitLinesRaw.Select(r => r[c]).ToArray();
                    var digitChars = new string(colChars.Where(char.IsDigit).ToArray());

                    if (!string.IsNullOrEmpty(digitChars))
                    {
                        currentNumbers.Add(double.Parse(digitChars));
                        lastNonEmptyColumn = c;
                        continue;
                    }

                    if (currentNumbers.Count > 0)
                    {
                        if (lastNonEmptyColumn < 0) throw new InvalidOperationException("Cannot determine operator for cephalopod problem.");
                        var opChar = operatorLine[lastNonEmptyColumn];
                        double value = opChar switch
                        {
                            '+' => currentNumbers.Sum(),
                            '*' => currentNumbers.Aggregate(1.0, (a, b) => a * b),
                            _ => throw new InvalidOperationException($"Unsupported cephalopod operator: {opChar}")
                        };
                        results2.Add(value);
                        currentNumbers.Clear();
                        lastNonEmptyColumn = -1;
                    }
                }

                if (currentNumbers.Count > 0)
                {
                    if (lastNonEmptyColumn < 0) throw new InvalidOperationException("Cannot determine operator for cephalopod problem.");
                    var opChar = operatorLine[lastNonEmptyColumn];
                    double value = opChar switch
                    {
                        '+' => currentNumbers.Sum(),
                        '*' => currentNumbers.Aggregate(1.0, (a, b) => a * b),
                        _ => throw new InvalidOperationException($"Unsupported cephalopod operator: {opChar}")
                    };
                    results2.Add(value);
                }
            }
        }

    Console.WriteLine($"Grand Total: {results.Sum()}");
    Console.WriteLine($"Grand Total Cephalopod math: {results2.Sum()}");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message.ToString());
}