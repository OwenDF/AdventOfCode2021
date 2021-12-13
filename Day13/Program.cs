// See https://aka.ms/new-console-template for more information
var lines = await File.ReadAllLinesAsync("Input.txt");

var coordinates = new HashSet<Coordinate>();

for (var i = 0; lines[i] != ""; i++)
{
    var splitLine = lines[i].Split(',');
    coordinates.Add(new(int.Parse(splitLine[0]), int.Parse(splitLine[1])));
}

var instructions = lines.Where(x => x.StartsWith("fold along"))
    .Select(x => x.Split(" ")[2])
    .Select(x => x.Split("="))
    .Select(ParseOperation);

foreach (var instruction in instructions)
{
    var afterFold = new HashSet<Coordinate>();
    foreach (var coordinate in coordinates)
    {
        switch (instruction.Axis)
        {
            case Axis.X when coordinate.X > instruction.Value:
                afterFold.Add(coordinate with { X = instruction.Value - (coordinate.X - instruction.Value) });
                break;
            case Axis.Y when coordinate.Y > instruction.Value:
                afterFold.Add(coordinate with { Y = instruction.Value - (coordinate.Y - instruction.Value) });
                break;
            default:
                afterFold.Add(coordinate);
                break;
        }
    }

    coordinates = afterFold;
}

Console.WriteLine(coordinates.Count);

var maxX = coordinates.Max(x => x.X);
var maxY = coordinates.Max(y => y.Y);

var output = new int[maxX + 1, maxY + 1];
foreach (var coordinate in coordinates) output[coordinate.X, coordinate.Y] = 1;

for (var i = 0; i <= maxY; i++)
{
    for (var j = 0; j <= maxX; j++)
    {
        Console.Write(output[j, i] == 1 ? "#" : ".");
    }

    Console.Write(Environment.NewLine);
}

Operation ParseOperation(string[] arr)
    => new(
        arr[0] switch
        {
            "x" => Axis.X,
            "y" => Axis.Y
        },
        int.Parse(arr[1]));

internal enum Axis { Undefined, X, Y }
internal record Operation(Axis Axis, int Value);
internal record struct Coordinate(int X, int Y);