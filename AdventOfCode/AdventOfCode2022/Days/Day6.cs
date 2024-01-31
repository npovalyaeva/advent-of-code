namespace AdventOfCode2022.Days
{
	public sealed class Day6 : Day
	{
        protected override object Part1() => DetectMarkerPosition(lenght: 4);
        protected override object Part2() => DetectMarkerPosition(lenght: 14);

        private int DetectMarkerPosition(int lenght)
        {
            var line = lines.First();
            // https://stackoverflow.com/questions/70594619/how-can-i-split-string-into-array-of-string-that-take-two-characters-with-includ
            var test = line.Select((x, i) => new { Value = x, Index = i })
                .GroupBy(_ => line.Substring(_.Index, (line.Length - _.Index) < lenght ? (line.Length - _.Index) : lenght));
                //.GroupBy(_ => _.Key.Distinct());
                //.Select(_ => (_.Key.Distinct()));
                //.First(_ => _.Length == lenght);



            List<char> stack = new();
            for (int i = 0; i <= line.Length; i++)
            {
                if (stack.Contains(line[i]))
                {
                    stack.RemoveRange(0, stack.IndexOf(line[i]) + 1);
                }
                stack.Add(line[i]);
                if (stack.Count == 14)
                {
                    return (i + 1);
                }

            }
            return 0;
        }
    }
}

