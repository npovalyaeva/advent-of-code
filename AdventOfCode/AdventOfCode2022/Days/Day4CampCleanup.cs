using static System.Collections.Specialized.BitVector32;

namespace AdventOfCode2022.Days
{
    // Camp Cleanup
    public sealed class Day4 : Day
	{
        protected override object Part1() => CalcPairsCount(Contain);
        protected override object Part2() => CalcPairsCount(Overlap);

        private object CalcPairsCount(Func<IEnumerable<int[]>, bool> action)
        {
            var pairsCount = 0;

            foreach (var line in lines)
            {
                var sections = line.Split(",")
                    .Select(_ => _.Split("-"))
                    .Select(_ => Array.ConvertAll(_, int.Parse));

                pairsCount += Convert.ToInt32(action(sections));
            }
            return pairsCount;
        }

        private static bool Contain(IEnumerable<int[]> s) =>
            (s.First().First() >= s.Last().First() && s.First().Last() <= s.Last().Last())
            || (s.Last().First() >= s.First().First() && s.Last().Last() <= s.First().Last());

        private static bool Overlap(IEnumerable<int[]> s) =>
            s.First().First() <= s.Last().Last() && s.Last().First() <= s.First().Last();
    }
}