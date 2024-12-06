namespace AdventOfCode2024;

public class Day05 : Aoc
{

    [Test]
    public void Part()
    {
        var input = InputLines();

        var orderRules = input
            .Where(it => it.Contains("|"))
            .Select(it => it.Split('|'))
            .GroupBy(it => it[0])
            .ToDictionary(it => it.Key, it => it.Select(it => it[1]).ToList());
        var updates = input.Where(it => it.Contains(",")).ToList();

        var result = 0;
        var invalid = new List<string[]>();
        foreach (var update in updates)
        {
            var valid = true;
            var pages = update.Split(',');
            for (int i = 0; i < pages.Length; i++)
            {
                var firstPage = pages[i];
                for (int j = i + 1; j < pages.Length; j++)
                {
                    var secondPage = pages[j];
                    if (orderRules.TryGetValue(secondPage, out var rule))
                    {
                        if(rule.Contains(firstPage))
                        {
                            valid = false;
                        }
                    }
                }
            }

            var middle = pages.Length / 2;
            if (valid)
            {
                result+= int.Parse( pages[middle]);
            }
            else
            {
                invalid.Add(update.Split(','));
            }
        }

        Console.WriteLine(result);

        foreach (var update in invalid)
        {
            var valid = false;
            while (!valid)
            {
                valid = true;
                for (int i = 0; valid && i < update.Length; i++)
                {
                    var firstPage = update[i];
                    for (int j = i + 1; valid && j < update.Length; j++)
                    {
                        var secondPage = update[j];
                        if (orderRules.TryGetValue(secondPage, out var rule))
                        {
                            if (rule.Contains(firstPage))
                            {
                                update[j] = firstPage;
                                update[i] = secondPage;
                                valid = false;
                            }
                        }
                    }
                }
            }
        }

        var result2 = 0;
        foreach (var update in invalid)
        {
            var middle = update.Length / 2;
            result2 += int.Parse(update[middle]);
        }

        Console.WriteLine(result2);
    }

}