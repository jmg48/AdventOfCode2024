namespace AdventOfCode2024;

public class Day01 : Aoc
{
    [Test]
    public void Part()
    {
        var lines = InputLines();

        var numbers = lines
            .Select(it =>
                it.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse).ToList()).ToList();
        var left = numbers.Select(it => it[0]).Order().ToList();
        var right = numbers.Select(it => it[1]).Order().ToList();
        var diffs = left.Zip(right, (i, j) => Math.Abs(i - j));
        var result = diffs.Sum();
        Console.WriteLine(result);

        var rightCounts = right.GroupBy(it => it).ToDictionary(it => it.Key, it => it.Count());

        var result2 = left.Select(it => it * rightCounts.GetValueOrDefault(it, 0)).Sum();

        Console.WriteLine(result2);
    }

}