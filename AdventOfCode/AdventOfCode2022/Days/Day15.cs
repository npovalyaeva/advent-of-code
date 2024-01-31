using System;
using System.Text.RegularExpressions;
using static AdventOfCode2022.Days.Day7;

namespace AdventOfCode2022.Days
{
	public class Day15 : Day
	{
        protected override object Part1()
        {
            List<Point> points = new();
            List<Position> arrangement = new();
            foreach (var line in lines)
            {
                var coordinates = Regex.Match(line, @"Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)");
                arrangement.Add(new Position
                {
                    Sensor = new Point { X = int.Parse(coordinates.Groups[1].Value), Y = int.Parse(coordinates.Groups[2].Value) },
                    Beacon = new Point { X = int.Parse(coordinates.Groups[3].Value), Y = int.Parse(coordinates.Groups[4].Value) },
                });   
            }
            foreach (var pair in arrangement)
            {
                var length = Math.Abs(pair.Sensor.X - pair.Beacon.X) + Math.Abs(pair.Sensor.Y - pair.Beacon.Y);

                if (2000000 >= pair.Sensor.Y - length && 2000000 <= pair.Sensor.Y + length)
                {
                    int from = pair.Sensor.X - length;
                    int to = pair.Sensor.X + length;

                    for (int x = from; x <= to; x++) // X
                    {
                        if ((Math.Abs(pair.Sensor.X - x) + Math.Abs(pair.Sensor.Y - 2000000) <= length))
                        {
                            points.Add(new Point { X = x, Y = 2000000 });
                        }
                    }
                }
                    

            }

            foreach (var pair in arrangement)
            {
                points.RemoveAll(_ => _.X == pair.Sensor.X && _.Y == pair.Sensor.Y);
                points.RemoveAll(_ => _.X == pair.Beacon.X && _.Y == pair.Beacon.Y);
            }


            int test1 = points.Where(_ => _.Y == 2000000).GroupBy(_ => _.X).Count();

            return points.Where(_ => _.Y == 2000000).GroupBy(_ => _.X).Count();
        }

        protected override object Part2()
        {
            List<Point> points = new();
            List<Position> arrangement = new();
            foreach (var line in lines)
            {
                var coordinates = Regex.Match(line, @"Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)");
                arrangement.Add(new Position
                {
                    Sensor = new Point { X = int.Parse(coordinates.Groups[1].Value), Y = int.Parse(coordinates.Groups[2].Value) },
                    Beacon = new Point { X = int.Parse(coordinates.Groups[3].Value), Y = int.Parse(coordinates.Groups[4].Value) },
                });
            }


            foreach (var pair in arrangement)
            {
                var length = Math.Abs(pair.Sensor.X - pair.Beacon.X) + Math.Abs(pair.Sensor.Y - pair.Beacon.Y);
                int from = pair.Sensor.Y - 1;
                int to = pair.Sensor.Y - length;
                for (int y = from; y >= to; y--)
                {
                    points.Add(new Point { Y = y, X = pair.Sensor.X - length - 1 + (pair.Sensor.Y - y) });
                    points.Add(new Point { Y = y, X = pair.Sensor.X + length + 1 - (pair.Sensor.Y - y) });
                }

                points.Add(new Point { X = pair.Sensor.X, Y = pair.Sensor.Y - length - 1 }); // up
                points.Add(new Point { X = pair.Sensor.X, Y = pair.Sensor.Y + length + 1 }); // down
                points.Add(new Point { X = pair.Sensor.X - length - 1, Y = pair.Sensor.Y }); // left
                points.Add(new Point { X = pair.Sensor.X + length + 1, Y = pair.Sensor.Y }); // right

                from = pair.Sensor.Y + 1;
                to = pair.Sensor.Y + length;
                for (int y = from; y <= to; y++)
                {
                    points.Add(new Point { Y = y, X = pair.Sensor.X - length - 1 - (pair.Sensor.Y - y) });
                    points.Add(new Point { Y = y, X = pair.Sensor.X + length + 1 + (pair.Sensor.Y - y) });
                }
            }


            int beaconX = 0, beaconY = 0;
            foreach (var point in points)
            {
                if ((point.X >= 0 && point.X <= 4000000) && (point.Y >= 0 && point.Y <= 4000000))
                {
                    beaconX = point.X;
                    beaconY = point.Y;
                    string mark = "outside";
                    foreach (var pair in arrangement)
                    {
                        var length = Math.Abs(pair.Sensor.X - pair.Beacon.X) + Math.Abs(pair.Sensor.Y - pair.Beacon.Y);
                        if ((Math.Abs(pair.Sensor.X - beaconX) + Math.Abs(pair.Sensor.Y - beaconY) <= length))
                        {
                            mark = "inside";
                            break;
                        }
                    }
                    if (mark == "outside")
                    {
                        int test = beaconX * 4000000 + beaconY;
                        //break;
                    }
                }
                    


            }
            int result = beaconX * 4000000 + beaconY;

            return null;
        }
    }

    public class Position
    {
        public required Point Sensor { get; set; }

        public required Point Beacon { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

