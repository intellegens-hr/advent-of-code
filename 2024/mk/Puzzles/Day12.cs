namespace AdventOfCode2024.Days;

public class Day12 : Puzzle<int>
{
    public class Block
    {
        private readonly char[,] _map;

        public Block(char[,] map, int i, int j)
        {
            this._map = map;
            this.Letter = map[i,j];
            this.Locations.Add((i, j));
        }

        public char Letter { get; set; }

        public int Area { get; set; } = 1;
        public int Perimeter { get; set; }
        public int Sides { get; set; }

        public List<(int i, int j)> Locations = [];

        public (int i, int j)? SurroundedBy = null;

        public override string ToString()
        {
            return Letter + " " + Area + " " + Perimeter;
        }

        readonly List<(int x, int y)> _directions = [ (0, 1), (1, 0), (0, -1), (-1, 0),];
        public bool WithinMap((int x, int y) pos) => pos.x >= 0 && pos.y >= 0 && pos.x < _map.GetLength(0) && pos.y < _map.GetLength(1);

        public int CalculateSides()
        {
            var first = Locations.OrderBy(x => x.i).ThenBy(x => x.j).First();
            var location = first;

            var directionIndex = 0;
            var sides = 0;

            var forward = _directions[directionIndex];
            var left = _directions[(directionIndex + 4 - 1) % 4];

            var surrounded = true;
            char? leftSide = null;
            if (!WithinMap((location.i + left.x, location.j + left.y)))
                surrounded = false;
            else
                leftSide = _map[location.i + left.x, location.j + left.y];

            while (true)
            {
                forward = _directions[directionIndex];
                left = _directions[(directionIndex + 4 - 1) % 4];

                if (surrounded && leftSide != null && (!WithinMap((location.i + left.x, location.j + left.y)) || _map[location.i + left.x, location.j + left.y] != leftSide.Value))
                {
                    surrounded = false;
                }

                // can turn left
                if (WithinMap((location.i + left.x, location.j + left.y)) && _map[location.i + left.x, location.j + left.y] == _map[location.i, location.j])
                {
                    // turn left
                    sides++;
                    directionIndex = (directionIndex + 4 - 1) % 4;

                    if (surrounded && leftSide != null && (!WithinMap((location.i + left.x, location.j + left.y)) || _map[location.i + left.x, location.j + left.y] != leftSide.Value))
                    {
                        surrounded = false;
                    }
                    // continue forward
                    forward = _directions[directionIndex];
                    location = (location.i + forward.x, location.j + forward.y);
                }
                // can continue forward
                else if (WithinMap((location.i + forward.x, location.j + forward.y)) && _map[location.i + forward.x, location.j + forward.y] == _map[location.i, location.j])
                {
                    // continue forward
                    location = (location.i + forward.x, location.j + forward.y);
                }
                // else turn right
                else
                {
                    // turn right
                    sides++;
                    directionIndex = (directionIndex + 1) % 4;
                }


                if (first == location && directionIndex == 0)
                {
                    if (surrounded)
                        SurroundedBy = (location.i + left.x, location.j + left.y);

                    return sides;
                }
            }



        }
    }
    public override int First(string input)
    {
        var map = input.ToLines().To2DArray();
        var blocks = new Block[map.GetLength(0), map.GetLength(1)];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                    continue;

                FillNeighbours(map, blocks, i, j);
            }
        }

        var distinctBlocks = new HashSet<Block>();
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                distinctBlocks.Add(blocks[i, j]);
            }
        }
        foreach (var item in distinctBlocks)
        {
            Console.WriteLine($"{item.Letter}: {item.Area} * {item.Perimeter}");
        }
        return distinctBlocks.Sum(x => x.Area * x.Perimeter);
    }

    public bool WithinMap((int x, int y) pos, char[,] map) => pos.x >= 0 && pos.y >= 0 && pos.x < map.GetLength(0) && pos.y < map.GetLength(1);

    readonly List<(int x, int y)> _directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    private void FillNeighbours(char[,] map, Block[,] blocks, int i, int j)
    {
        if (blocks[i, j] == null)
            blocks[i, j] = new Block(map, i, j);

        foreach (var (x, y) in _directions)
        {
            if (WithinMap((i + x, j + y), map) && map[i, j] == map[i + x, j + y])
            {
                if (blocks[i + x, j + y] != null)
                    continue;

                blocks[i + x, j + y] = blocks[i, j];
                blocks[i + x, j + y].Area++;
                blocks[i + x, j + y].Locations.Add((i + x, j + y));

                FillNeighbours(map, blocks, i + x, j + y);
            }
            if (!WithinMap((i + x, j + y), map) || blocks[i, j] != blocks[i + x, j + y])
            {
                blocks[i, j].Perimeter++;
            }
        }
    }

    public override int Second(string input)
    {
        var map = input.ToLines().To2DArray();
        var blocks = new Block[map.GetLength(0), map.GetLength(1)];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                    continue;

                FillNeighbours(map, blocks, i, j);
            }
        }

        var distinctBlocks = new HashSet<Block>();
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                distinctBlocks.Add(blocks[i, j]);
            }
        }
        foreach (var item in distinctBlocks)
        {
            var sides = item.CalculateSides();
            item.Sides += sides;

            if (item.SurroundedBy != null)
                blocks[item.SurroundedBy.Value.i, item.SurroundedBy.Value.j].Sides += sides;

            //Console.WriteLine($"{item.Letter}: {item.Area} * {item.GetSides()}");
        }
        //foreach (var block in distinctBlocks.Where(x => x.SurroundedBy != null))
        //{
        //    Console.WriteLine($"{block.Letter}: {block.Area} * {block.Sides}");

        //    for (int i = 0; i < blocks.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < blocks.GetLength(1); j++)
        //        {
        //            if (block.Locations.Contains((i, j)))
        //            {
        //                Console.BackgroundColor = ConsoleColor.Blue;
        //                Console.ForegroundColor = ConsoleColor.White;
        //            }
        //            Console.Write(map[i,j]);
        //            //else
        //            //    Console.Write(" ");
        //            ////distinctBlocks.Add(blocks[i, j]);
        //            Console.ResetColor();
        //        }
        //        Console.WriteLine();
        //    }

        //    Console.Clear();
        //}
        return distinctBlocks.Sum(x => x.Area * x.Sides);
    }
}

