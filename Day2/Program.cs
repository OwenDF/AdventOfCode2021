// See https://aka.ms/new-console-template for more information
Console.WriteLine((await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.Split(' '))
    .Select(x => (x[0], int.Parse(x[1])))
    .Aggregate(new PositionChange(0, 0, 0), Apply, x => x.Depth * x.Horizontal));

PositionChange Apply(PositionChange current, (string op, int value) change)
    => change.op switch
    {
        "forward" => current with
        {
            Horizontal = current.Horizontal + change.value,
            Depth = current.Depth + current.Aim * change.value
        },
        "up" => current with { Aim = current.Aim - change.value },
        "down" => current with { Aim = current.Aim + change.value }
    };

internal record PositionChange(int Horizontal, int Depth, int Aim);
