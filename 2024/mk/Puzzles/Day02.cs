namespace AdventOfCode2024.Days;

public class Day02 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines().Select(x => x.Split(" ").Select(x => int.Parse(x)));

        var count = lines.Count(x => IsSafe(x.ToArray()));

        return count;
    }
    
    private bool IsSafe(int[] array)
    {
        if (array[0] == array[1])
            return false;

        var increasing = array[0] > array[1] ? 1 : -1;

        for (int i = 0; i < array.Length - 1; i++)
        {
            var el1 = array[i];
            var el2 = array[i + 1];

            var diff = (el1 - el2) * increasing;

            if (diff < 1 || diff > 3)
                return false;
        }

        return true;
    }

    public override int Second(string input)
    {
        var lines = input.ToLines().Select(x => x.Split(" ").Select(x => int.Parse(x)));

        var count = lines.Count(x => IsSafeWithDampener(x.ToArray()));

        return count;
    }

    private bool IsSafeWithDampener(int[] array)
    {
        // TODO: redo this with any kind of performance in mind
        
        for (int i = 0; i < array.Length; i++)
        {
            var arrayWithoutElement = array.Where((val, idx) => idx != i).ToArray();

            if (IsSafe(arrayWithoutElement))
                return true;
        }

        return false;
    }
}
