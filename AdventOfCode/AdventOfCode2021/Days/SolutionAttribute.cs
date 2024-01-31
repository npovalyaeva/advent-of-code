namespace AdventOfCode2021.Days
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SolutionAttribute : Attribute
    {
        public required string Puzzle { get; set; }
    }
}