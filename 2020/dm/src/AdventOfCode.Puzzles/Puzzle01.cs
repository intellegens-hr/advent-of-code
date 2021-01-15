using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public class Puzzle01
    {
        public static int TuplesProductForGivenSum(IEnumerable<int> inputs, int requiredSum, int tupleCount)
        {
            IEnumerable<IEnumerable<int>> inputsEnumerable = inputs.Select(x => new int[] { x });

            for (int i = 2; i <= tupleCount; i++)
                inputsEnumerable = 
                    from a in inputsEnumerable
                    from b in inputs
                    where a.Sum() + b <= requiredSum
                    select a.Append(b);

            return inputsEnumerable
                .Where(x => x.Sum() == requiredSum)
                .FirstOrDefault()
                .Aggregate((a, b) => a * b);
        }
    }
}