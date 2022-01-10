namespace AoC2021.Solutions.Models.Day04
{
   public class Board
    {
        private int? measure;
        private int? lastCalledNumber;
        private readonly int rows;
        private readonly int columns;

        public int Measure
        {
            get
            {
                if (measure == null)
                {
                    CalculateMeasure();
                }
                return measure.Value;
            }
        }

        public Board(IList<int[]> fields)
        {
            Fields = fields;
            MatchedFields = fields.Select(x => x.Select(y => false).ToArray()).ToArray();
            rows = Fields.Count;
            columns = Fields[0].Length;
        }

        public IList<int[]> Fields { get; set; }
        private bool[][] MatchedFields { get; set; }

        public void FlagNumber(int number)
        {
            lastCalledNumber = number;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (Fields[row][column] == number)
                    {
                        MatchedFields[row][column] = true;
                    }
                }
            }
        }

        public bool HasBingo()
        {
            if (MatchedFields.Any(x => x.All(y => y)))
            {
                return true;
            }

            for (int column = 0; column < columns; column++)
            {
                var hasMatch = MatchedFields
                    .Select(x => x.Where((y, i) => i == column).First())
                    .All(x => x);

                if (hasMatch)
                {
                    return true;
                }
            }

            return false;
        }

        private void CalculateMeasure()
        {
            var sum = Fields
                .Select((x, row) => x.Where((_, column) => !MatchedFields[row][column]).Sum())
                .Sum();

            this.measure = sum * lastCalledNumber.Value;
        }
    }
}