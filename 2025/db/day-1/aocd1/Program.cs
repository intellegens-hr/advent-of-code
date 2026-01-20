try
{
    string? line;
    long sum = 50; // current dial position (0..99)
    long numberOfZero = 0; // times dial is exactly 0 at the end of a rotation
    long numberOfZeroDuring = 0; // times dial points at 0 during the rotation (excluding the final click)

    using var sr = new StreamReader("input.txt");
    while ((line = sr.ReadLine()) != null)
    {
        line = line.Trim();
        if (line.Length < 2) continue;

        char dir = line[0];
        if (dir != 'R' && dir != 'L') throw new FormatException($"Invalid direction in line: '{line}'");

        if (!long.TryParse(line.Substring(1), out var steps) || steps < 0)
            throw new FormatException($"Invalid numeric part in line: '{line}'");

        long start = sum;

        // Smallest positive step t0 (1..100) after the start that lands on 0:
        // - For R (increasing): need t ≡ -start (mod 100) -> t0 = (100 - start) % 100 (0 -> 100)
        // - For L (decreasing): need t ≡ start (mod 100) -> t0 = start % 100 (0 -> 100)
        long t0 = dir == 'R' ? ((100 - start) % 100) : (start % 100);
        if (t0 == 0) t0 = 100;

        // Count occurrences with t in [1..steps-1] (during rotation, excluding final click)
        long crossesDuring = 0;
        if (steps > t0)
        {
            crossesDuring = 1 + (steps - 1 - t0) / 100;
        }

        numberOfZeroDuring += crossesDuring;

        // Apply rotation and normalize to [0..99]
        long delta = dir == 'R' ? steps : -steps;
        sum = ((start + delta) % 100 + 100) % 100;

        // Count final click separately
        if (sum == 0) numberOfZero++;
    }

    Console.WriteLine(numberOfZero);
    Console.WriteLine(numberOfZeroDuring + numberOfZero);
}
catch (Exception ex)
{
    Console.WriteLine("Exception: " + ex.Message);
}
