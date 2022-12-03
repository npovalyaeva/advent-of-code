namespace AdventOfCode2022
{
    public abstract class AdventOfCode
    {
        public (object, object) Run()
        {
            var lines = File.ReadAllLines($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName}/Input Data/{GetType().Name}.txt");
            return (Part1(lines), Part2(lines));
        }

        protected abstract object Part1(string[] lines);

        protected abstract object Part2(string[] lines);
    }
}