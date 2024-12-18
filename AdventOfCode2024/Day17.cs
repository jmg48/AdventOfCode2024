using System.Runtime.InteropServices;

namespace AdventOfCode2024;

public class Day17 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().ToList();

        var a = 30899381; // 729;
        var b = 0;
        var c = 0;

        var program = new List<int> { 0, 1, 5, 4, 3, 0 };
        program = new List<int> { 2, 4, 1, 1, 7, 5, 4, 0, 0, 3, 1, 6, 5, 5, 3, 0 };

        for (int i = 0; i < program.Count; i += 2)
        {
            var op = program[i];
            var arg = program[i + 1];
            switch (op)
            {
                case 0:
                    a = a / (1 << Combo(arg));
                    break;
                case 1:
                    b = b ^ arg;
                    break;
                case 2:
                    b = Combo(arg) % 8;
                    break;
                case 4:
                    b = b ^ c;
                    break;
                case 5:
                    Console.Write($"{Combo(arg) % 8},");
                    break;
                case 3:
                    if (a != 0)
                    {
                        i = arg - 2;
                    }

                    break;
                case 7:
                    c = a / (1 << Combo(arg));
                    break;
                default:
                    throw new NotSupportedException($"opCode {op} is out of range");
            }
        }

        int Combo(int arg) =>
            arg switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
            };
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().ToList();

        long b = 0;
        long c = 0;

        var program = new List<int> { 0, 3, 5, 4, 3, 0 };
        program = new List<int> { 2, 4, 1, 1, 7, 5, 4, 0, 0, 3, 1, 6, 5, 5, 3, 0 };

        var searches = new Queue<(int Order, long Start, long Step, long Limit)>([
            (15, 1L << (3 * 15), 1L << (3 * 15), (1L << (3 * 15)) * 8)
        ]);

        while (searches.Count > 0)
        {
            var (order, start, step, limit) = searches.Dequeue();

            for (long result = start; result < limit; result += step)
            {
                long a = result;
                var output = new List<int>();
                var isCopy = true;
                for (int i = 0; isCopy && i < program.Count; i += 2)
                {
                    var op = program[i];
                    var arg = program[i + 1];
                    switch (op)
                    {
                        case 0:
                            a = a / (1 << (int)Combo(arg));
                            break;
                        case 1:
                            b = b ^ arg;
                            break;
                        case 2:
                            b = Combo(arg) % 8;
                            break;
                        case 3:
                            if (a != 0)
                            {
                                i = arg - 2;
                            }

                            break;
                        case 4:
                            b = b ^ c;
                            break;
                        case 5:
                            var outVal = Combo(arg) % 8;
                            output.Add((int)outVal);
                            break;
                        case 6:
                            b = a / (1 << (int)Combo(arg));
                            break;
                        case 7:
                            c = a / (1 << (int)Combo(arg));
                            break;
                        default:
                            throw new NotSupportedException($"opCode {op} is out of range");
                    }

                    continue;

                    long Combo(int arg) =>
                        arg switch
                        {
                            0 => 0,
                            1 => 1,
                            2 => 2,
                            3 => 3,
                            4 => a,
                            5 => b,
                            6 => c,
                        };
                }

                if (output.Count == program.Count &&
                    output.Skip(order).Zip(program.Skip(order), (a, b) => a == b).All(it => it))
                {
                    Console.WriteLine($"{order} : {result,-12} : {output.Count} : {string.Join(',', output)}");
                    searches.Enqueue((order - 1, result, step / 8, result + step));

                    if (order == 0)
                    {
                        return;
                    }
                }
            }
        }
    }
}

