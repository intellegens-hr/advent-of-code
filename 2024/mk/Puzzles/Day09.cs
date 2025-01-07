using System.Numerics;

namespace AdventOfCode2024.Days;

public class Day09 : Puzzle<long>
{
    public override long First(string input)
    {
        var line = input.Select(x => int.Parse(x.ToString())).ToArray();

        var blocks = new List<int?>();

        var m = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (i % 2 == 0)
                blocks.AddRange(Enumerable.Repeat<int?>(m++, line[i]));
            else
                blocks.AddRange(Enumerable.Repeat<int?>(null, line[i]));
        }


        long checksum = 0;
        var left = 0;
        var right = blocks.Count - 1;

        while (left <= right)
        {
            if (blocks[left] == null)
            {
                if (blocks[right] != null)
                {
                    blocks[left] = blocks[right];
                    blocks.RemoveAt(right);
                    right--;
                }
                else
                {
                    right--;
                }
            }
            else
            {
                checksum += left * (blocks[left] ?? 0);
                left++;
            }
        }

        return checksum;
    }

    public override long Second(string input)
    {
        var line = input.Select(x => int.Parse(x.ToString())).ToArray();

        var files = new List<(int Length,int? FileId)>();
        for (int i = 0; i < line.Length; i++)
        {
            if (i % 2 == 0)
                files.Add((line[i], i / 2));
            else
                files.Add((line[i], null));
        }


        var visitedFiles = new HashSet<int>();
        for (int i = files.Count - 1; i >= 0; i--)
        {
            var file = files[i];

            if (file.FileId != null && !visitedFiles.Contains(file.FileId.Value))
            {
                visitedFiles.Add(file.FileId.Value);

                var moveto = files.Find(x => x.FileId == null && x.Length >= file.Length);
                if (moveto != default)
                {
                    var movetoIndex = files.IndexOf(moveto);
                    if (movetoIndex > i)
                        continue;

                    if (moveto.Length - file.Length > 0)
                    {
                        // partial replace and split
                        files[movetoIndex] = (moveto.Length - file.Length, null);
                        files[i] = (file.Length, null);
                        files.Insert(movetoIndex, (file.Length, file.FileId));
                    }
                    else
                    {
                        // full replace
                        files[i] = (file.Length, null);
                        files.RemoveAt(movetoIndex);
                        files.Insert(movetoIndex, (file.Length, file.FileId));
                    }
                }
            }
            else
            {
                // merge empty space
                if (files[i - 1].FileId == null)
                {
                    files[i - 1] = (files[i - 1].Length * files[i].Length, null);
                    files.RemoveAt(i);
                }

            }
        }


        long checksum = 0;
        var idx = 0;
        foreach (var item in files)
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (item.FileId != null)
                    checksum += idx * item.FileId.Value;

                idx++;
            }
        }

        return checksum;
    }
}

