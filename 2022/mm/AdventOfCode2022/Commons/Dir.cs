using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Commons
{
    public class Dir
    {
        public string Name { get; set; }
        public Dir Parent { get; set; }
        public List<Dir> Children { get; set; } = new();
        public List<File> Files { get; set; } = new();

        public long GetSize()
        {
            long s = 0;
            foreach (var dir in Children) s += dir.GetSize();
            s += Files.Sum(f => f.Size);

            return s;
        }

        public long GetSizeIfSmallerThan(long n, ref long s)
        {
            long tempS = 0;
            foreach (var dir in Children) tempS += dir.GetSizeIfSmallerThan(n, ref s);
            tempS += Files.Sum(f => f.Size);

            if (tempS <= n) s += tempS;

            return tempS;
        }

        /// <summary>
        /// Based on a name method will either return and existing Dir from children or create one and return it.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dir CreateOrFind(string name)
        {
            var folderName = name;
            var nextFolder = Children.FirstOrDefault(c => c.Name == folderName);
            if (nextFolder != null)
            {
                return nextFolder;
            }
            else
            {
                var newDir = new Dir()
                {
                    Name = folderName,
                    Parent = this
                };
                Children.Add(newDir);
                return newDir;
            }
        }

        /// <summary>
        /// Adds a file to this folder if the file does not exist
        /// </summary>
        /// <param name="file"></param>
        public void AddFileIfDoesntExist(File file)
        {
            if (!Files.Any(f => f.Name == file.Name)) { Files.Add(file); }
        }

        public void GetDirSizeDictionary(Dictionary<string, long> dirSizeDictionary)
        {
            long s = 0;
            foreach (var subDir in Children)
            {
                subDir.GetDirSizeDictionary(dirSizeDictionary);
                s += subDir.GetSize();
            }
            s += Files.Sum(f => f.Size);

            if (!dirSizeDictionary.ContainsKey(Name))
            {
                dirSizeDictionary.Add(Name, s);
            }
        }
    }
}
