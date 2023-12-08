namespace AdventOfCode2023.Days
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SolutionAttribute : Attribute
    {
        public required string Puzzle { get; set; }
    }
}