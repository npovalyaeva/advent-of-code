using System;
namespace AdventOfCode2022.Days
{
    public sealed class Day8 : Day
    {
        protected override object Part1()
        {
            var forest = lines.Select(_ => _.ToCharArray().Select(_ => Int32.Parse(_.ToString())).ToList()).ToList();
            var visibleForest = forest.Select(_ => _.Select(_ => _ = -1).ToList()).ToList();

            FindVisibleForRow(forest, visibleForest);
            for (int i = 1; i < 4; i++)
            {
                forest = RotateForest(forest);
                visibleForest = RotateForest(visibleForest);
                FindVisibleForRow(forest, visibleForest);
            }

            return visibleForest.Select(_ => _.Where(_ => _ != -1).Count()).Sum();
        }

        protected override object Part2()
        {
            var forest = lines.Select(_ => _.ToCharArray().Select(_ => Int32.Parse(_.ToString())).ToList()).ToList();
            int highestScore = 1;

            for (int col = 0; col < forest.Count; col++)
            {
                for (int row = 0; row < forest.First().Count; row++)
                {
                    var myTree = forest[row][col];

                    // UP
                    int up = 0;
                    if (row != 0)
                    {
                        for (int myRow = row-1; myRow >= 0; myRow--)
                        {
                            up++;
                            if (forest[myRow][col] >= forest[row][col]) break;
                        }
                    }


                    //DOWN
                    int down = 0;
                    if (row != forest.First().Count - 1)
                    {
                        for (int myRow = row + 1; myRow <= forest.First().Count - 1; myRow++)
                        {
                            down++;
                            if (forest[myRow][col] >= forest[row][col]) break;
                        }
                    }


                    //RIGHT
                    int right = 0;
                    if (col != forest.Count - 1)
                    {
                        for (int myCol = col + 1; myCol <= forest.Count - 1; myCol++)
                        {
                            right++;
                            if (forest[row][myCol] >= forest[row][col]) break;
                        }
                    }

                    // LEFT
                    int left = 0;
                    if (col != 0)
                    {
                        for (int myCol = col - 1; myCol >= 0; myCol--)
                        {
                            left++;
                            if (forest[row][myCol] >= forest[row][col]) break;
                        }
                    }


                    int score = up * left * down * right;
                    if (score > highestScore) highestScore = score;
                }
            }
            return highestScore;

        }

        private static void FindVisibleForRow(List<List<int>> forest, List<List<int>> visibleForest)
        {
            for (int col = 0; col < forest.Count; col++)
            {
                visibleForest[0][col] = forest[0][col];
                var treeToCompare = forest[0][col];

                for (int row = 1; row < forest.First().Count; row++)
                {
                    if (forest[row][col] > treeToCompare)
                    {
                        visibleForest[row][col] = forest[row][col];
                        treeToCompare = forest[row][col];
                    }
                }
            }
        }

        private static List<List<int>> RotateForest(List<List<int>> forest)
            => forest.Select(lineOfTrees => lineOfTrees.Select((s, i) => new { s, i }))
                    .SelectMany(a => a).GroupBy(a => a.i, a => a.s)
                    .Select(a => a.Reverse().ToList()).ToList();
    }
}

