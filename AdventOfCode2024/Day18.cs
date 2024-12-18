using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day18 : Aoc
{
    [Test]
    public void Part()
    {
        var size = 70;
        var start = new Coord(0, 0);
        var endPos = new Coord(size, size);

        var allBytes = InputLines()
            .Select(it =>
            {
                var match = Regex.Match(it, @"(\d+),(\d+)");
                return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }).ToList();

        Console.WriteLine($"Part One: {PathLength(1024)}");

        var increment = 1;
        while (increment * 2 < allBytes.Count)
        {
            increment *= 2;
        }

        var i = increment;
        while (increment > 1)
        {
            var isBlocked = PathLength(i) == -1;
            increment /= 2;
            i += isBlocked ? -increment : increment;
        }

        Console.WriteLine($"Part Two: {allBytes[i - 1]}");

        return;

        int PathLength(int i)
        {
            var bytes = new HashSet<Coord>(allBytes.Take(i));
            var visited = new Dictionary<Coord, int>();
            var unVisited = new Dictionary<Coord, int> { { start, 0 } };
            while (unVisited.Count > 0)
            {
                var (pos, score) = unVisited.OrderBy(it => it.Value).First();
                unVisited.Remove(pos);
                visited.Add(pos, score);

                foreach (var newPos in new[] { Dir.N, Dir.S, Dir.E, Dir.W }.Select(it => pos.Move(it)))
                {
                    if (newPos.X < 0 || newPos.X > size || newPos.Y < 0 || newPos.Y > size
                        || visited.ContainsKey(newPos) || bytes.Contains(newPos))
                    {
                        continue;
                    }

                    if (!unVisited.TryGetValue(newPos, out var existingScore) || score + 1 < existingScore)
                    {
                        unVisited[newPos] = score + 1;
                    }
                }
            }

            return visited.GetValueOrDefault(endPos, -1);
        }
    }
}

