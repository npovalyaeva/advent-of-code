using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Gear Ratios")]
    public sealed class Day3 : Day
    {
        protected override object Part1()
        {
            //int sum = 0;
            //foreach (var line in lines.Select((value, index) => new { index, value }))
            //{
            //    var numbers = Regex.Split(line.value, @"\D+").Where(_ => _ != "").ToList().GroupBy(_ => _);
            //    foreach (var number in numbers)
            //    {
            //        var neighbours = FindNeighbours(new Point
            //        {
            //            X = line.value.IndexOf(number),
            //            Y = line.index
            //        }, number.Length);

            //        sum += (neighbours.Where(_ => _ != '.').ToList().Count > 0) ? Int32.Parse(number) : 0;
            //    }
            //}
            return null;
        }

        private List<char> FindNeighbours(Point firstPoint, int lenght)
        {
            var neighbours = new List<char>();

            var numberPoints = new List<Point>();
            for (int i = 0; i < lenght; i++)
            {
                numberPoints.Add(new Point { X = firstPoint.X + i, Y = firstPoint.Y});
            }

            var allPoints = new List<Point>();
            for (int i = -1; i < lenght + 1; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int x = firstPoint.X + i;
                    int y = firstPoint.Y + j;
                    if (x >= 0 && x < lines[0].Length
                        && y >= 0 && y < lines.Length
                        && numberPoints.Where(_ => _.X == x && _.Y == y).ToList().Count == 0)
                        allPoints.Add(new Point { X = firstPoint.X + i, Y = firstPoint.Y + j });
                }
            }

            //neighbours = allPoints.Select(_ => lines[_.Y][_.X]).ToList();
            foreach (var point in allPoints)
            {
                neighbours.Add(lines[point.Y][point.X]);
            }
            return neighbours;
        }


        protected override object Part2()
        {
            
            return null;
        }

        // Fix the access
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                if (!(obj is Point))
                {
                    return false;
                }
                return (this.X == ((Point)obj).X)
                    && (this.Y == ((Point)obj).Y);
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }
    }

}
