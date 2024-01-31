using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Hot Springs")]
    public sealed class Day12 : Day
    {
        private readonly List<Row> records = new();

        public Day12() : base()
        {
            foreach (var line in lines)
            {
                var test = line.Split(' ');
                var testChar = test[0];
                var newString = testChar + '?' + testChar + '?' + testChar + '?' + testChar + '?' + testChar;
                var resultChar = newString.ToCharArray();

                var intTest = test[1].Split(',').Select(int.Parse).ToArray();
                var resultInt = Enumerable.Repeat(intTest, 5).SelectMany(x => x).ToArray();

                //records.Add(new Row(test[0].ToCharArray(), test[1].Split(',').Select(int.Parse).ToArray()));
                records.Add(new Row(resultChar, resultInt));
            }
        }

        protected override object Part1()
        {
            long count = 0;
            foreach (var record in records)
            {
                Queue<char> test = new();
                for (int i = 0; i < record.Record.Length; i++)
                {
                    test.Enqueue(record.Record[i]);
                }

                count += CalcArrangementsCount(new Queue<char>(), test, record.Pattern);
                Console.WriteLine(count);
            }
            return count;
        }

        private static long CalcArrangementsCount(Queue<char> beginning, Queue<char> ending, int[] pattern)
        {
            long count = 0;
            if (ending.Count == 0)
            {
                var testAgain = new string(beginning.ToArray());
                var myNewTest = testAgain.Split('.').Where(_ => _ != "").ToList();
                if (myNewTest.Count() == pattern.Length)
                {
                    for (int i = 0; i < pattern.Length; i++)
                    {
                        if (myNewTest[i].Count() != pattern[i])
                            return 0;
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var elementToProcess = ending.Dequeue();
                if (elementToProcess == '?')
                {
                    var copy1 = new Queue<char>(beginning);
                    copy1.Enqueue('.');
                    var copy2 = new Queue<char>(beginning);
                    copy2.Enqueue('#');
                    count += CalcArrangementsCount(copy1, new Queue<char>(ending), pattern);
                    count += CalcArrangementsCount(copy2, new Queue<char>(ending), pattern);
                }
                else
                {
                    beginning.Enqueue(elementToProcess);
                    count += CalcArrangementsCount(beginning, new Queue<char>(ending), pattern);
                }

            }
            
            return count;
        }

        protected override object Part2() => null;

        public record Row(char[] Record, int[] Pattern);
    }

}