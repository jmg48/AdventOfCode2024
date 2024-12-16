using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day14 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToList();

        var height = 103;
        var width = 101;

        var robots = new List<(Coord Pos, Coord Velocity)>();
        foreach (var line in input)
        {
            var match = Regex.Match(line, @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
            robots.Add((new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), new Coord(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
        }

        for (var i = 0; i < 100; i++)
        {
            for (var j = 0; j < robots.Count; j++)
            {
                var (pos, v) = robots[j];
                var newPos = new Coord(((pos.X + v.X) % width + width) % width, ((pos.Y + v.Y) % height + height) % height);
                robots[j] = (newPos, v);
            }
        }

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var count = robots.Count(it => it.Pos.X == j && it.Pos.Y == i);
                Console.Write(count == 0 ? "." : $"{count}");
            }

            Console.WriteLine();
        }

        var (a, b, c, d) = (0, 0, 0, 0);
        for (var i = 0; i < robots.Count; i++)
        {
            var (pos, _) = robots[i];
            if (pos.X < width / 2)
            {
                if (pos.Y < height / 2)
                {
                    a++;
                }
                else if (pos.Y > height / 2)
                {
                    b++;
                }
            }
            else if (pos.X > width / 2)
            {
                if (pos.Y < height / 2)
                {
                    c++;
                }
                else if (pos.Y > height / 2)
                {
                    d++;
                }
            }
        }

        var result = a * b * c * d;

        Console.WriteLine(result);
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToList();

        var height = 103;
        var width = 101;

        var robots = new List<(Coord Pos, Coord Velocity)>();
        foreach (var line in input)
        {
            var match = Regex.Match(line, @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
            robots.Add((new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), new Coord(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
        }

        var lowestVar = double.MaxValue;
        for (var i = 0; i < height * width; i++)
        {
            for (var j = 0; j < robots.Count; j++)
            {
                var (pos, v) = robots[j];
                var newPos = new Coord(((pos.X + v.X) % width + width) % width, ((pos.Y + v.Y) % height + height) % height);
                robots[j] = (newPos, v);
            }

            var averageX = robots.Select(it => (double)it.Pos.X).Average();
            var varX = robots.Select(it => Math.Pow(it.Pos.X - averageX, 2)).Sum();
            var averageY = robots.Select(it => (double)it.Pos.Y).Average();
            var varY = robots.Select(it => Math.Pow(it.Pos.Y - averageY, 2)).Sum();
            if (varX * varY < lowestVar)
            {
                lowestVar = varX * varY;
                Console.WriteLine(i);
                PrintGrid();
            }
        }

        return;

        void PrintGrid()
        {
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var count = robots.Count(it => it.Pos.X == j && it.Pos.Y == i);
                    Console.Write(count == 0 ? "." : $"{count}");
                }

                Console.WriteLine();
            }
        }
    }
}

