namespace AoC2021.Solutions
{
    public class Day09 : DayAbstract<int>
    {
        public override int CalculatePart1(string input)
        {
            var data = ParseInput(input);
            return FindLowPoints(data).Select(p => data[p.x][p.y] + 1).Sum();
        }

        public override int CalculatePart2(string input)
        {
            var data = ParseInput(input);

            return FindLowPoints(data)
                .Select(p => GetBasinSize(p, data))
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate((x, y) => x * y);
        }

        private int GetBasinSize((int x, int y) point, int[][] data)
        {
            List<(int x, int y)> pointsToCheck = new() { point };
            var size = 1;

            var rows = data.Length;
            var cols = data[0].Length;

            var matches = new bool[rows, cols];
            matches[point.x, point.y] = true;

            void pointFoundCheck(bool matched, int x, int y)
            {
                if (matched && !matches[x, y] && data[x][y] < 9)
                {
                    size++;
                    pointsToCheck.Add((x, y));
                    matches[x, y] = true;
                }
            }

            while (pointsToCheck.Count > 0)
            {
                var (i, j) = pointsToCheck[0];
                pointsToCheck.RemoveAt(0);

                var aboveValid = i > 0 && data[i - 1][j] > data[i][j];
                pointFoundCheck(aboveValid, i - 1, j);

                var belowValid = i < rows - 1 && data[i + 1][j] > data[i][j];
                pointFoundCheck(belowValid, i + 1, j);

                var leftValid = j > 0 && data[i][j - 1] > data[i][j];
                pointFoundCheck(leftValid, i, j - 1);

                var rightValid = j < cols - 1 && data[i][j + 1] > data[i][j];
                pointFoundCheck(rightValid, i, j + 1);
            }

            return size;
        }

        private IEnumerable<(int x, int y)> FindLowPoints(int[][] data)
        {
            var rows = data.Length;
            var cols = data[0].Length;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    var element = data[i][j];

                    var above = i > 0 ? data[i - 1][j] : element + 1;
                    var below = i < rows - 1 ? data[i + 1][j] : element + 1;
                    var left = j > 0 ? data[i][j - 1] : element + 1;
                    var right = j < cols - 1 ? data[i][j + 1] : element + 1;

                    if (element < above && element < below && element < left && element < right)
                        yield return (i, j);
                }
            }
        }

        private static int[][] ParseInput(string input)
        {
            return input.Split("\r\n")
                .Select(x => x.Select(y => y - '0').ToArray())
                .ToArray();
        }
    }
}