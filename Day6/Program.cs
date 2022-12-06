var dataStream = File.ReadAllText("input.txt");

Console.WriteLine($"Answer1: {GetStartOfSequence(dataStream, 4)}");
Console.WriteLine($"Answer2: {GetStartOfSequence(dataStream, 14)}");

int GetStartOfSequence(string input, int sequenceLength)
{
    var queue = new Queue<char>(input[..(sequenceLength - 1)]);
    for (var x = sequenceLength; x < input.Length; x++)
    {
        queue.Enqueue(input[x]);
        if (queue.Distinct().Count() == sequenceLength)
            return x + 1;
        queue.Dequeue();
    }
    throw new Exception("Missing sequence");
}