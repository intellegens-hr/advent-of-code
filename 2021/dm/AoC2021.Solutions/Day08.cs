namespace AoC2021.Solutions
{
    public class Day08 : DayAbstract<int>
    {
        private static readonly List<Digit> digits = new()
        {
            new Digit(0, 6, new DigitMap(false, true, true, true, false, true, true, true)),
            new Digit(1, 2, new DigitMap(true, false, false, true, false, false, true, false)),
            new Digit(2, 5, new DigitMap(false, true, false, true, true, true, false, true)),
            new Digit(3, 5, new DigitMap(false, true, false, true, true, false, true, true)),
            new Digit(4, 4, new DigitMap(true, false, true, true, true, false, true, false)),
            new Digit(5, 5, new DigitMap(false, true, true, false, true, false, true, true)),
            new Digit(6, 6, new DigitMap(false, true, true, false, true, true, true, true)),
            new Digit(7, 3, new DigitMap(true, true, false, true, false, false, true, false)),
            new Digit(8, 7, new DigitMap(true, true, true, true, true, true, true, true)),
            new Digit(9, 6, new DigitMap(false, true, true, true, true, false, true, true))
        };

        static Day08()
        {
            digits = digits.OrderBy(x => x.SegmentsCount).ToList();
        }

        public override int CalculatePart1(string input)
        {
            var entries = ParseInput(input);

            var uniqueDigits = digits.Where(x => x.DigitMap.IsUniqueMap).Select(x => x.SegmentsCount).ToArray();

            return entries
                 .Select(entry => entry.Digits.Where(x => uniqueDigits.Contains(x.Length)).Count())
                 .Sum();
        }

        public override int CalculatePart2(string input)
        {
            var entries = ParseInput(input);
            var sum = entries.Select(x => ParseEntry(x)).Sum();
            return sum;
        }

        private int ParseEntry(Entry entry)
        {
            var processList = entry.SignalPatterns.Union(entry.Digits).ToList();

            var matches = digits.ToDictionary(x => x.Number, x => new EntryDigitMap(x.Number, x));
            var allSignals = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            foreach (var digit in digits)
            {
                var entryDigit = matches[digit.Number];
                foreach (var signal in processList.Where(s => s.Length == digit.SegmentsCount))
                {
                    entryDigit.AddEntrySegment(signal);
                }
            }

            // TOP MATCH
            // Sets: 7 minus 1
            var top = matches[7].Map[0].Where(x => !matches[1].PossibleCharacters.Contains(x)).Single();

            // BOTTOM/BOTTOM-LEFT CANDIDATES
            // Sets: 9 minus (7 union 4)
            var fourUnionSeven = matches[4].PossibleCharacters.Union(matches[7].PossibleCharacters);
            var bottomBottomLeftCandidates = allSignals.Where(x => !fourUnionSeven.Contains(x)).ToArray();

            // BOTTOM LEFT AND BOTTOM MATCH
            // 6, 9, 0 - compare with bottomBottomLeftCandidates, signal that exists in all is bottom, other is bottom left
            var entries690 = matches[0].Entries.Union(matches[6].Entries).Union(matches[9].Entries);
            var m1Count = entries690.Where(x => x.Contains(bottomBottomLeftCandidates[0])).Count();
            var m2Count = entries690.Where(x => x.Contains(bottomBottomLeftCandidates[1])).Count();

            var bottomLeft = m1Count < m2Count ? bottomBottomLeftCandidates[0] : bottomBottomLeftCandidates[1];
            var bottom = m1Count > m2Count ? bottomBottomLeftCandidates[0] : bottomBottomLeftCandidates[1];

            // BOTTOM RIGHT MATCH AND UPPER RIGHT MATCH
            // 6, 9, 0 - compare with one, signal that exists in all is lower, the other is upper
            var one = matches[1].PossibleCharacters.ToArray();
            m1Count = entries690.Where(x => x.Contains(one[0])).Count();
            m2Count = entries690.Where(x => x.Contains(one[1])).Count();

            var topRight = m1Count < m2Count ? one[0] : one[1];
            var bottomRight = m1Count > m2Count ? one[0] : one[1];

            var middleUpperLeftCandidates = matches[4].PossibleCharacters.Where(x => x != topRight && x != bottomRight).ToArray();

            // MIDDLE AND UPPER LEFT MATCH
            // 6, 9, 0 - compare with middleUpperLeftCandidates, signal that exists in all is middle, the other is upper left
            m1Count = entries690.Where(x => x.Contains(middleUpperLeftCandidates[0])).Count();
            m2Count = entries690.Where(x => x.Contains(middleUpperLeftCandidates[1])).Count();
            var middle = m1Count < m2Count ? middleUpperLeftCandidates[0] : middleUpperLeftCandidates[1];
            var topLeft = m1Count > m2Count ? middleUpperLeftCandidates[0] : middleUpperLeftCandidates[1];

            var foundMatches = new char[] { top, topLeft, topRight, middle, bottomLeft, bottomRight, bottom };

            string matchedNumber = "";
            foreach (var digit in entry.Digits)
            {
                var numberMap = foundMatches.Select(x => digit.Contains(x)).ToArray();

                foreach(var mappedDigit in digits)
                {
                    var found = mappedDigit.DigitMap.Mappings
                        .Select((x, i) => new {HasSignal = x, Index = i})
                        .All(x => numberMap[x.Index] == x.HasSignal);

                    if (found)
                    {
                        matchedNumber += mappedDigit.Number;
                    }
                }
            }

            return int.Parse(matchedNumber);
        }

        private static IEnumerable<Entry> ParseInput(string input)
        {
            return input.Split("\r\n")
                .Select(x => x.Split(" | "))
                .Select(x => new Entry(x[0].Split(" "), x[1].Split(" ")));
        }
    }

    public class EntryDigitMap
    {
        public List<string> Entries { get; set; } = new();
        public List<char>[] Map { get; set; } = new List<char>[7];
        public int Number { get; }
        public Digit Digit { get; }

        public IEnumerable<char> PossibleCharacters => Map[0]
            .Union(Map[1])
            .Union(Map[2])
            .Union(Map[3])
            .Union(Map[4])
            .Union(Map[5])
            .Union(Map[6]);

        public EntryDigitMap(int number, Digit digit)
        {
            Number = number;
            Digit = digit;

            for (var i = 0; i < 7; i++)
                Map[i] = new List<char>();
        }

        public void AddEntrySegment(string segment)
        {
            for (var i = 0; i < Digit.DigitMap.Mappings.Length; i++)
            {
                if (!Digit.DigitMap.Mappings[i])
                    continue;

                foreach (var s in segment.Where(x => !Map[i].Contains(x)))
                {
                    Map[i].Add(s);
                }
                Entries.Add(segment);
            }

        }
    }

    public class DigitMap
    {
        /// <summary>
        /// Top, Top-Left, Top-Right, Middle, Bottom-Left, Bottom-Right, Bottom
        /// </summary>
        public bool[] Mappings { get; set; }

        public DigitMap(bool isUniqueMap, bool hasTopSegment, bool hasTopLeftSegment, bool hasTopRightSegment, bool hasMiddleSegment, bool hasBottomLeftSegment, bool hasBottomRightSegment, bool hasBottomSegment)
        {
            IsUniqueMap = isUniqueMap;
            Mappings = new bool[] { hasTopSegment, hasTopLeftSegment, hasTopRightSegment, hasMiddleSegment, hasBottomLeftSegment, hasBottomRightSegment, hasBottomSegment };
        }

        public bool IsUniqueMap { get; }
    }

    public class Digit
    {
        public int SegmentsCount { get; set; }
        public int Number { get; set; }
        public DigitMap DigitMap { get; set; }

        public Digit(int number, int segmentsCount, DigitMap digitMap)
        {
            DigitMap = digitMap;
            Number = number;
            SegmentsCount = segmentsCount;
        }
    }

    public class Entry
    {
        public Entry(IEnumerable<string> signalPatterns, IEnumerable<string> digits)
        {
            SignalPatterns = signalPatterns;
            Digits = digits;
        }

        public IEnumerable<string> SignalPatterns { get; set; }
        public IEnumerable<string> Digits { get; set; }

    }
}