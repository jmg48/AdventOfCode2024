namespace AdventOfCode2024;

public class Day08 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToArray();

        var antinodes = new HashSet<Coord>();
        foreach (var frequency in "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var antennas = new List<Coord>();
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == frequency)
                    {
                        antennas.Add(new Coord(i, j));
                    }
                }
            }

            for (var i = 0; i < antennas.Count; i++)
            {
                for (var j = 0; j < antennas.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var a = antennas[i];
                    var b = antennas[j];
                    var antinode = new Coord(b.X + b.X - a.X, b.Y + b.Y - a.Y);
                    if (antinode.IsInBoundsOf(input))
                    {
                        antinodes.Add(antinode);
                    }
                }
            }
        }

        Console.WriteLine(antinodes.Count);
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToArray();

        var antinodes = new HashSet<Coord>();
        foreach (var frequency in "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var antennas = new List<Coord>();
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == frequency)
                    {
                        antennas.Add(new Coord(i, j));
                    }
                }
            }

            for (var i = 0; i < antennas.Count; i++)
            {
                for (var j = i + 1; j < antennas.Count; j++)
                {
                    var a = antennas[i];
                    var b = antennas[j];
                    
                    var dX = (b.X - a.X);
                    var dY = (b.Y - a.Y);

                    var antinode = a;
                    while (antinode.IsInBoundsOf(input))
                    {
                        antinodes.Add(antinode);
                        antinode = new Coord(antinode.X + dX, antinode.Y + dY);
                    }

                    antinode = a;
                    while (antinode.IsInBoundsOf(input))
                    {
                        antinodes.Add(antinode);
                        antinode = new Coord(antinode.X - dX, antinode.Y - dY);
                    }
                }
            }
        }

        Console.WriteLine(antinodes.Count);
    }
}