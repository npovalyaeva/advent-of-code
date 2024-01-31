using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2021.Days
{
    [Solution(Puzzle = "Dive!")]
    public sealed class Day2 : Day
    {
        protected override object Part1()
        {
            (int, int) coordinates = (0, 0);
            foreach (var line in lines)
            {
                var command = line.Split(" ");
                switch (command)
                {
                    case ["forward", string coordinate]:
                        coordinates.Item1 += Int32.Parse(coordinate);
                        break;
                    case ["down", string coordinate]:
                        coordinates.Item2 += Int32.Parse(coordinate);
                        break;
                    case ["up", string coordinate]:
                        coordinates.Item2 -= Int32.Parse(coordinate);
                        break;
                }
            }
            return coordinates.Item1 * coordinates.Item2;
        }

        protected override object Part2()
        {
            (int, int, int) coordinates = (0, 0, 0);
            foreach (var line in lines)
            {
                var command = line.Split(" ");
                switch (command)
                {
                    case ["forward", string coordinate]:
                        coordinates.Item1 += Int32.Parse(coordinate);
                        coordinates.Item2 += coordinates.Item3 * Int32.Parse(coordinate);
                        break;
                    case ["down", string coordinate]:
                        coordinates.Item3 += Int32.Parse(coordinate);
                        break;
                    case ["up", string coordinate]:
                        coordinates.Item3 -= Int32.Parse(coordinate);
                        break;
                }
            }
            return coordinates.Item1 * coordinates.Item2;
        }

    }
}