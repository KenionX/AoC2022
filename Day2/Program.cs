var data = File.ReadLines("Input.txt").Select(x => (x[0] - 'A',x[2] - 'X')).ToArray();
var solution1 = data.Select(x =>
{
    var (a, b) = x;
    return b + 1 + (b - a + 4) % 3 * 3;
}).Sum();

var solution2 = data.Select(x =>
{
    var (a, outcome) = x;
    return (outcome % 3 * 3) + ((outcome + 2) % 3 + a) % 3 + 1;
}).Sum();

Console.WriteLine($"Answer1: {solution1}");
Console.WriteLine($"Answer2: {solution2}");