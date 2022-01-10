using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Helpers
{
    public class FileReader
    {
        public static List<string> ReadFileIntoStringList(string path)
        {
            StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd().Split("\n").ToList();
        }
    }
}
