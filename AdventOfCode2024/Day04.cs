namespace AdventOfCode2024;

public class Day04 : Aoc
{
    [Test]
    public void Part()
    {
        var input = InputLines().ToArray();
            
        var result = 0;

        char It(int x, int y)
        {
            if (x < input.Length && y < input.Length)
            {
                return input[x][y];
            }

            return '.';
        }

        foreach (var word in new[] { "XMAS", "SAMX" })
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (It(i, j) == word[0])
                    {
                        if (It(i + 1, j) == word[1] && It(i + 2, j) == word[2] && It(i + 3, j) == word[3])
                        {
                            result++;
                        }

                        if (It(i, j + 1) == word[1] && It(i, j + 2) == word[2] && It(i, j + 3) == word[3])
                        {
                            result++;
                        }

                        if (It(i + 1, j + 1) == word[1] && It(i + 2, j + 2) == word[2] &&
                            It(i + 3, j + 3) == word[3])
                        {
                            result++;
                        }
                    }

                    if (It(i + 3, j) == word[0] && It(i + 2, j + 1) == word[1] && It(i + 1, j + 2) == word[2] &&
                        It(i, j + 3) == word[3])
                    {
                        result++;
                    }

                }
            }
        }

        Console.WriteLine(result);

        var result2 = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                if (It(i + 1, j + 1) == 'A')
                {
                    if (It(i, j) == 'M' && It(i + 2, j + 2) == 'S' || It(i, j) == 'S' && It(i + 2, j + 2) == 'M')
                    {
                        if (It(i+2, j) == 'M' && It(i, j + 2) == 'S' || It(i+2, j) == 'S' && It(i, j + 2) == 'M')
                        {
                            result2++;
                        }
                    }
                }
            }
        }

        Console.WriteLine(result2);
    }
}