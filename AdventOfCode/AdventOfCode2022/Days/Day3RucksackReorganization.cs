namespace AdventOfCode2022
{
    // Rucksack Reorganization
    public sealed class Day3 : AdventOfCode
    {
        const int ASCII_CODE_TO_COMPARE_TO = 93; // could be 91..96
        const int UPPERCASE_SUBTRAHEND = 38;
        const int LOWERCASE_SUBTRAHEND = 96;

        private static int FindPriority(char symbol) => symbol - (symbol < ASCII_CODE_TO_COMPARE_TO ? UPPERCASE_SUBTRAHEND : LOWERCASE_SUBTRAHEND);

        protected override object Part1(string[] lines)
        {
            var prioritiesSum = 0;

            foreach (var line in lines)
            {
                var compartments = line.Chunk(line.Length / 2).Select(x => new string(x)).ToList();
                var appearingItem = compartments[0].Where(_ => compartments[1].Contains(_)).First();
                prioritiesSum += FindPriority(appearingItem);
            }

            return prioritiesSum;
        }

        protected override object Part2(string[] lines)
        {
            var prioritiesSum = 0;
            string[][] groups = lines.Chunk(3).ToArray();

            foreach (var group in groups)
            {
                var appearingItem = group[0].Intersect(group[1]).Intersect(group[2]).First();
                prioritiesSum += FindPriority(appearingItem);
            }
            return prioritiesSum;
        }
    }
}

