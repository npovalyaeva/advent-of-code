using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Lens Library")]
    public sealed class Day15 : Day
    {
        public Day15() : base()
        {
        }

        protected override object Part1()
        {
            long sum = 0;
            foreach (var element in lines.First().Split(','))
            {
                int test = 0;
                foreach (var symbol in System.Text.Encoding.UTF8.GetBytes(element.ToCharArray()))
                {
                    test = (test + symbol) * 17 % 256;
                }
                sum += test;
            }
            return sum;
        }

        protected override object Part2()
        {
            var hashMap = new Dictionary<int, List<Entry>>();
            foreach (var element in lines.First().Split(','))
            {
                if (element.Contains('='))
                {
                    var test = element.Split('=');
                    var hash = CalcHash(test[0]);
                    if (hashMap.TryGetValue(hash, out List<Entry>? list))
                    {
                        if (list.Where(_ => _.Value == test[0]).FirstOrDefault() != null)
                        {
                            list.Where(_ => _.Value == test[0]).First().Cost = test[1];
                        }
                        else
                        {
                            list.Add(new Entry { Value = test[0], Cost = test[1] });
                        }
                    }
                    else
                    {
                        hashMap.Add(hash, new List<Entry> { new Entry { Value = test[0], Cost = test[1] } });
                    }

                }
                else // -
                {
                    var test = element.Split('-');
                    var hash = CalcHash(test[0]);
                    if (hashMap.TryGetValue(hash, out List<Entry>? list))
                    {
                        if (list.Where(_ => _.Value == test[0]).FirstOrDefault() != null)
                        {
                            list.RemoveAll(_ => _.Value == test[0]);
                        }
                    }
                }
            }

            // Calc final res, hashMap is ready
            long sum = 0;
            foreach (var slot in hashMap)
            {
                foreach (var slotItem in slot.Value.Select((value, index) => new { index, value }))
                    sum += (slot.Key + 1) * (slotItem.index + 1) * Int32.Parse(slotItem.value.Cost);
            }

            return null;
        }

        private int CalcHash(string str)
        {
            int test = 0;
            foreach (var symbol in System.Text.Encoding.UTF8.GetBytes(str.ToCharArray()))
            {
                test = (test + symbol) * 17 % 256;
            }
            return test;
        }


        public class Entry
        {
            public string Value { get; set; }
            public string? Cost { get; set; }
        }
    }
}