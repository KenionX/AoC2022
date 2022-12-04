// ReSharper disable PossibleMultipleEnumeration
var pairs = File.ReadLines("input.txt").Select(i => i.Split(',').Select(elf =>
{
    var range = elf.Split('-').Select(int.Parse).ToArray();
    return Enumerable.Range(range[0], range[1] - range[0] + 1);
}).ToArray());

var validPairs = pairs.Where(pair =>
{
    var intersected = pair[0].Intersect(pair[1]);
    return intersected.Count() == Math.Min(pair[0].Count(), pair[1].Count());
});

//part2
var validPairs2 = pairs.Where(pair =>
{
    var intersected = pair[0].Intersect(pair[1]);
    return intersected.Any();
});

Console.WriteLine($"Answer1: {validPairs.Count()}");
Console.WriteLine($"Answer2: {validPairs2.Count()}");