namespace AdventOfCode2022.Day1

{
    internal class Day1
    {
        const string path = "C:\\Users\\nikol\\source\\VisualStudio\\AdventOfCode\\2022\\AdventOfCode2022\\AdventOfCode2022\\Day1\\calories.txt";

        public int CountCalroies()
        {
            int elfCount = 0;
            int maxElfCount = 0;            

            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (elfCount > maxElfCount)
                    {
                        maxElfCount = elfCount;                                               
                    }

                    elfCount = 0;
                    continue;
                }

                elfCount += int.Parse(line);
            }

            return maxElfCount;
        }

        public int CountThreeCalroies()
        {
            int elfCount = 0;
            int[] maxElfCounts = new[] { 0, 0, 0 };

            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) 
                {
                    var minElfCount = maxElfCounts.Min(x => x);
                    if (elfCount > minElfCount)
                    {
                        int minElfCounIndex = Array.FindIndex(maxElfCounts, c => c == minElfCount);
                        maxElfCounts[minElfCounIndex] = elfCount;
                    }

                    elfCount = 0;
                    continue;
                }

                elfCount += int.Parse(line);
            }

            return maxElfCounts.Sum(x => x);
        }
    }
}
