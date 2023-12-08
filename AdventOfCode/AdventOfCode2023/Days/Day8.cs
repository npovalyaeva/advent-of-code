using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Haunted Wasteland")]
    public sealed class Day8 : Day
    {

        private readonly List<Node> nodes = new();
        private readonly char[] commands;

        public Day8() : base()
        {
            commands = lines[0].ToCharArray();
            foreach (var line in lines.Skip(2))
            {
                var elements = Regex.Match(line, @"(.*) = \((.*), (.*)\)");
                nodes.Add(new Node(
                    Value: elements.Groups[1].Value,
                    Left: elements.Groups[2].Value,
                    Right: elements.Groups[3].Value)
                );
            }
        }

        protected override object Part1() =>
            CalculateSteps(
                node: nodes.Where(_ => _.Value == "AAA").First(),
                patternToReach: "ZZZ"
            );

        protected override object Part2()
        {
            long lcm = 1;
            var nodesEndWithA = nodes.Where(_ => _.Value.Last() == 'A').ToList();
            var stepsCountForNodes = nodesEndWithA.Select(_ => CalculateSteps(_, "Z")).ToList();

            foreach (var steps in stepsCountForNodes)
            {
                lcm = TheGreatestMath.Lcm(steps, lcm);
            }
            return lcm;
        }

        private int CalculateSteps(Node node, string patternToReach)
        {
            int stepsCount = 0;
            while (true)
            {
                foreach (var command in commands)
                {
                    node = FindNext(node, command);
                    stepsCount++;

                    if (node.Value.EndsWith(patternToReach))
                    {
                        return stepsCount;
                    }
                }
            }
        }

        private Node FindNext(Node node, char command) => command switch
        {
            'L' => nodes.Where(_ => _.Value == node.Left).First(),
            'R' => nodes.Where(_ => _.Value == node.Right).First()
        };

        private record class Node(string Value, string Left, string Right);
    }

    public static class TheGreatestMath
    {
        public static long Gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long Lcm(long a, long b) => a / Gcf(a, b) * b;
    }
}