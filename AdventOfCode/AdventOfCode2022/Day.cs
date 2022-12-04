namespace AdventOfCode2022
{
    public abstract class Day
    {
        protected readonly string[] lines;

        public Day() => 
            lines = File.ReadAllLines($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName}/Input Data/{GetType().Name}.txt");

        public (object, object) Run() => (Part1(), Part2());

        protected abstract object Part1();

        protected abstract object Part2();
    }
}