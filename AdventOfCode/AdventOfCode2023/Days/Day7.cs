using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Wait For It")]
    public sealed class Day7 : Day
    {
        private readonly Dictionary<Type, List<Hand>> handsMap = new();
        private readonly List<char> labels = new() { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
        private readonly List<char> labelsWithJoker = new() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

        public Day7() : base()
        {
            foreach (Type type in (Enum.GetValues(typeof(Type))))
            {
                handsMap.Add(type, new List<Hand>());
            }
            

            int currentBid = 1;
            //foreach (Type type in (Enum.GetValues(typeof(Type))))
            //{

            //    foreach (var card in handsMap[type])
            //    {
            //        res += card.Rank * currentBid;
            //        currentBid++;
            //    }
            //}
        }

        protected override object Part1()
        {
            var sortedHands = SortHands(labels);
            foreach (var line in sortedHands)
            {
                var arr = line.Split(" ");
                var handType = GetHandType(arr.First());
                handsMap[handType].Add(new Hand(arr.First(), int.Parse(arr.Last())));
            }

            return null;
        }

        protected override object Part2()
        {
            var sortedHands = SortHands(labelsWithJoker);
            foreach (var hand in sortedHands)
            {
                Console.WriteLine(hand);
            }
            foreach (var line in sortedHands)
            {
                var arr = line.Split(" ");
                var handType = GetHandType(arr.First());
                handsMap[handType].Add(new Hand(arr.First(), int.Parse(arr.Last())));
            }

            return null;
        }

        private string[] SortHands(List<char> order) => lines
            .OrderBy(_ => order.IndexOf(_[0]))
            .ThenBy(_ => order.IndexOf(_[1]))
            .ThenBy(_ => order.IndexOf(_[2]))
            .ThenBy(_ => order.IndexOf(_[3]))
            .ThenBy(_ => order.IndexOf(_[4]))
            .ToArray();

        //private int CountTotalWinnings

        private Type GetHandType(string hand)
        {
            // Create a dictionary where the key is a card, and the value is the count of the cards with this symbol
            var dictionaryOfCards = new Dictionary<char, int>();
            foreach (var card in hand.ToCharArray())
            {
                if (dictionaryOfCards.TryGetValue(card, out int count))
                {
                    dictionaryOfCards[card] = count + 1;
                }
                else
                {
                    dictionaryOfCards.Add(card, 1);
                }
            }

            // For One
            //if (dictionaryOfCards.FirstOrDefault(_ => _.Value == 5).Key != default)
            //    return Type.FiveOfAKind;
            //if (dictionaryOfCards.FirstOrDefault(_ => _.Value == 4).Key != default)
            //    return Type.FourOfAKind;
            //if (dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default
            //    && dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default)
            //    return Type.FullHouse;
            //if (dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default)
            //    return Type.ThreeOfAKind;
            //if (dictionaryOfCards.Where(_ => _.Value == 2).ToList().Count == 2)
            //    return Type.TwoPair;
            //if (dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default)
            //    return Type.OnePair;
            //return Type.HighCard;

            // For Two
            int countOfJokers = dictionaryOfCards.TryGetValue('J', out int jokers) ? jokers : 0;
            dictionaryOfCards.Remove('J');

            // JJJJJ
            if (dictionaryOfCards.Count == 0)
                return Type.FiveOfAKind;

            if ((dictionaryOfCards.FirstOrDefault(_ => _.Value == 5).Key != default)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 4).Key != default && countOfJokers == 1)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default && countOfJokers == 2)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default && countOfJokers == 3)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 1).Key != default && countOfJokers == 4))
                return Type.FiveOfAKind;

            if ((dictionaryOfCards.FirstOrDefault(_ => _.Value == 4).Key != default)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default && countOfJokers == 1)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default && countOfJokers == 2)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 1).Key != default && countOfJokers == 3))
                return Type.FourOfAKind;

            if ((dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default
                && dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default)
                || (countOfJokers == 2 && dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default)
                || (countOfJokers == 1 && dictionaryOfCards.Where(_ => _.Value == 2).ToList().Count == 2))
                return Type.FullHouse;

            if ((dictionaryOfCards.FirstOrDefault(_ => _.Value == 3).Key != default)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default && countOfJokers == 1)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 1).Key != default && countOfJokers == 2))
                return Type.ThreeOfAKind;

            if (dictionaryOfCards.Where(_ => _.Value == 2).ToList().Count == 2
                || ((countOfJokers == 1) && dictionaryOfCards.Where(_ => _.Value == 2).ToList().Count == 1))
                return Type.TwoPair;

            if ((dictionaryOfCards.FirstOrDefault(_ => _.Value == 2).Key != default)
                || (dictionaryOfCards.FirstOrDefault(_ => _.Value == 1).Key != default && countOfJokers == 1))
                return Type.OnePair;

            return Type.HighCard;
        }


        public enum Type
        {
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            FullHouse = 5,
            FourOfAKind = 6,
            FiveOfAKind = 7,
        }

        public record Hand(string Cards, int Rank);
    }
}
