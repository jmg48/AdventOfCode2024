namespace AdventOfCode2024;

public class Day06 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToArray();

        var visited = new HashSet<Coord>();

        var dir = Dir.N;
        var pos = Enumerable.Range(0, input.Length)
            .Where(i => input[i].Contains('^'))
            .Select(i => new Coord(i, input[i].IndexOf('^')))
            .Single();

        while (true)
        {
            var nextPos = pos.Move(dir);

            if (!nextPos.IsInBoundsOf(input))
            {
                break;
            }

            var isBlocked = input[nextPos.X][nextPos.Y] == '#';
            if (isBlocked)
            {
                dir = dir.TurnRight();
            }
            else
            {
                pos = nextPos;
                visited.Add(pos);
            }
        }

        Console.WriteLine(visited.Count);
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToArray();

        var startDir = Dir.N;
        var startPos =  Enumerable.Range(0, input.Length)
            .Where(i => input[i].Contains('^'))
            .Select(i => new Coord(i, input[i].IndexOf('^')))
            .Single();

        var loops = 0;

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (new Coord(i, j) == startPos)
                {
                    continue;
                }

                var visited = new HashSet<(Coord, Dir)>();
                var pos = startPos;
                var dir = startDir;
                visited.Add((pos, dir));
                while (true)
                {
                    var nextPos = dir switch
                    {
                        Dir.N => new Coord(pos.X - 1, pos.Y),
                        Dir.S => new Coord(pos.X + 1, pos.Y),
                        Dir.E => new Coord(pos.X, pos.Y + 1),
                        Dir.W => new Coord(pos.X, pos.Y - 1)
                    };

                    if (!nextPos.IsInBoundsOf(input))
                    {
                        break;
                    }

                    var isBlocked = input[nextPos.X][nextPos.Y] == '#' || nextPos.X == i && nextPos.Y == j;
                    if (isBlocked)
                    {
                        dir = dir.TurnRight();
                    }
                    else
                    {
                        pos = nextPos;
                    }

                    if (!visited.Add((pos, dir)))
                    {
                        loops++;
                        break;
                    }
                }
            }
        }
            
        Console.WriteLine(loops);
    }
}