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

foreach (var instruction in instructions.Take(1))
{
    var afterFold = new HashSet<Coordinate>();
    foreach (var coordinate in coordinates)
    {
        if (instruction.Axis == Axis.X && coordinate.X > instruction.Value)
        {
            afterFold.Add(coordinate with { X = instruction.Value - (coordinate.X - instruction.Value) });
        }
        else if (instruction.Axis == Axis.Y && coordinate.Y > instruction.Value)
        {
            afterFold.Add(coordinate with { Y = instruction.Value - (coordinate.Y - instruction.Value) });
        }
        else afterFold.Add(coordinate);
    }

    coordinates = afterFold;
}

Console.WriteLine(coordinates.Count);

Operation ParseOperation(string[] arr)
    => new Operation(
        arr[0] switch
        {
            "x" => Axis.X,
            "y" => Axis.Y
        },
        int.Parse(arr[1]));

internal enum Axis { Undefined, X, Y }
internal record Operation(Axis Axis, int Value);
internal record struct Coordinate(int X, int Y);