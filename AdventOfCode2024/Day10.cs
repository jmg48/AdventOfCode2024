namespace AdventOfCode2024;

public class Day10 : Aoc
{
    [Test]
    public void Part1()
    {
        var inputLines = InputLines().ToList();

        var input = inputLines.Select(it => it.Select(it => it - '0').ToList()).ToList();

        var result = 0;
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[0].Count; j++)
            {
                if (input[i][j] == 0)
                {
                    result += Score(new Coord(i, j), 0).Count;
                }
            }
        }

        Console.WriteLine(result);
        return;

        HashSet<Coord> Score(Coord pos, int height)
        {
            if (height == 9)
            {
                return [pos];
            }

            var score = new HashSet<Coord>();
            foreach (var dir in new[] { Dir.N, Dir.S, Dir.E, Dir.W })
            {
                var newPos = pos.Move(dir);
                if (!newPos.IsInBoundsOf(input))
                {
                    continue;
                }

                if (input[newPos.X][newPos.Y] == height + 1)
                {
                    score.UnionWith(Score(newPos, height + 1));
                }
            }

            return score;
        }
    }

    [Test]
    public void Part2()
    {
        var inputLines = InputLines().ToList();

        var input = inputLines.Select(it => it.Select(it => it - '0').ToList()).ToList();

        var result = 0;
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[0].Count; j++)
            {
                if (input[i][j] == 0)
                {
                    result += Score(new Coord(i, j), 0);
                }
            }
        }

        Console.WriteLine(result);
        return;

        int Score(Coord pos, int height)
        {
            if (height == 9)
            {
                return 1;
            }

            var score = 0;
            foreach (var dir in new[] { Dir.N, Dir.S, Dir.E, Dir.W })
            {
                var newPos = pos.Move(dir);
                if (newPos.X >= 0 && newPos.Y >= 0 && newPos.X < input.Count && newPos.Y < input[0].Count)
                {
                    if (input[newPos.X][newPos.Y] == height + 1)
                    {
                        score += Score(newPos, height + 1);
                    }
                }
            }

            return score;
        }
    }
}

