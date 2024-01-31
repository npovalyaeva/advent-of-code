using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Cube Conundrum")]
    public sealed class Day2 : Day
    {
        protected override object Part1()
        {
            //int idsSum = 0;
            //foreach (var line in lines)
            //{
            //    var output = line.Split(" ");
            //    int gameID = Int32.Parse(output[1].Remove(output[1].Length-1));
            //    int test = 2;
            //    for (int i = 2; i < output.Length; i = i + 2)
            //    {
            //        if (output[i + 1].Contains("red"))
            //        {
            //            if (Int32.Parse(output[i]) > 12)
            //                break;
            //        }
            //        else if (output[i + 1].Contains("green"))
            //        {
            //            if (Int32.Parse(output[i]) > 13)
            //                break;
            //        }
            //        else
            //        {
            //            if (Int32.Parse(output[i]) > 14)
            //                break;
            //        }
            //        test = i;
            //    }
            //    if (test == output.Length - 2) idsSum += gameID;
            //}
            return null;
            }


        protected override object Part2()
        {
            int idsSum = 0;
            foreach (var line in lines)
            {
                int[] arr = new int[3];

                var output = line.Split(" ");
                int gameID = Int32.Parse(output[1].Remove(output[1].Length - 1));
                for (int i = 2; i < output.Length; i = i + 2)
                {
                    if (output[i + 1].Contains("red"))
                    {
                        if (Int32.Parse(output[i]) > arr[0])
                            arr[0] = Int32.Parse(output[i]);
                    }
                    else if (output[i + 1].Contains("green"))
                    {
                        if (Int32.Parse(output[i]) > arr[1])
                            arr[1] = Int32.Parse(output[i]);
                    }
                    else
                    {
                        if (Int32.Parse(output[i]) > arr[2])
                            arr[2] = Int32.Parse(output[i]);
                    }
                }
                idsSum += arr[0] * arr[1] * arr[2];
            }
            return null;
        }
    }


        enum Bag
        {
            red = 12,
            green = 13,
            blue = 14
        }
    }
