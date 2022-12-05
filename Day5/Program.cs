using System.Text.RegularExpressions;

var metaData = File.ReadAllText("input.txt").Split("\r\n\r\n").Select(group => group.Split("\n")).ToArray();

//load operations
var operations = metaData[1].Select(o =>
{
    var matches = Regex.Matches(o, @"\d+");
    return new Operation(int.Parse(matches[0].Value), int.Parse(matches[1].Value) - 1, int.Parse(matches[2].Value) - 1);
}).ToArray();

//load container data
var containerCount = int.Parse(metaData[0].Last().Split("   ").Last().Trim());
var containers = Enumerable.Range(0, containerCount).Select(x =>
{
    var items = new Stack<char>();
    foreach (var itemRow in metaData[0][..^1].Reverse())
    {
        var item = itemRow[4 * x + 1];
        if(item != ' ')
            items.Push(item);
    }
    return items;
}).ToArray();

//duplicate container for part2
var containers2 = containers.Select(x => new Stack<char>(x.Reverse())).ToArray();

//execute move operations
foreach (var operation in operations)
{
    var fromStack = containers[operation.FromStack];
    var toStack = containers[operation.ToStack];
    for (var x = 0; x < operation.Num; x++)
        toStack.Push(fromStack.Pop());
    
    //part2
    var moveAble = new List<char>();
    fromStack = containers2[operation.FromStack];
    toStack = containers2[operation.ToStack];
    for (var x = 0; x < operation.Num; x++)
        moveAble.Add(fromStack.Pop());
    
    foreach (var item in moveAble.ToArray().Reverse())
        toStack.Push(item);
}

//buildAnswers
var answer1 = new string(containers.Select(i => i.Peek()).ToArray());
var answer2 = new string(containers2.Select(i => i.Peek()).ToArray());

Console.WriteLine($"Answer1: {answer1}");
Console.WriteLine($"Answer1: {answer2}");

internal record struct Operation(int Num, int FromStack, int ToStack);