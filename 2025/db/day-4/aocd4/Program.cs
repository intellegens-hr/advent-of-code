try
{
    var input = File.ReadAllLines("input.txt");
    int rows = input.Length;
    int cols = input[0].Length;
    char[][] grid = new char[rows][];
    for (int i = 0; i < rows; i++)
    {
        grid[i] = input[i].ToCharArray();
    }

    int accessible = FindAccessibleRolls(grid).Count;
    int totalRemoved = 0;

    while (true)
    {
        List<(int r, int c)> toRemove = FindAccessibleRolls(grid);

        if (toRemove.Count == 0) break;

        foreach (var (r, c) in toRemove)
            grid[r][c] = 'X';

        totalRemoved += toRemove.Count;
    }


    Console.WriteLine($"Accessed by a forklift: {accessible}");
    Console.WriteLine($"Total removed: {totalRemoved}");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static List<(int r, int c)> FindAccessibleRolls(char[][] grid)
{
    List<(int r, int c)> accessibleRole = [];
    int rows = grid.Length;
    int cols = grid[0].Length;
    int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
    int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

    for (int r = 0; r < rows; r++)
    {
        for (int c = 0; c < cols; c++)
        {
            if (grid[r][c] != '@') continue;
            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    if (grid[nr][nc] == '@')
                        count++;
                }
            }

            if (count < 4)
                accessibleRole.Add((r, c));
        }
    }

    return accessibleRole;
}