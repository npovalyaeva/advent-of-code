using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Days
{
    [Solution(Puzzle = "Binary Diagnostic")]
    public sealed class Day3 : Day
    {
        protected override object Part1()
        {
            int range = 12;
            int[] bit0Count = new int[range];
            int[] bit1Count = new int[range];

            for (int index = 0; index < range;  index++)
            {
                foreach (var line in lines)
                {
                    switch (line[index])
                    {
                        case '0':
                            bit0Count[index]++;
                            break;
                        case '1':
                            bit1Count[index]++;
                            break;
                    }
                }
            }

            var gammaRate = Convert.ToInt32(String.Join("", bit0Count.Zip(bit1Count, (first, second) => first > second ? "0" : "1")), 2);
            var epsilonRate = Convert.ToInt32(String.Join("", bit0Count.Zip(bit1Count, (first, second) => first < second ? "0" : "1")), 2);

            return gammaRate * epsilonRate;
        }

        protected override object Part2()
        {
            return null;
        }

        //private string DetermineValue(string[] numbers, string bitCriteria = "")
        //{
        //    var matchedNumbersWith0 = numbers.Where(_ => new Regex($@"^{bitCriteria}0").IsMatch(_)).ToList();
        //    var matchedNumbersWith1 = numbers.Where(_ => new Regex($@"^{bitCriteria}1").IsMatch(_)).ToList();

        //    if (matchedNumbers.Count() == 1) return matchedNumbers.First();
        //    DetermineValue(matchedNumbers, bitCriteria, comparedFunction);
        //}

        private int CompareAndGetBigger(int a, int b) =>  (a > b ? a : b);
        private int CompareAndGetSmaller(int a, int b) => (a < b ? a : b);
    }
}