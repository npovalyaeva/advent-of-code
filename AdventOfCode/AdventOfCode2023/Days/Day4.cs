using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Gear Ratios")]
    public sealed class Day4 : Day
    {
        private readonly List<Card> cards = new();

        public Day4() : base()
        {
            foreach (var line in lines)
            {
                int count = 0;
                var test = Regex.Match(new Regex(@"\s\s+")
                    .Replace(line, " "), @"Card (.*): (.*) \| (.*)");

                var attempts = test.Groups[2].Value.Split(" ");
                foreach (var num in test.Groups[3].Value.Split(" "))
                {
                    if (attempts.Contains(num)) count++;
                }

                cards.Add(new Card {
                    Index = Int32.Parse(test.Groups[1].Value),
                    MatchesCount = count
                });
            }
        }

        protected override object Part1() =>
            cards.Where(_ => _.MatchesCount > 0).Select(_ => Math.Pow(2, (_.MatchesCount - 1))).Sum();

        protected override object Part2() => cards.Select(_ => CountCopies(_)).Sum();


        private int CountMatches()
        {
            return 0;
        }

        private int CountCopies(Card card) =>
            Enumerable.Range(1, card.MatchesCount)
                .Select(i => cards.First(_ => _.Index == card.Index + i))
                .Select(c => CountCopies(c))
                .Sum() + 1;


        private class Card
        {
            public required int Index { get; init; }
            public required int MatchesCount { get; init; }
        }
    }
}
