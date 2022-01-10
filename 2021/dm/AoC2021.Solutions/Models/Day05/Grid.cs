namespace AoC2021.Solutions.Models.Day05
{
    public class Grid
    {
        private readonly int[,] fields;
        private readonly int rows;
        private readonly int cols;

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            fields = new int[rows, cols];
        }

        public int CountFieldsWithAtLeastCount(int count)
        {
            var sum = 0;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (fields[i, j] >= count)
                        sum++;
                }
            }

            return sum;
        }

        public void AddLine(Line line)
        {
            var fromX = line.X1;
            var toX = line.X2;
            var fromY = line.Y1;
            var toY = line.Y2;

            var steps = (fromX == toX ? Math.Abs(fromY - toY) : Math.Abs(fromX - toX)) + 1;
            var directionX = fromX == toX ? 0 : fromX > toX ? -1 : 1;
            var directionY = fromY == toY ? 0 : fromY > toY ? -1 : 1;

            for (var i = 0; i < steps; i++)
            {
                fields[fromX + i * directionX, fromY + i * directionY]++;
            }
        }
    }
}