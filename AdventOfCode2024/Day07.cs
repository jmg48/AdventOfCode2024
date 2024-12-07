using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2024;

public class Day07 : Aoc
{
    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var input = InputLines().ToArray();

        long result = 0;

        foreach (var line in input)
        {
            var split1 = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var target = long.Parse(split1[0]);
            var values = split1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();

            foreach (var total in Calc(values))
            {
                if (total == target)
                {
                    result += total;
                    break;
                }
            }
        }

        Console.WriteLine(result);
        return;

        IEnumerable<long> Calc(List<long> values)
        {
            if (values.Count == 1)
            {
                yield return values[^1];
                yield break;
            }

            var subValues = values.Take(values.Count - 1).ToList();
            foreach (var total in Calc(subValues))
            {
                yield return total + values[^1];
                yield return total * values[^1];
                if (part == 2)
                {
                    yield return long.Parse($"{total}{values[^1]}");
                }
            }
        }
    }
}