var input = File.ReadAllLines("input.txt").Select(row => row.Select(tree => int.Parse(tree.ToString())).ToArray()).ToArray();

bool IsCovered(int x, int y)
{
    var left = input[y][..x].Max() >= input[y][x];
    var right = input[y][(x + 1)..].Max() >= input[y][x];
    var top = input[..y].Select(i => i[x]).Max() >= input[y][x];
    var bottom = input[(y + 1)..].Select(i => i[x]).Max() >= input[y][x];
    return left && right && top && bottom;
}

int ScenicScore(int x, int y)
{
    var leftTrees = input[y][..x].Reverse().TakeWhile(value => value < input[y][x]).Count() + 1;
    var rightTrees = input[y][(x + 1)..].TakeWhile(value => value < input[y][x]).Count() + 1;
    var topTrees = input[..y].Select(i => i[x]).Reverse().TakeWhile(value => value < input[y][x]).Count() + 1;
    var bottomTrees = input[(y + 1)..].Select(i => i[x]).TakeWhile(value => value < input[y][x]).Count() + 1;

    if (leftTrees > x)
        leftTrees--;
    if (topTrees > y)
        topTrees--;
    if (input[0].Length - x == rightTrees)
        rightTrees--;
    if (input.Length - y == bottomTrees)
        bottomTrees--;
    
    return leftTrees * rightTrees * topTrees * bottomTrees;
}

var answer1 = Enumerable.Range(1, input.Length - 2)
    .Sum(y => Enumerable.Range(1, input[y].Length - 2)
        .Count(x => !IsCovered(x, y)));

var answer2 = Enumerable.Range(1, input.Length - 1)
    .SelectMany(y => Enumerable.Range(1, input[y].Length - 1)
        .Select(x => ScenicScore(x, y))).Max();

Console.WriteLine($"Answer1: {answer1 + input.Length * 2 + input[0].Length * 2 - 4}");
Console.WriteLine($"Answer2: {answer2}");