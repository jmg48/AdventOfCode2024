namespace AdventOfCode2024;

public class Day06 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToArray();

        var visited = new HashSet<Coord>();

        Coord pos = default;
        var dir = Dir.N;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var j = line.IndexOf('^');
            if (j != -1)
            {
                pos = new Coord(i, j);
                visited.Add(pos);
            }
        }

        while (true)
        {
            var nextPos = pos.Move(dir);

            if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= input.Length || nextPos.Y >= input[0].Length)
            {
                break;
            }

            var isBlocked = input[nextPos.X][nextPos.Y] == '#';
            if (isBlocked)
            {
                dir = dir switch
                {
                    Dir.N => Dir.E,
                    Dir.S => Dir.W,
                    Dir.E => Dir.S,
                    Dir.W => Dir.N
                };
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

        Coord startPos = default;
        var startDir = Dir.N;
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var j = line.IndexOf('^');
            if (j != -1)
            {
                startPos = new Coord(i, j);
            }
        }

        var loops = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
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

                    if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= input.Length || nextPos.Y >= input[0].Length)
                    {
                        break;
                    }

                    var isBlocked = input[nextPos.X][nextPos.Y] == '#' || nextPos.X == i && nextPos.Y == j;
                    if (isBlocked)
                    {
                        dir = dir switch
                        {
                            Dir.N => Dir.E,
                            Dir.S => Dir.W,
                            Dir.E => Dir.S,
                            Dir.W => Dir.N
                        };
                    }
                    else
                    {
                        pos = nextPos;
                    }

                    if (visited.Contains((pos, dir)))
                    {
                        loops++;
                        break;
                    }

                    visited.Add((pos, dir));
                }
            }
        }
            
        Console.WriteLine(loops);
    }
}