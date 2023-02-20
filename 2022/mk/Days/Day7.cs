using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Days;

public class Day7 : Puzzle<int>
{
    public override int Day => 7;

    public override int First()
    {
        var lines = GetInputLines();

        var root = ParseFileSystemStructure(lines);

        return root.GetFolders().Where(a => a.Size < 100000).Sum(a => a.Size);

    }

    public override int Second()
    {
        var lines = GetInputLines();

        var root = ParseFileSystemStructure(lines);
        var availableSize = 70000000;
        var updateSize = 30000000;

        var unusedSpace = availableSize - root.Size;
        var toDelete = updateSize - unusedSpace;

        var folderToDelete = root.GetFolders().Where(a => a.Size > toDelete).OrderBy(a => a.Size).First();

        return folderToDelete.Size;

    }

    private static Folder ParseFileSystemStructure(string[] lines)
    {
        var root = new Folder();
        var currentFolder = root;
        var lsReading = false;

        foreach (var line in lines)
        {
            if (line.StartsWith("$"))
            {
                lsReading = false;

                var cmds = line.Split(new[] { "$ ", " " }, StringSplitOptions.RemoveEmptyEntries);
                var cmd = cmds[0];

                if (cmd == "ls")
                {
                    lsReading = true;
                }
                else if (cmd == "cd")
                {
                    var arg = cmds[1];
                    if (arg == "..")
                    {
                        currentFolder = currentFolder.Parent;
                    }
                    else if (arg == "/")
                    {
                        currentFolder = root;
                    }
                    else
                    {
                        currentFolder = currentFolder.GetChild(arg) as Folder;
                    }
                }
            }
            else if (lsReading)
            {
                var spl = line.Split(" ");
                if (spl[0] == "dir")
                {
                    currentFolder.AddChild(new Folder { Name = spl[1] });
                }
                else
                {
                    currentFolder.AddChild(new File { Name = spl[1], Size = int.Parse(spl[0]) });
                }
            }
        }

        return root;
    }


    public class FileSystemObject
    {
        public string Name { get; set; }
        public Folder Parent { get; set; }
        public virtual int Size { get; set; }
    }

    public class File : FileSystemObject
    {
        public override string ToString() => $"{Name} (file, size={Size})";
    }

    public class Folder : FileSystemObject
    {
        public List<FileSystemObject> Children { get; set; } = new List<FileSystemObject>();

        public override int Size => Children.Sum(f => f.Size);

        public override string ToString() => $"{Name} (dir)";

        public void AddChild(FileSystemObject child)
        {
            if (GetChild(child.Name) != null)
                return;

            child.Parent = this;
            Children.Add(child);
        }

        public FileSystemObject? GetChild(string name)
        {
            return Children.FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Folder> GetFolders()
        {
            return Children.OfType<Folder>().Concat(Children.OfType<Folder>().SelectMany(f => f.GetFolders()));
        }
    }
}
