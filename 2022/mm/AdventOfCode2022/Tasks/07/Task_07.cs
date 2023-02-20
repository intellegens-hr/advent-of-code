using AdventOfCode2022.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._07
{
    public class Task_07
    {
        public static long PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\07\PartOne.txt");
            // using var reader = new StreamReader(@"..\..\..\Tasks\07\Example.txt");
            string line = String.Empty;
            string contentLine = String.Empty;
            long s = 0;
            Queue<char> queue = new Queue<char>();

            Dir root = new Dir();
            root.Name = "/";

            Dir currentDir = root;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("$ ls"))
                {
                    // Read more lines
                    while ((contentLine = reader.ReadLine()) != null && !contentLine.StartsWith("$"))
                    {
                        if (contentLine.StartsWith("dir "))
                        {
                            // Directory, create it if it doesn't exist!
                            var temp = currentDir.CreateOrFind(contentLine.Substring(4));
                        }
                        else
                        {
                            var parts = contentLine.Split(' ');
                            var myFile = new Commons.File()
                            {
                                Directory = currentDir,
                                Name = parts[1],
                                Size = Convert.ToInt64(parts[0])
                            };
                            currentDir.AddFileIfDoesntExist(myFile);
                        }
                    }

                    if (contentLine == null)
                    {
                        long mySum = 0;
                        long rootSum = root.GetSizeIfSmallerThan(100000, ref mySum);
                        return mySum;
                    }

                    line = contentLine;
                }

                if (line.StartsWith("$ cd /")) currentDir = root;
                else if (line.StartsWith("$ cd ..")) currentDir = currentDir.Parent;
                else if (line.StartsWith("$ cd"))
                {
                    var folderName = line.Substring(5);
                    currentDir = currentDir.CreateOrFind(folderName);
                }
            }

            long mySumMain = 0;
            long rootSumMain = root.GetSizeIfSmallerThan(100000, ref mySumMain);
            return mySumMain;
        }



        public static long PartTwo()
        {
            long diskSpace = 70000000;
            long diskNeeded = 30000000;

            using var reader = new StreamReader(@"..\..\..\Tasks\07\PartOne.txt");
            // using var reader = new StreamReader(@"..\..\..\Tasks\07\Example.txt");
            string line = String.Empty;
            string contentLine = String.Empty;
            long s = 0;
            Queue<char> queue = new Queue<char>();

            Dir root = new Dir();
            root.Name = "/";

            Dir currentDir = root;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("$ ls"))
                {
                    // Read more lines
                    while ((contentLine = reader.ReadLine()) != null && !contentLine.StartsWith("$"))
                    {
                        if (contentLine.StartsWith("dir "))
                        {
                            // Directory, create it if it doesn't exist!
                            var temp = currentDir.CreateOrFind(contentLine.Substring(4));
                        }
                        else
                        {
                            var parts = contentLine.Split(' ');
                            var myFile = new Commons.File()
                            {
                                Directory = currentDir,
                                Name = parts[1],
                                Size = Convert.ToInt64(parts[0])
                            };
                            currentDir.AddFileIfDoesntExist(myFile);
                        }
                    }

                    if (contentLine == null)
                    {
                        line = string.Empty;
                    }
                    else
                    {
                        line = contentLine;
                    }
                }

                if (line.StartsWith("$ cd /")) currentDir = root;
                else if (line.StartsWith("$ cd ..")) currentDir = currentDir.Parent;
                else if (line.StartsWith("$ cd"))
                {
                    var folderName = line.Substring(5);
                    currentDir = currentDir.CreateOrFind(folderName);
                }
            }

            Dictionary<string, long> dirSizes = new Dictionary<string, long>();
            root.GetDirSizeDictionary(dirSizes);
            var minimumNeeded = diskNeeded - (diskSpace - dirSizes["/"]);

            var size = dirSizes.Where(d => d.Value > minimumNeeded).OrderBy(d => d.Value).FirstOrDefault();
            return size.Value;
        }
    }
}
