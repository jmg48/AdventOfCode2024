using FluentAssertions;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day19 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToList();
        var designs = new HashSet<string>(input[0].Split(", "));
        var patterns = input.Skip(2).ToList();
        var maxLength = designs.Max(it => it.Length);

        var possible = new HashSet<string>(designs);
        var impossible = new HashSet<string>();

        Console.WriteLine(patterns.Count(IsPossible));
        return;

        bool IsPossible(string pattern)
        {
            if (designs.Contains(pattern) || possible.Contains(pattern))
            {
                return true;
            }

            if (impossible.Contains(pattern))
            {
                return false;
            }

            for (var i = 1; i <= maxLength && i <= pattern.Length; i++)
            {
                var prefix = pattern.Substring(0, i);
                if (designs.Contains(prefix))
                {
                    if (IsPossible(pattern.Substring(i)))
                    {
                        possible.Add(pattern);
                        return true;
                    }
                }
            }

            impossible.Add(pattern);
            return false;
        }
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToList();
        var designs = new HashSet<string>(input[0].Split(", "));
        var patterns = input.Skip(2).ToList();
        var maxLength = designs.Max(it => it.Length);

        var memo = new Dictionary<string, long>();

        Console.WriteLine(patterns.Sum(IsPossible));
        return;

        long IsPossible(string pattern)
        {
            if (pattern.Length == 0)
            {
                return 1;
            }

            if (memo.TryGetValue(pattern, out var existing))
            {
                return existing;
            }

            var ways = 0L;
            for (var i = 1; i <= maxLength && i <= pattern.Length; i++)
            {
                var prefix = pattern.Substring(0, i);
                if (designs.Contains(prefix))
                {
                    ways += IsPossible(pattern.Substring(i));
                }
            }

            memo.Add(pattern, ways);
            return ways;
        }
    }
}

