using FluentAssertions;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day18 : Aoc
{
    [Test]
    public void Part()
    {
        var allBytes = InputLines()
            .Select(it =>
            {
                var match = Regex.Match(it, @"(\d+),(\d+)");
                return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }).ToList();

        for (var i = 0; i < allBytes.Count; i++)
        {
            var bytes = new HashSet<Coord>(allBytes.Take(i));

            var size = 70;

            var start = new Coord(0, 0);
            var endPos = new Coord(size, size);

            var visited = new Dictionary<Coord, int>();
            var unVisited = new Dictionary<Coord, int>();
            unVisited.Add(start, 0);

            while (unVisited.Count > 0)
            {
                var current = unVisited.OrderBy(it => it.Value).First();
                unVisited.Remove(current.Key);
                visited.Add(current.Key, current.Value);

                var pos = current.Key;

                foreach (var (newPos, newScore) in new[]
                         {
                         (pos.Move(Dir.N), current.Value + 1),
                         (pos.Move(Dir.S), current.Value + 1),
                         (pos.Move(Dir.E), current.Value + 1),
                         (pos.Move(Dir.W), current.Value + 1),
                     })
                {
                    if (newPos.X < 0 || newPos.X > size || newPos.Y < 0 || newPos.Y > size)
                    {
                        continue;
                    }

                    if (visited.ContainsKey(newPos) || bytes.Contains(newPos))
                    {
                        continue;
                    }

                    if (!unVisited.TryGetValue(newPos, out var existingScore) || newScore < existingScore)
                    {
                        unVisited[newPos] = newScore;
                    }
                }
            }

            if(i == 1024)
            {
                Console.WriteLine($"Part One: {visited[endPos]}");
            }

            if (!visited.ContainsKey(endPos))
            {
                Console.WriteLine($"Part Two: {allBytes.Take(i).Last()}");
                return;
            }
        }
    }
}

