namespace AdventOfCode2022
{
    public abstract class AdventOfCode
    {
        public (object, object) Run()
        {
            // to do: update the path to hide the external part
            var lines = File.ReadAllLines($"/Users/nadyapovalyaeva/Desktop/{GetType().Name}.txt");
            return (Part1(lines), Part2(lines));
        }

        protected abstract object Part1(string[] lines);

        protected abstract object Part2(string[] lines);
    }
}