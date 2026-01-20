namespace AdventOfCode2025.Days;

public class Day08 : Puzzle<long>
{
    public override long First(string input, int limit)
    {
        var positions = input.ToLines().Select(x => x.Split(",").Select(int.Parse).ToList()).Select(x => (x[0], x[1], x[2])).ToList();
        List<((int, int, int), (int, int, int))> pairs = [];

        for (int i = 0; i < positions.Count - 1; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                pairs.Add((positions[i], positions[j]));
            }
        }

        var pairsOrdered = pairs.OrderBy(x => x.Item1.DistanceTo(x.Item2)).ToList();
        var circuits = new List<HashSet<(int, int, int)>>();

        for (var i = 0; i < limit; i++)
        {
            var pair = pairsOrdered[i];
            var cir1 = circuits.FirstOrDefault(c => c.Contains(pair.Item1));
            var cir2 = circuits.FirstOrDefault(c => c.Contains(pair.Item2));

            if (cir1 == cir2)
            {
                if (cir1 == null && cir2 == null)
                    circuits.Add([pair.Item1, pair.Item2]);
            }
            else if (cir1 == null && cir2 != null)
            {
                cir2.Add(pair.Item1);
            }
            else if (cir1 != null && cir2 == null)
            {
                cir1.Add(pair.Item2);
            }
            else
            {
                foreach (var item in cir2!)
                    cir1!.Add(item);

                circuits.Remove(cir2);
            }
        }

        return circuits.Select(x => x.Count).OrderByDescending(x => x).Take(3).Aggregate((a,b) => a * b);
    }

    public override long Second(string input)
    {
        var positions = input.ToLines().Select(x => x.Split(",").Select(int.Parse).ToList()).Select(x => (x[0], x[1], x[2])).ToList();
        List<(int, int)> pairs = [];

        for (int i = 0; i < positions.Count - 1; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                pairs.Add((i, j));
            }
        }

        var pairsOrdered = pairs.OrderBy(x => positions[x.Item1].DistanceTo(positions[x.Item2])).ToList();
        var circuits = new List<HashSet<int>>();

        for (var i = 0; i < pairsOrdered.Count; i++)
        {
            var pair = pairsOrdered[i];
            var cir1 = circuits.FirstOrDefault(c => c.Contains(pair.Item1));
            var cir2 = circuits.FirstOrDefault(c => c.Contains(pair.Item2));
            
            if (cir1 == cir2)
            {
                if (cir1 == null && cir2 == null)
                    circuits.Add([pair.Item1, pair.Item2]);
            }
            else if (cir1 == null && cir2 != null)
            {
                cir2.Add(pair.Item1);
            }
            else if (cir1 != null && cir2 == null)
            {
                cir1.Add(pair.Item2);
            }
            else
            {
                foreach (var item in cir2!)
                    cir1!.Add(item);

                circuits.Remove(cir2);
            }

            if (circuits.Count == 1 && circuits[0].Count == positions.Count)
                return (long)positions[pair.Item1].Item1 * positions[pair.Item2].Item1;
        }

        return 0;
    }
}