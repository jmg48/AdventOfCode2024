namespace AdventOfCode2024;

public class Day16 : Aoc
{
    [Test]
    public void Part()
    {
        var input = InputLines().ToList();

        var start = (new Coord(input.Count - 2, 1), Dir.E);
        var endPos = (new Coord(1, input[0].Length - 2));

        var visited = new Dictionary<(Coord, Dir), int>();
        var unVisited = new Dictionary<(Coord, Dir), int>();
        unVisited.Add(start, 0);

        while (unVisited.Count > 0)
        {
            var current = unVisited.OrderBy(it => it.Value).First();
            unVisited.Remove(current.Key);
            visited.Add(current.Key, current.Value);

            var (pos, dir) = current.Key;

            var move = pos.Move(dir);
            var left = dir.TurnLeft();
            var right = dir.TurnRight();

            foreach (var (newPos, newDir, newScore) in new[]
                     {
                         (move, dir, current.Value + 1),
                         (pos, left, current.Value + 1000),
                         (pos, right, current.Value + 1000),
                     })
            {
                if (input[newPos.X][newPos.Y] == '#' || visited.ContainsKey((newPos, newDir)))
                {
                    continue;
                }

                if (!unVisited.TryGetValue((newPos, newDir), out var existingScore) || newScore < existingScore)
                {
                    unVisited[(newPos, newDir)] = newScore;
                }
            }
        }
        
        var endDir = new[] { Dir.N, Dir.S, Dir.E, Dir.W }
            .Select(dir => (dir, score: visited[(endPos, dir)]))
            .OrderBy(it => it.score)
            .Select(it => it.dir)
            .First();

        Console.WriteLine(visited[(endPos, endDir)]);

        var shortestPath = new List<(Coord, Dir)> { (endPos, endDir) };
        for (var i = 0; i < shortestPath.Count; i++)
        {
            var (pos, dir) = shortestPath[i];
            var score = visited[(pos, dir)];

            var move = pos.Move(dir.TurnBack());
            var left = dir.TurnLeft();
            var right = dir.TurnRight();

            foreach (var (newPos, newDir, newScore) in new[]
                     {
                         (move, dir, score - 1),
                         (pos, left, score - 1000),
                         (pos, right, score - 1000),
                     })
            {
                if (visited.TryGetValue((newPos, newDir), out var existing) && existing == newScore &&
                    !shortestPath.Contains((newPos, newDir)))
                {
                    shortestPath.Add((newPos, newDir));
                }
            }
        }

        Console.WriteLine(shortestPath.Select(it => it.Item1).Distinct().Count());
    }
}

