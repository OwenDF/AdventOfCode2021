// See https://aka.ms/new-console-template for more information
var submarines = (await File.ReadAllTextAsync("Input.txt")).Split(',').Select(int.Parse).ToList();
var max = submarines.Max();

var minFuel = int.MaxValue;

for (var i = 0; i <= max; i++)
{
    var fuelUsed = 0;
    foreach (var submarine in submarines)
    {
        var steps = Math.Abs(submarine - i);
        for (var j = 1; j <= steps; j++) fuelUsed += j;
    }

    minFuel = Math.Min(minFuel, fuelUsed);
}

Console.WriteLine(minFuel);