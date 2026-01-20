try
{
    var input = File.ReadAllText("input.txt").Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
    var ids = input[0]
        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .ToList();
    var availableIngridientIds = input[1]
        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .ToList();
    List<double> freshIngridients = [];
    List<(long start, long end)> freshIngridientIds = new List<(long, long)>();

    foreach (string line in ids)
    {
        string[] rangeParts = line.Split('-', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(p => p.Trim())
                                  .ToArray();
        if (rangeParts.Length != 2)
            continue;

        if (!long.TryParse(rangeParts[0], out long start) ||
            !long.TryParse(rangeParts[1], out long end))
            continue;

        if (start > end)
        {
            long tmp = start;
            start = end;
            end = tmp;
        }

        freshIngridientIds.Add((start, end));
    }
    

    foreach (var availableIngridientId in availableIngridientIds)
    {
        foreach ((long start, long end) in freshIngridientIds)
        {
            if (start <= long.Parse(availableIngridientId) && long.Parse(availableIngridientId) <= end)
            {
                freshIngridients.Add(long.Parse(availableIngridientId));
                break;
            }
        }
    }

    // PART 2
    var sortedFreshIngridientIds = freshIngridientIds.OrderBy(i => i.start).ToList();

    var freshIngridientIdsInterval = new List<(long start, long end)>();

    foreach (var interval in sortedFreshIngridientIds)
    {
        if (freshIngridientIdsInterval.Count == 0)
        {
            freshIngridientIdsInterval.Add(interval);
        }
        else
        {
            var last = freshIngridientIdsInterval[freshIngridientIdsInterval.Count - 1];

            if (interval.start <= last.end + 1)
            {
                freshIngridientIdsInterval[freshIngridientIdsInterval.Count - 1] = (last.start, Math.Max(last.end, interval.end));
            }
            else
            {
                freshIngridientIdsInterval.Add(interval);
            }
        }
    }

    Console.WriteLine(freshIngridients.Count);
    Console.WriteLine($"Ukupno jedinstvenih fresh ID-eva: {freshIngridientIdsInterval.Sum(i => i.end - i.start + 1)}");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}