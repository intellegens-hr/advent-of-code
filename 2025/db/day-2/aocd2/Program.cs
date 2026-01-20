using System;

try
{
    string input = File.ReadAllText("input.txt");
    List<Int64> invalidIds = [];
    List<Int64> invalidIdsP2 = [];

    foreach (var (start, end) in input.Split(',')
        .Select(r => r.Split('-'))
        .Select(p => (Int64.Parse(p[0]), Int64.Parse(p[1]))))
    {
        for (Int64 i = start; i <= end; i++)
        {
            string value = i.ToString();
            //if (value.Length % 2 != 0) continue;
            //int half = value.Length / 2;
            //string firstHalf = value.Substring(0, half);
            //string secondHalf = value.Substring(half, half);
            //if (firstHalf.Equals(secondHalf))
            //{
            //    invalidIds.Add(i);
            //}
            // PART 2
            if (value.Length < 2) continue;
            string doubled = value + value;
            int idx = doubled.IndexOf(value, 1, StringComparison.Ordinal);
            if (idx >= 0 && idx < value.Length)
            {
                invalidIdsP2.Add(i);
            }
        }
    }

    Console.WriteLine($"Part 1: {invalidIds.Sum()}");
    Console.WriteLine($"Part 2: {invalidIdsP2.Sum()}");
}
catch (Exception ex)
{
    Console.WriteLine("Exception: " + ex.Message);
}
