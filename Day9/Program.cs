using System.Numerics;

var commands = File.ReadAllLines("input.txt").Select(i => (i.Split(' ')[0], int.Parse(i.Split(' ')[1])));

HashSet<Vector2> tailPositions = new();
HashSet<Vector2> tailPositionPart2 = new();

var knots = new Vector2[10];
foreach (var (direction, command) in commands.ToArray())
{
    for (var x = 0; x < command; x++)
    {
        knots[0] += direction switch
        {
            "R" => new Vector2(1, 0),
            "L" => new Vector2(-1, 0),
            "U" => new Vector2(0, 1),
            "D" => new Vector2(0, -1),
            _ => throw new ArgumentOutOfRangeException()
        };
        var headPosition = knots.First();

        for (var i = 1; i < knots.Length; i++)
        {
            var distance = Vector2.Distance(headPosition, knots[i]);
            if (distance >= 2)
                knots[i] += Vector2.Clamp(headPosition - knots[i], -Vector2.One,  Vector2.One);

            headPosition = knots[i];
        }

        tailPositions.Add(knots.Skip(1).First());
        tailPositionPart2.Add(knots.Last());
    }
}

Console.WriteLine($"Answer1: {tailPositions.Count}");
Console.WriteLine($"Answer2: {tailPositionPart2.Count}");