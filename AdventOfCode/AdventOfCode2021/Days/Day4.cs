using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    [Solution(Puzzle = "Giant Squid")]
    public sealed class Day4 : Day
    {
        protected override object Part1()
        {
            //(string[] numbersToDraw, List<List<Dictionary<string, bool>>> boards) = ReadLines();

            //foreach (var numberToDraw in numbersToDraw)
            //{
            //    boards.ForEach(board => board.ForEach(row =>
            //    {
            //        if (row.ContainsKey(numberToDraw))
            //            row[numberToDraw] = true;
            //    }));

            //    var winningBoard = ReturnWinnerOrDefault(boards);
            //    if (winningBoard != null)
            //    {
            //        var test = Int32.Parse(numberToDraw) * CalculateUnmarkedNumbersSum(winningBoard);
            //        return Int32.Parse(numberToDraw) * CalculateUnmarkedNumbersSum(winningBoard);
            //    }
            //}
            return null;
        }

        protected override object Part2()
        {
            (string[] numbersToDraw, List<List<Dictionary<string, bool>>> boards) = ReadLines();
            long lastWinSum = 0;

            foreach (var numberToDraw in numbersToDraw)
            {
                boards.ForEach(board => board.ForEach(row =>
                {
                    if (row.ContainsKey(numberToDraw))
                        row[numberToDraw] = true;
                }));

                var winningBoards = ReturnWinnerOrDefault(boards);
                if (winningBoards != null)
                {
                    foreach (var board in winningBoards)
                    {
                        lastWinSum = Int32.Parse(numberToDraw) * CalculateUnmarkedNumbersSum(board);
                        boards.Remove(board);
                    }                    
                }
            }
            return null;
        }

        private (string[] numbersToDraw, List<List<Dictionary<string, bool>>> boards) ReadLines()
        {
            var numbersToDraw = lines.First().Split(',');

            var boards = new List<List<Dictionary<string, bool>>>();  
            var board = new List<Dictionary<string, bool>>();
            foreach (var line in lines.Skip(2))
            {
                switch (line)
                {
                    case "":
                        boards.Add(board);
                        board = new List<Dictionary<string, bool>>();
                        break;
                    default:
                        board.Add(line.Trim(' ').Replace("  ", " ").Split(' ') // Fix it
                            .Select(_ => new KeyValuePair<string, bool>(_, false))
                            .ToDictionary(x => x.Key, x => x.Value));
                        break;
                }
            }
            return (numbersToDraw, boards);
        }

        private static List<List<Dictionary<string, bool>>>? ReturnWinnerOrDefault(List<List<Dictionary<string, bool>>> boards)
        {
            bool isAdded = false;
            var winningBoards = new List<List<Dictionary<string, bool>>>();
            foreach (var board in boards)
            {
                isAdded = false;
                // Check rows
                board.ForEach(row =>
                {
                    if (!row.ContainsValue(false))
                    {
                        winningBoards.Add(board);
                        isAdded = true;
                    }
                        
                });

                if (!isAdded)
                {
                    // Check colomns
                    var test = board.Select(lineOfTrees => lineOfTrees.Select((s, i) => new { s, i }))
                        .SelectMany(a => a).GroupBy(a => a.i, a => a.s)
                        .Select(a => a.ToDictionary(x => x.Key, x => x.Value)).ToList();

                    test.ForEach(row =>
                    {
                        if (!row.ContainsValue(false))
                            winningBoards.Add(board);
                    });
                }
                
            }
            return winningBoards;
        }

        private static int CalculateUnmarkedNumbersSum(List<Dictionary<string, bool>> board)
        {
            int sum = 0;
            board.ForEach(row =>
            {
                sum += row.Where(_ => !_.Value).Select(_ => Int32.Parse(_.Key)).Sum();
            });
            return sum;
        }
    }
}