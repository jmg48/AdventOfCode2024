namespace AdventOfCode2024;

public class Day12 : Aoc
{
    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var input = InputLines().ToList();

        var visited = new HashSet<Coord>();
        var result = 0;

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                var pos = new Coord(i, j);
                var (area, perimeter) = Visit(pos);

                if (part == 2)
                {
                    foreach (var (edge, dir) in perimeter.ToList())
                    {
                        var neighbour = (edge.Move(dir.TurnLeft()), dir);
                        perimeter.Remove(neighbour);
                    }
                }

                result += area.Count * perimeter.Count;
            }
        }

        Console.WriteLine(result);

        return;
        
        (HashSet<Coord>, HashSet<(Coord, Dir)>) Visit(Coord pos)
        {
            if (!visited.Add(pos))
            {
                return ([], []);
            }

            var species = input[pos.X][pos.Y];

            var area = new HashSet<Coord>{pos};
            var perimeter = new HashSet<(Coord, Dir)>();
            foreach (var dir in new[] { Dir.N, Dir.S, Dir.E, Dir.W })
            {
                var newPos = pos.Move(dir);
                if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= input.Count || newPos.Y >= input[0].Length)
                {
                    perimeter.Add((pos, dir));
                    continue;
                }

                var newSpecies = input[newPos.X][newPos.Y];
                if (species != newSpecies)
                {
                    perimeter.Add((pos, dir));
                    continue;
                }

                var (subArea, subPerimeter) = Visit(newPos);
                area.UnionWith(subArea);
                perimeter.UnionWith(subPerimeter);
            }

            return (area, perimeter);
        }
    }
}

