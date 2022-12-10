var instructions = File.ReadAllLines("input.txt").Select(i =>
{
    var split = i.Split(' ');
    var instruction = split[0];
    var value = split.Length == 2 ? int.Parse(split[1]) : 0;
    return (instruction, value);
}).ToArray();

void ExecuteInstructions(bool partOne)
{
    var instructionQueue = new Queue<(string, int)>(instructions);
    var registerX = 1;
    var isExecutingAddx = false;
    var signalList = new List<int>();
    
    var x = partOne ? 1 : 0;
    var cycles = partOne ? 221 : 240;
    for (; x < cycles; x++)
    {
        if (!partOne)
        {
            if (x != 0 && x % 40 == 0)
                Console.WriteLine();
            Console.Write(Math.Abs(registerX - (x - x / 40 * 40)) > 1 ? " " : "█");
        }

        if ((x - 20) % 40 == 0 && x != 0)
            signalList.Add(registerX * x);

        if (instructionQueue.Peek().Item1 == "noop")
        {
            instructionQueue.Dequeue();
            continue;
        }

        if (isExecutingAddx)
        {
            var (_, value) = instructionQueue.Dequeue();
            registerX += value;
            isExecutingAddx = false;
            continue;
        }

        isExecutingAddx = true;
    }
    
    Console.WriteLine("");
    if(partOne)
        Console.WriteLine($"Answer1: {signalList.Sum()}");
}

ExecuteInstructions(true);
ExecuteInstructions(false);