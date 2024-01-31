using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Trebuchet?!")]
    public sealed class Day1 : Day
    {
        protected override object Part1() => lines
                .Select(_ => FindDigit(_, false, Mode.FirstDigit) * 10 + FindDigit(_, false, Mode.LastDigit))
                .Sum();

        protected override object Part2() => lines
                .Select(_ => FindDigit(_, true, Mode.FirstDigit) * 10 + FindDigit(_, true, Mode.LastDigit))
                .Sum();


        private static int FindDigit(string line, bool shouldParseText, Mode mode)
        {
            for (int i = 1; i <= line.Length; i++)
            {
                string substring = mode switch
                {
                    Mode.FirstDigit => line.Substring(0, i),
                    Mode.LastDigit => line.Substring(line.Length - i, i)
                };

                if (shouldParseText) substring = FindDigit(substring);

                var digit = Regex.Replace(substring, "[^0-9]", "");

                if (digit != "")
                {
                    return Int32.Parse(digit);
                }
            }
            return -1;
        }

        private static string FindDigit(string input) => input switch
        {
            string a when a.Contains("one") => "1",
            string b when b.Contains("two") => "2",
            string c when c.Contains("three") => "3",
            string d when d.Contains("four") => "4",
            string e when e.Contains("five") => "5",
            string f when f.Contains("six") => "6",
            string g when g.Contains("seven") => "7",
            string h when h.Contains("eight") => "8",
            string i when i.Contains("nine") => "9",
            _ => input
        };

        enum Mode
        {
            FirstDigit,
            LastDigit
        }
    }
}