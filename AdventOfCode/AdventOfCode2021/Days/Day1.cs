using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2021.Days
{
    [Solution(Puzzle = "Sonar Sweep")]
    public sealed class Day1 : Day
    {
        protected override object Part1() => null; //GetACountOfIncreases(ConvertToListOfInt());
        protected override object Part2()
        {
            return null;
            //var test = ConvertToListOfInt();
            //return GetACountOfIncreases(test.Zip(test.Skip(1), (a, b) => Tuple.Create(a, b)).Zip(test.Skip(2), (a, b) => a.Item1 + a.Item2 + b).ToList());
        }
            

        private List<int> ConvertToListOfInt() => lines.Select(_ => Int32.Parse(_)).ToList();

        private int GetACountOfIncreases(List<string> lines)
        {
            var nums = lines.Select(_ => Int32.Parse(_)).ToList();
            return nums.Zip(nums.Skip(1), (first, second) => first < second).Count(_ => _ == true);
        }
    }
}