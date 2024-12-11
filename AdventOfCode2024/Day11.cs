namespace AdventOfCode2024;

public class Day11 : Aoc
{
    [Test]
    public void Part1()
    {
        var inputLines = InputLines().Single().Split(' ').Select(long.Parse).ToList();

        var seq = new LinkedList<long>(inputLines);

        for (var i = 0; i < 25; i++)
        {
            var it = seq.First;
            while (it != null)
            {
                var val = it.Value;
                if (val == 0)
                {
                    it.Value = 1;
                }
                else
                {
                    var s = $"{it.Value}";
                    if (s.Length % 2 == 0)
                    {
                        var left = long.Parse(s[..(s.Length / 2)]);
                        var right = long.Parse(s[(s.Length / 2)..]);
                        seq.AddBefore(it, left);
                        it.Value = right;
                    }
                    else
                    {
                        it.Value *= 2024;
                    }
                }

                it = it.Next;
            }
        }

        Console.WriteLine(seq.Count);
    }

    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var inputLines = InputLines().Single().Split(' ').Select(long.Parse).ToList();

        var seq = inputLines.ToDictionary(it => it, _ => 1L);

        for (var i = 0; i < part switch { 1 => 25, 2 => 75 }; i++)
        {
            var nextSeq = new Dictionary<long, long>();
            foreach (var (val, count) in seq)
            {
                if (val == 0)
                {
                    Add(1, count);
                }
                else
                {
                    var s = $"{val}";
                    if (s.Length % 2 == 0)
                    {
                        var left = long.Parse(s[..(s.Length / 2)]);
                        var right = long.Parse(s[(s.Length / 2)..]);
                        Add(left, count);
                        Add(right, count);
                    }
                    else
                    {
                        Add(val * 2024, count);
                    }
                }
            }

            seq = nextSeq;
            continue;

            void Add(long value, long count) => nextSeq[value] = count + nextSeq.GetValueOrDefault(value, 0);
        }

        Console.WriteLine(seq.Values.Sum());
    }
}

