using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    public sealed class Day5 : Day
    {
        protected override object Part1() => Rearrange(false);
        protected override object Part2() => Rearrange(true);

        private object Rearrange(bool isOrderRetained)
        {
            var stacksOfCrates = InitializeStacksOfCrates();
            foreach (var line in lines)
            {
                var rearrangementNums = Regex.Matches(line, @"\d+").Cast<Match>()
                    .Select(_ => Int32.Parse(_.Value)).ToArray();

                List<char> cratesToMove = new();
                for (int i = 0; i < rearrangementNums[0]; i++)
                {
                    cratesToMove.Add(stacksOfCrates[rearrangementNums[1] - 1].Pop());

                }
                if (isOrderRetained) cratesToMove.Reverse();
                foreach (var crate in cratesToMove)
                {
                    stacksOfCrates[rearrangementNums[2] - 1].Push(crate);
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

            List<Stack<char>> stacksOfCrates = new();
            foreach (var list in listsOfCrates)
            {
                stacksOfCrates.Add(new Stack<char>(list));
            }
            return stacksOfCrates;
        }
    }
}