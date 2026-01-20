try
{
	string input = File.ReadAllText("input.txt");
    List<double> results = [];
    List<double> results2 = [];

    foreach (var item in input.Split(Environment.NewLine))
    {
        //PART 2
        results2.Add(double.Parse(MaxSubsequence(item, 12)));
        // PART 1
        var maxValue = item.Max();
        var maxIndex = item.ToList().LastIndexOf(maxValue);
        var secondMaxValue = item.Where((c, i) => i != maxIndex).Max();
        var secondMaxIndex = item.ToList().LastIndexOf(secondMaxValue);
        if (maxIndex > secondMaxIndex && maxIndex + 1 != item.Length)
        {
            secondMaxValue = item.Where((c, i) => i != maxIndex && i > maxIndex).Max();
            results.Add(double.Parse(maxValue.ToString() + secondMaxValue.ToString()));
            continue;
        }
        else if (maxIndex + 1 == item.Length)
        {
            results.Add(double.Parse(secondMaxValue.ToString() + maxValue.ToString()));
            continue;
        }
        results.Add(double.Parse(maxValue.ToString() + secondMaxValue.ToString()));
        
    }

    Console.WriteLine($"Sum: {results.Sum()}");
    Console.WriteLine($"Sum: {results2.Sum()}");
}
catch (Exception ex)
{
	Console.WriteLine($"Error during process: ${ex.Message}");
}

static string MaxSubsequence(string s, int k)
{
    if (k >= s.Length) return s;
    int toRemove = s.Length - k;
    var stack = new List<char>(s.Length);

    foreach (char c in s)
    {
        while (toRemove > 0 && stack.Count > 0 && stack[stack.Count - 1] < c)
        {
            stack.RemoveAt(stack.Count - 1);
            toRemove--;
        }

        stack.Add(c);
    }

    if (toRemove > 0)
    {
        // still need to remove from the end
        stack.RemoveRange(stack.Count - toRemove, toRemove);
    }

    return new string(stack.Take(k).ToArray());
}