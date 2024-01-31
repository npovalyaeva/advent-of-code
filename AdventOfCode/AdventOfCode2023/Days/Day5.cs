using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "If You Give A Seed A Fertilizer")]
    public sealed class Day5 : Day
    {
        private readonly List<long> seeds = new();
        private readonly List<List<MapLine>> maps = new();

        public Day5() : base()
        {
            //seeds = Regex.Split(lines.First(), @"\D+").Where(_ => _ != "").Select(_ => Int64.Parse(_)).ToList();
            var test = Regex.Split(lines.First(), @"\D+").Where(_ => _ != "").Select(_ => Int64.Parse(_)).ToList();
            for (int i = 0; i < test.Count; i+=2)
            {
                for (long j = 0; j < test.Skip(i + 1).First(); j++)
                {
                    seeds.Add(test.Skip(i).First() + j);
                }
            }

            List<MapLine> map = new();
            foreach (var line in lines.Skip(3))
            {
                var splittedLine = line.Split(" ");
                switch (splittedLine)
                {
                    case [.., "map:"]:
                        map = new();
                        break;
                    case [string source, string destination, string length]:
                        map.Add(new MapLine
                        {
                            Source = Int64.Parse(source),
                            Destination = Int64.Parse(destination),
                            Length = Int64.Parse(length)
                        });
                        break;
                    case [""]:
                        maps.Add(map);
                        break;
                }
            }
            maps.Add(map);
        }


        protected override object Part1()
        {
            List<long> locations = new();
            foreach (var seed in seeds.Select((value, index) => new { index, value }))
            {
                long middleResult = seed.value;
                foreach (var map in maps)
                {
                    foreach (var mapLine in map)
                    {
                        var (flag, result) = IsWorking(mapLine, middleResult);
                        if (flag)
                        {
                            middleResult = result;
                            
                            break;
                        }
                    }
                }
                locations.Add(middleResult);
                Console.WriteLine($"{seed.index} / {seeds.Count()}");
            }
            locations.Sort();
            var res = locations.First();
            return locations.First();
        }

        protected override object Part2() => null;


        private (bool, long) IsWorking(MapLine mapLine, long initial)
        {
            if (initial >= mapLine.Destination && initial - mapLine.Destination < mapLine.Length)
                return (true, initial + (mapLine.Source - mapLine.Destination));
            return (false, -1);
        }

        private class MapLine
        {
            public required long Source { get; set; }

            public required long Destination { get; set; }

            public required long Length { get; set; }
        }
    }
}
