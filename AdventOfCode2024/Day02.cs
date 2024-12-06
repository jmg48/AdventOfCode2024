namespace AdventOfCode2024;

public class Day02 : Aoc
{
    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var lines = InputLines()
            .Select(it => it.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList())
            .ToList();

        var result = 0;
        foreach (var line in lines)
        {
            {
                if (IsSafe(line))
                {
                    result++;
                    continue;
                }
            }

            if (part == 2)
            {
                for (var j = 0; j < line.Count; j++)
                {
                    var skipped = line.Take(j).Concat(line.Skip(j + 1)).ToList();
                    if (IsSafe(skipped))
                    {
                        result++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(result);
        return;

        bool IsSafe(List<int> line)
        {
            var isIncreasing = default(bool?);
            for (var i = 1; i < line.Count; i++)
            {
                var a = line[i - 1];
                var b = line[i];
                if (a == b || a - b > 3 || b - a > 3)
                {
                    return false;
                }

                if (!isIncreasing.HasValue)
                {
                    isIncreasing = b > a;
                }
                else if (isIncreasing != b > a)
                {
                    return false;
                }
            }

            return true;
        }
    }
}