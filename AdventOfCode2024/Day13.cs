using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day13 : Aoc
{
    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var offset = part switch { 1 => 0, 2 => 10000000000000 };

        var input = InputLines().ToList();

        var games = new List<(Coord A, Coord B, LongCoord P)>();
        for (var i = 0; i < input.Count; i+=4)
        {
            var matchA = Regex.Match(input[i], @"Button A: X\+(\d+), Y\+(\d+)");
            var a = new Coord(int.Parse(matchA.Groups[1].Value), int.Parse(matchA.Groups[2].Value));
            var matchB = Regex.Match(input[i + 1], @"Button B: X\+(\d+), Y\+(\d+)");
            var b = new Coord(int.Parse(matchB.Groups[1].Value), int.Parse(matchB.Groups[2].Value));
            var matchP = Regex.Match(input[i + 2], @"Prize: X=(\d+), Y=(\d+)");
            var p = new LongCoord(long.Parse(matchP.Groups[1].Value) + offset, long.Parse(matchP.Groups[2].Value) + offset);
            games.Add((a, b, p));
        }

        var result = 0L;
        foreach (var (a,b,p) in games)
        {
            var rhs = (double)p.X / b.X - (double)p.Y / b.Y;
            var lhs = (double)a.X / b.X - (double)a.Y / b.Y;
            var n = rhs / lhs;

            var m = (double)p.X / b.X - n * a.X / b.X;

            var iN = (long)Math.Round(n);
            var iM = (long)Math.Round(m);

            if (iN * a.X + iM * b.X == p.X && iN * a.Y + iM * b.Y == p.Y)
            {
                result += 3 * iN + iM;
            }
        }

        Console.WriteLine(result);
    }
}

