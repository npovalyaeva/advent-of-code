using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    [Solution(Puzzle = "Haunted Wasteland")]
    public sealed class Day8 : Day
    {
        private Dictionary<string, Node> test = new();

        private readonly List<Node> nodes = new();
        private readonly char[] commands;

        public Day8() : base()
        {
            commands = lines[0].ToCharArray(); // 1
            foreach (var line in lines.Skip(2)) // N
            {
                var elements = Regex.Match(line, @"(.*) = \((.*), (.*)\)"); // 1

                test.Add(elements.Groups[1].Value, new Node( // 1
                    Value: elements.Groups[1].Value,
                    Left: elements.Groups[2].Value,
                    Right: elements.Groups[3].Value));

                nodes.Add(new Node( // 1
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
            var nodesEndWithA = nodes.Where(_ => _.Value.Last() == 'A').ToList();
            var stepsCountForNodes = nodesEndWithA.Select(_ => CalculateSteps(_, "Z")).ToList();

            return stepsCountForNodes.Aggregate((a, b) => TheGreatestMath.Lcm(a, b));
        }

        private long CalculateSteps(Node node, string patternToReach)
        {
            int stepsCount = 0;
            while (true)
            {
                foreach (var command in commands) // N
                {
                    node = FindNext(node, command); // 1
                    stepsCount++; // 1


                    if (node.Value.EndsWith(patternToReach))
                    {
                        return stepsCount;
                    }
                }
            }
        }

        // N
        private Node FindNext(Node node, char command) => command switch
        {
            'L' => test[node.Left],//1
            'R' => test[node.Right] //1
            //'L' => nodes.Where(_ => _.Value == node.Left).First(), // N
            //'R' => nodes.Where(_ => _.Value == node.Right).First() // N
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