namespace AdventOfCode2022.Days
{
    // Camp Cleanup
    public class Day4 : AdventOfCode
	{
        protected override object Part1()
        {
            var pairsCount = 0;
            foreach (var line in lines)
            {
                var section = line.Split(",").Select(_ => _.Split("-").ToArray());
                pairsCount += Convert.ToInt32(IsFullyContained(section));

            }
            return pairsCount;
        }

        protected override object Part2()
        {
            var pairsCount = 0;
            foreach (var line in lines)
            {
                var section = line.Split(",").Select(_ => _.Split("-").ToArray());
                pairsCount += Convert.ToInt32(IsOverlapped(section));

            }
            return pairsCount;
        }

        private static bool IsFullyContained(IEnumerable<string[]> section)
        {
            return !(GetRange(section.Last())).Except(GetRange(section.First())).Any()
                || !(GetRange(section.First())).Except(GetRange(section.Last())).Any();
        }

        private static bool IsOverlapped(IEnumerable<string[]> section)
        {
            return GetRange(section.Last()).Where(GetRange(section.First()).Contains).Any();
        }

        private static IEnumerable<int> GetRange(string[] section)
        {
            int[] ints = Array.ConvertAll(section, int.Parse);
            return Enumerable.Range(ints[0], ints[1] - ints[0] + 1);
        }
    }
}