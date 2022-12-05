namespace AdventOfCode2022.Days
{
    [Solution(Puzzle = "Calorie Counting")]
    public sealed class Day1 : Day
    {
        protected override object Part1() => CalcCalories(1);
        protected override object Part2() => CalcCalories(3);

        private object CalcCalories(int amountOfElves)
        {
            var elfKey = 0;
            var sumsOfCalories = lines
                .Select(_ => new { Item = _, Key = (_ != "") ? elfKey : elfKey++ })
                .Where(_ => _.Item != "")
                .GroupBy(_ => _.Key)
                .Select(_ => _.Select(_ => Int32.Parse(_.Item)).Sum());

            return sumsOfCalories.OrderDescending().Take(amountOfElves).Sum();
        }
    }
}