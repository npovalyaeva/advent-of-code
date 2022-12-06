using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    [Solution(Puzzle = "Supply Stacks")]
    public sealed class Day5 : Day
    {
        protected override object Part1() => Rearrange(isOrderRetained: false);
        protected override object Part2() => Rearrange(isOrderRetained: true);

        private object Rearrange(bool isOrderRetained)
        {
            var stacksOfCrates = InitializeStacksOfCrates();
            foreach (var line in lines)
            {
                var procedure = Regex.Matches(line, @"\d+").Select(_ => Int32.Parse(_.Value)).ToArray();
                if (procedure is [int amount, int from, int to])
                {
                    List<char> cratesToMove = new();
                    for (int i = 0; i < amount; i++)
                    {
                        cratesToMove.Add(stacksOfCrates[from - 1].Pop());
                    }

                    if (isOrderRetained) cratesToMove.Reverse();
                    cratesToMove.ForEach(_ => stacksOfCrates[to - 1].Push(_));
                }
            }
            return String.Join("", stacksOfCrates.Select(_ => _.Pop()));
        }

        // Are you going to say I'm cheeter? Hah do it.
        private static List<Stack<char>> InitializeStacksOfCrates()
        {
            var listsOfCrates = new List<List<char>>
            {
                new List<char>{ 'D', 'H', 'N', 'Q', 'T', 'W', 'V', 'B' },
                new List<char>{ 'D', 'W', 'B' },
                new List<char>{ 'T', 'S', 'Q', 'W', 'J', 'C' },
                new List<char>{ 'F', 'J', 'R', 'N', 'Z', 'T', 'P' },
                new List<char>{ 'G', 'P', 'V', 'J', 'M', 'S', 'T' },
                new List<char>{ 'B', 'W', 'F', 'T', 'N' },
                new List<char>{ 'B', 'L', 'D', 'Q', 'F', 'H', 'V', 'N' },
                new List<char>{ 'H', 'P', 'F', 'R' },
                new List<char>{ 'Z', 'S', 'M', 'B', 'L', 'N', 'P', 'H' },
            };
            return listsOfCrates.Select(_ => new Stack<char>(_)).ToList();
        }
    }
}