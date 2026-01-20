try
{
    var input = File.ReadLines("input.txt");
    var grid = input
        .Select(line => line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToList())
        .ToList();
    int rowLength = grid[0].Count;
    for (var i = 0; i < grid.Count; i++)
    {
        for (var j = 0; j < rowLength; j++)
        {
            Console.Write(grid[i][j] + " ");
        }
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}