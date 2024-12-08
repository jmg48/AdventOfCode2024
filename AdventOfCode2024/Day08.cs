using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

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
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == frequency)
                    {
                        antennas.Add(new Coord(i, j));
                    }
                }
            }

            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = 0; j < antennas.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var a = antennas[i];
                    var b = antennas[j];
                    var dX = (b.X - a.X);
                    var dY = (b.Y - a.Y);
                    var antinode = new Coord(b.X + dX, b.Y + dY);

                    if (antinode.X >= 0 && antinode.Y >= 0 && antinode.X < input.Length && antinode.Y < input[0].Length)
                    {
                        Console.WriteLine($"{frequency} | {a} | {b} | {antinode}");
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
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == frequency)
                    {
                        antennas.Add(new Coord(i, j));
                    }
                }
            }

            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    var a = antennas[i];
                    var b = antennas[j];
                    
                    var dX = (b.X - a.X);
                    var dY = (b.Y - a.Y);

                    //foreach (var prime in new[]{2,3,5,7,11,13,17,19,23,29,31,37,41,43,47})
                    //{
                    //    if (dX % prime == 0 && dY % prime == 0)
                    //    {
                    //        dX /= prime;
                    //        dY /= prime;
                    //    }
                    //}

                    var antinode = a;
                    while (antinode.X >= 0 && antinode.Y >= 0 && antinode.X < input.Length && antinode.Y < input[0].Length)
                    {
                        antinodes.Add(antinode);
                        antinode = new Coord(antinode.X + dX, antinode.Y + dY);
                    }

                    antinode = a;
                    while (antinode.X >= 0 && antinode.Y >= 0 && antinode.X < input.Length && antinode.Y < input[0].Length)
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