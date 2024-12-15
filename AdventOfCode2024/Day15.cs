using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using FluentAssertions;

namespace AdventOfCode2024;

public class Day15 : Aoc
{
    public void Part1()
    {
        var input = InputLines().ToList();

        var map = new List<List<char>>();
        var route = new List<Dir>();
        Coord startPos = default;

        var flag = false;
        foreach (var line in input)
        {
            if (!flag)
            {
                if (string.IsNullOrEmpty(line))
                {
                    flag = true;
                    continue;
                }

                if (line.Contains('@'))
                {
                    startPos = new Coord(map.Count, line.IndexOf('@'));
                }

                map.Add(line.ToList());
            }
            else
            {
                route.AddRange(line.Select(it => it switch
                {
                    '^' => Dir.N,
                    'v' => Dir.S,
                    '>' => Dir.E,
                    '<' => Dir.W,
                }));
            }
        }

        var pos = startPos;
        foreach (var dir in route)
        {
            var checkPos = pos;
            while (checkPos.IsInBoundsOf(map) && new[]{ '@', 'O'}.Contains( map[checkPos.X][checkPos.Y]))
            {
                checkPos = checkPos.Move(dir);
            }

            if (map[checkPos.X][checkPos.Y] == '#')
            {
                continue;
            }

            var unDir = dir.TurnBack();
            while (checkPos != pos)
            {
                var backPos = checkPos.Move(unDir);
                map[checkPos.X][checkPos.Y] = map[backPos.X][backPos.Y];
                checkPos = backPos;
            }

            map[checkPos.X][checkPos.Y] = '.';

            pos = pos.Move(dir);
        }

        var result = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == 'O')
                {
                    result += 100 * i + j;
                }
            }
        }

        result.Should().BeGreaterThan(1552497);

        Console.WriteLine(result);
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToList();

        //input = new List<string>
        //{
        //    "##########",
        //    "#..O..O.O#",
        //    "#......O.#",
        //    "#.OO..O.O#",
        //    "#..O@..O.#",
        //    "#O#..O...#",
        //    "#O..O..O.#",
        //    "#.OO.O.OO#",
        //    "#....O...#",
        //    "##########",
        //    "",
        //    "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
        //    "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
        //    "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
        //    "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
        //    "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
        //    "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
        //    ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
        //    "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
        //    "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
        //    "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
        //};

        //input = new List<string>
        //{
        //    "#######",
        //    "#...#.#",
        //    "#.....#",
        //    "#..OO@#",
        //    "#..O..#",
        //    "#.....#",
        //    "#######",
        //    "",
        //    "<vv<<^^<<^^",
        //};

        var map = new List<List<char>>();
        var route = new List<Dir>();
        Coord startPos = default;

        var flag = false;
        foreach (var line in input)
        {
            if (!flag)
            {
                if (string.IsNullOrEmpty(line))
                {
                    flag = true;
                    continue;
                }

                var chars = line.SelectMany(it  => it switch
                {
                    '#' => new[] { '#', '#' },
                    'O' => new[] { '[', ']' },
                    '.' => new[] { '.', '.' },
                    '@' => new[] { '@', '.' },
                }).ToList();

                if (chars.Contains('@'))
                {
                    startPos = new Coord(map.Count, chars.IndexOf('@'));
                }

                map.Add(chars);
            }
            else
            {
                route.AddRange(line.Select(it => it switch
                {
                    '^' => Dir.N,
                    'v' => Dir.S,
                    '>' => Dir.E,
                    '<' => Dir.W,
                }));
            }
        }

        void Swap(Coord a, Coord b)
        {
            (map[a.X][a.Y], map[b.X][b.Y]) = (map[b.X][b.Y], map[a.X][a.Y]);
        }

        bool TryMove(Coord pos, Dir dir)
        {
            switch (dir)
            {
                case Dir.E:
                case Dir.W:
                    var checkPos = pos;
                    while (checkPos.IsInBoundsOf(map) && new[] { '@', '[',']' }.Contains(map[checkPos.X][checkPos.Y]))
                    {
                        checkPos = checkPos.Move(dir);
                    }

                    if (map[checkPos.X][checkPos.Y] == '#')
                    {
                        return false;
                    }

                    var unDir = dir.TurnBack();
                    while (checkPos != pos)
                    {
                        var backPos = checkPos.Move(unDir);
                        Swap(checkPos, backPos);
                        checkPos = backPos;
                    }

                    return true;
            }

            var toMove = new List<Coord> { pos };
            for (int i = 0; i < toMove.Count; i++)
            {
                var toCheck = toMove[i];
                var moved = toCheck.Move(dir);
                switch (map[moved.X][moved.Y])
                {
                    case '#':
                        return false;
                    case '.':
                        break;
                    case '[':
                        toMove.Add(moved);
                        toMove.Add(moved.Move(Dir.E));
                        break;
                    case ']':
                        toMove.Add(moved);
                        toMove.Add(moved.Move(Dir.W));
                        break;
                }
            }

            var distinct = new HashSet<Coord>();
            for (int i = toMove.Count - 1; i >= 0; i--)
            {
                if (distinct.Add(toMove[i]))
                {
                    Swap(toMove[i], toMove[i].Move(dir));
                }
            }
        
            return true;
        }

        var pos = startPos;
        foreach (var dir in route)
        {
            if (TryMove(pos, dir))
            {
                pos = pos.Move(dir);
            }

            //for (int i = 0; i < map.Count; i++)
            //{
            //    for (int j = 0; j < map[i].Count; j++)
            //    {
            //        Console.Write(map[i][j]);
            //    }

            //    Console.WriteLine();
            //}

            //Console.WriteLine();
        }

        var result = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '[')
                {
                    result += 100 * i + j;
                }
            }
        }

        Console.WriteLine(result);
    }
}

