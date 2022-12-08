var instructions = File.ReadAllLines("Input.txt");

var mainNode = new Node();
var activeNode = mainNode;

//building file system
foreach (var inst in instructions.Skip(1))
{
    if (activeNode is null)
        throw new Exception("active node can't be null");
    
    if (inst == "$ ls")
        continue;
    
    if (inst.Contains("$ cd"))
    {
        var target = inst.Split("cd ").Last();
        activeNode = target == ".." ? activeNode.ParentNode : activeNode.Children[target];
        continue;
    }

    if (inst.Contains("dir"))
    {
        var directory = inst.Split(" ").Last();
        activeNode.Children[directory] = new Node
        {
            ParentNode = activeNode
        };
        continue;
    }

    var file = inst.Split(" ");
    activeNode.Children[file[1]] = new Node
    {
        FileSize = int.Parse(file[0]),
        ParentNode = activeNode
    };
}

List<int> matching = new();
List<int> matching2 = new();

var freeSpace = 70000000 - mainNode.GetDirectorySize();
var requiredAdditional = 30000000 - freeSpace;

mainNode.GetAnswer1Values(matching);
mainNode.GetAnswer2Values(matching2, requiredAdditional);

Console.WriteLine($"Answer1: {matching.Sum()}");
Console.WriteLine($"Answer2: {matching2.Min()}");

internal class Node
{
    public Node? ParentNode;
    public Dictionary<string, Node> Children { get; } = new();
    public int FileSize { get; init; }

    public void GetAnswer1Values(List<int> matching)
    {
        matching.AddRange(Children.Values.Where(c => c.Children.Any()).Select(x => x.GetDirectorySize()).Where(size => size <= 100000));
        foreach (var child in Children.Values)
            child.GetAnswer1Values(matching);
    }
    
    public void GetAnswer2Values(List<int> matching, int requiredSpace)
    {
        matching.AddRange(Children.Values.Where(c => c.Children.Any()).Select(x => x.GetDirectorySize()).Where(size => size >= requiredSpace));
        foreach (var child in Children.Values)
            child.GetAnswer2Values(matching, requiredSpace);
    }
  
    public int GetDirectorySize() => Children.Values.Sum(i => i.GetDirectorySize()) + FileSize;

    public Node(Node? parentNode = null)
    {
        ParentNode = parentNode;
    }
}