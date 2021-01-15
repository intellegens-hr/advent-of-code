using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Commons
{
    public static class ReadInputs
    {
        public static string GetFullPath(string filename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Inputs\\", filename);
        }

        public static StreamReader OpenInputFile(string filename)
        {
            return new StreamReader(GetFullPath(filename));
        }

        public static List<int> ReadAllInts(string filename)
        {
            return File.ReadAllLines(GetFullPath(filename)).Select(s => Convert.ToInt32(s)).ToList();
        }

        public static List<Int64> ReadAllInt64s(string filename)
        {
            return File.ReadAllLines(GetFullPath(filename)).Select(s => Convert.ToInt64(s)).ToList();
        }

        public static List<string> ReadAllStrings(string filename)
        {
            return File.ReadAllLines(GetFullPath(filename)).ToList();
        }

        public static string ReadWholeFileAsString (string filename)
        {
            return File.ReadAllText(GetFullPath(filename));
        }
    }
}
