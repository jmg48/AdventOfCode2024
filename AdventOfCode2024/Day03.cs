using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day03 : Aoc
{
    [TestCase(1)]
    [TestCase(2)]
    public void Part(int part)
    {
        var input = InputText();

        var result = 0;
        var enabled = true;

        char It(int i)
        {
            return i < input.Length ? input[i] : ' ';
        }

        while (input.Length > 0)
        {
            if (input.StartsWith("do()"))
            {
                enabled = true;
            }
            else if (input.StartsWith("don't()"))
            {
                enabled = false;
            }
            else if (enabled || part == 1)
            {
                var match = Regex.Match(input, @"^mul\((\d+),(\d+)\)");
                if (match.Success)
                {
                    var a = int.Parse(match.Groups[1].Value);
                    var b = int.Parse(match.Groups[2].Value);
                    result += a * b;
                }
            }

            input = input[1..];
        }

        Console.WriteLine(result);
    }
}