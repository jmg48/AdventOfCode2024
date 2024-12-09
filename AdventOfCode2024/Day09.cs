namespace AdventOfCode2024;

public class Day09 : Aoc
{
    [Test]
    public void Part1()
    {
        var input = InputLines().Single();

        var id = 0;
        var isFile = true;
        var disk = new List<int>();
        foreach (var length in input.Select(it => it - '0'))
        {
            if (isFile)
            {
                for (var j = 0; j < length; j++)
                {
                    disk.Add(id);
                }

                id++;
            }
            else
            {
                for (var j = 0; j < length; j++)
                {
                    disk.Add(-1);
                }
            }

            isFile = !isFile;
        }

        for (var i = 0; i < disk.Count; i++)
        {
            while (disk[^1] == -1)
            {
                disk.RemoveAt(disk.Count - 1);
            }

            if (disk[i] != -1)
            {
                continue;
            }

            disk[i] = disk[^1];
            disk.RemoveAt(disk.Count - 1);
        }

        long result = 0;
        for (var i = 0; i < disk.Count; i++)
        {
            result += i * disk[i];
        }

        Console.WriteLine(result);
    }

    [Test]
    public void Part2()
    {
        var input = InputLines().Single();

        var id = 0;
        var pos = 0;
        var isFile = true;
        var disk = new LinkedList<Day9File>();
        foreach (var length in input.Select(it => it - '0'))
        {
            if (isFile)
            {
                disk.AddLast(new Day9File(id, pos, length));
                id++;
            }

            pos += length;
            isFile = !isFile;
        }

        for (var i = id - 1; i >= 0; i--)
        {
            var file = disk.Single(it => it.Id == i);
            var it = disk.First;
            while (it != disk.Last && it.Value.Pos < file.Pos)
            {
                var current = it.Value;
                var next = it.Next.Value;
                var space = next.Pos - current.Pos - current.Length;

                if (space >= file.Length)
                {
                    disk.AddAfter(it, file with { Pos = current.Pos + current.Length });
                    disk.Remove(file);
                    break;
                }

                it = it.Next;
            }
        }

        long result = 0;
        foreach (var file in disk)
        {
            for (var i = 0; i < file.Length; i++)
            {
                result += file.Id * (file.Pos + i);
            }
        }

        Console.WriteLine(result);
    }
}

public record Day9File(int Id, int Pos, int Length);