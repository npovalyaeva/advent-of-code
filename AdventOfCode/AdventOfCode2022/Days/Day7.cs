using System;
using System.Xml.Linq;

namespace AdventOfCode2022.Days
{
    public class Day7 : Day
    {
        private int _sizeToFree;

        public record Item
        {
            public required string Name { get; set; }
            public int Size { get; set; }
            public Dir? Parent { get; set; }
        };

        public record Dir : Item
        {
            public List<Item>? Items { get; set; }
        }

        public record File : Item { }

        protected override object Part1()
        {
            Dir filesystem = CreateFileSystem();
            _sizeToFree = 30000000 - (70000000 - filesystem.Size);
            var test = Part2Part(filesystem);
            //var test = FindDirsWithSize(filesystem);
            var trst2 = test.Min();
            return test.Min();
        }

        protected override object Part2()
        {
            throw new NotImplementedException();
        }

        private Dir CreateFileSystem()
        {
            Dir filesystem = new Dir { Name = "/" };
            Dir activeDir = filesystem;

            var commands = lines.Skip(1);
            foreach (var command in commands)
            {
                var output = command.Split(" ");
                switch (output)
                {
                    case ["$", "cd", ".."]:
                        activeDir = activeDir.Parent;
                        break;
                    case ["$", "cd", string name]:
                        activeDir = (Dir)activeDir.Items.Where(_ => _.Name == name).First();
                        break;
                    case ["$", "ls"]:
                        activeDir.Items = new();
                        break;
                    case ["dir", string name]:
                        activeDir.Items.Add(new Dir { Name = name, Parent = activeDir });
                        break;
                    case [string size, string name]:
                        activeDir.Items.Add(new File { Name = name, Size = Int32.Parse(size), Parent = activeDir });
                        break;
                }
            }
            CalcSizes(filesystem);
            return filesystem;
        }

        private void CalcSizes(Dir dir)
        {
            foreach (var item in dir.Items)
            {
                if (item is Dir) CalcSizes((Dir)item);
            }
            dir.Size = dir.Items.Sum(_ => _.Size);
        }

        private List<int> FindDirsWithSize(Dir dir)
        {
            List<int> sizes = new();
            foreach (var item in dir.Items)
            {
                if (item is Dir)
                {
                    if (item.Size <= 100000)
                    {
                        sizes.Add(item.Size);
                    }
                    sizes.AddRange(FindDirsWithSize((Dir)item));
                }
            }
            return sizes;
        }

        private List<int> Part2Part(Dir dir)
        {
            List<int> sizes = new();
            foreach (var item in dir.Items)
            {
                if (item is Dir)
                {
                    if (item.Size >= _sizeToFree)
                    {
                        sizes.Add(item.Size);
                    }
                    sizes.AddRange(Part2Part((Dir)item));
                }
            }
            return sizes;
        }

    }
}