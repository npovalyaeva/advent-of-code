using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    [Solution(Puzzle = "Hydrothermal Venture")]
    public sealed class Day5 : Day
    {
        protected override object Part1() => GetOverlappedPoints(false);
        protected override object Part2() => GetOverlappedPoints(true);

        private int GetOverlappedPoints(bool allowsDiagonalLines)
        {
            List<Line> linesOfVents = ReadInput(allowsDiagonalLines);
            var diagram = new Dictionary<Point, int>();
            foreach (var coveredPoint in linesOfVents.SelectMany(_ => _.Points).ToList())
            {
                if (diagram.ContainsKey(coveredPoint))
                    diagram[coveredPoint] += 1;
                else
                    diagram.Add(coveredPoint, 1);
            }

            return diagram.Where(_ => _.Value > 1).Count();
        }

        private List<Line> ReadInput(bool allowsDiagonalLines)
        {
            var linesOfVents = new List<Line>();
            foreach (var inputLine in lines)
            {
                if (inputLine.Split(" ") is [string firstCoordinates, "->", string secondCoordinates])
                {
                    linesOfVents.Add(new Line(
                        oneEnd: new Point { X = Int32.Parse(firstCoordinates.Split(',').First()), Y = Int32.Parse(firstCoordinates.Split(',').Last()) },
                        otherEnd: new Point { X = Int32.Parse(secondCoordinates.Split(',').First()), Y = Int32.Parse(secondCoordinates.Split(',').Last()) },
                        allowsDiagonalLines: allowsDiagonalLines
                    ));
                }
            }
            return linesOfVents;
        }
    }

    internal class Point
    {
        public required int X { get; set; }
        public required int Y { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Point)) return false;
            return (this.X == ((Point)obj).X) && (this.Y == ((Point)obj).Y);
        }

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
    }

    internal class Line
    {
        private readonly List<Point> points = new List<Point>();
        public List<Point> Points { get => points; }

        public Line(Point oneEnd, Point otherEnd, bool allowsDiagonalLines)
        {
            // Will not work if oneEnd is the same as otherEnd
            if (oneEnd.X == otherEnd.X)
                points.AddRange(Enumerable
                    .Range(oneEnd.Y <= otherEnd.Y ? oneEnd.Y : otherEnd.Y, Math.Abs(oneEnd.Y - otherEnd.Y) + 1)
                    .Select(_ => new Point { X = oneEnd.X, Y = _ })
                    .ToHashSet());

            if (oneEnd.Y == otherEnd.Y)
                points.AddRange(Enumerable
                    .Range(oneEnd.X <= otherEnd.X ? oneEnd.X : otherEnd.X, Math.Abs(oneEnd.X - otherEnd.X) + 1)
                    .Select(_ => new Point { X = _, Y = oneEnd.Y })
                    .ToHashSet());

            if (allowsDiagonalLines)
            {
                // Make sure the line is diagonal
                if (Math.Abs(oneEnd.X - otherEnd.X) == Math.Abs(oneEnd.Y - otherEnd.Y))
                {
                    List<int> listOfXs = Enumerable.Range(oneEnd.X <= otherEnd.X ? oneEnd.X : otherEnd.X, Math.Abs(oneEnd.X - otherEnd.X) + 1).ToList();
                    if (oneEnd.X > otherEnd.X) listOfXs.Reverse();
                    List<int> listOfYs = Enumerable.Range(oneEnd.Y <= otherEnd.Y ? oneEnd.Y : otherEnd.Y, Math.Abs(oneEnd.Y - otherEnd.Y) + 1).ToList();
                    if (oneEnd.Y > otherEnd.Y) listOfYs.Reverse();


                    for (int i = 0; i < listOfXs.Count; i++)
                        points.Add(new Point { X = listOfXs[i], Y = listOfYs[i] });
                }
            }
        }
    }
}