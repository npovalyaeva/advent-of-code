using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Wait For It")]
    public sealed class Day6 : Day
    {
        private readonly List<long> times = new();
        private readonly List<long> distances = new();

        public Day6() : base()
        {
            times = Regex.Split(Regex.Replace(lines.First(), @"\s", string.Empty), @"\D+").Where(_ => _ != "").Select(_ => Int64.Parse(_)).ToList();
            distances = Regex.Split(Regex.Replace(lines.Last(), @"\s", string.Empty), @"\D+").Where(_ => _ != "").Select(_ => Int64.Parse(_)).ToList();
        }


        protected override object Part1()
        {
            long res = 1;
            for (int i = 0; i < times.Count; i++)
            {
                res *= IsWinning(times[i], distances[i]);
            }
            return res;
        }

        protected override object Part2() => null;


        private int IsWinning(long time, long distance)
        {
            int winningRacesCount = 0;
            for (int i = 1; i <= time; i++)
            {
                if (i * (time - i) > distance)
                {
                    winningRacesCount++;
                }
            }
            return winningRacesCount;
        }
    }
}
