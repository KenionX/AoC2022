var data = File.ReadAllText("Input.txt").Split("\r\n\r\n").Select(elf => elf.Split("\r\n").Sum(int.Parse)).ToArray();
Console.WriteLine($"Solution1: {data.Max()}");
Console.WriteLine($"Solution2: {data.OrderDescending().Take(3).Sum()}");