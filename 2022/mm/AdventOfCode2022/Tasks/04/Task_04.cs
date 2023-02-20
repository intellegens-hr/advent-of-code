using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._04
{
    public class Task_04
    {
        private static bool SequenceContains(int start1, int end1, int start2, int end2)
        {
            return start1 <= start2 && end1 >= end2;
        }
        private static bool SequenceOverlaps(int start1, int end1, int start2, int end2)
        {
            return start2 <= end1 && end2 >= start1;
        }

        public static int PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\04\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\04\Example.txt");
            string line = String.Empty;
            int s = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                var first = parts[0].Split('-');
                var second = parts[1].Split('-');

                if (SequenceContains(Convert.ToInt32(first[0]), Convert.ToInt32(first[1]), Convert.ToInt32(second[0]), Convert.ToInt32(second[1]))) { s++; continue; }
                if (SequenceContains(Convert.ToInt32(second[0]), Convert.ToInt32(second[1]), Convert.ToInt32(first[0]), Convert.ToInt32(first[1]))) { s++; continue; }

            }

            return s;
        }

        public static int PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\04\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\04\Example.txt");
            string line = String.Empty;
            int s = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                var first = parts[0].Split('-');
                var second = parts[1].Split('-');

                if (SequenceOverlaps(Convert.ToInt32(first[0]), Convert.ToInt32(first[1]), Convert.ToInt32(second[0]), Convert.ToInt32(second[1]))) { s++; continue; }
                if (SequenceOverlaps(Convert.ToInt32(second[0]), Convert.ToInt32(second[1]), Convert.ToInt32(first[0]), Convert.ToInt32(first[1]))) { s++; continue; }

            }

            return s;
        }

        
    }
}
