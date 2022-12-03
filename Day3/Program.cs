var data = File.ReadLines("Input.txt").ToArray();
var components = data.Select(i => i[..(i.Length/2)].Intersect(i[(i.Length/2)..]).First()).ToArray();
var groupData = Enumerable.Range(0, data.Length / 3)
    .Select(i => data[(i * 3)..(i * 3 + 3)])
    .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).First()).ToArray();

int Value(char c) => c > 'Z' ? c - 'a' + 1 : c - 'A' + 27;

Console.WriteLine($"Answer1: {components.Sum(Value)}");
Console.WriteLine($"Answer2: {groupData.Sum(Value)}");