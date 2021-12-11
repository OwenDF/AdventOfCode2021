// See https://aka.ms/new-console-template for more information
var grid = new int[10, 10];
var octopuses = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
    .ToArray();

for (var i = 0; i < 10; i++) for (var j = 0; j < 10; j++) grid[i, j] = octopuses[i][j];

var totalFlashes = 0;

for (var i = 0; i < 100; i++)
{
    var flashed = new HashSet<(int x, int y)>();
    for (var j = 0; j < 10; j++) for (var k = 0; k < 10; k++) grid[j, k]++;
    
    for (var j = 0; j < 10; j++) for (var k = 0; k < 10; k++) FlashAt(j, k, flashed, false);

    foreach (var flashPoint in flashed) grid[flashPoint.x, flashPoint.y] = 0;
}

void FlashAt(int x, int y, HashSet<(int, int)> flashed, bool increment)
{
    if (x is > 9 or < 0 || y is > 9 or < 0) return;
    if (increment) grid[x, y]++;
    if (grid[x, y] < 10 || flashed.Contains((x, y))) return;

    totalFlashes++;
    flashed.Add((x, y));

    FlashAt(x, y - 1, flashed, true);
    FlashAt(x, y + 1, flashed, true);
    FlashAt(x - 1, y - 1, flashed, true);
    FlashAt(x - 1, y, flashed, true);
    FlashAt(x - 1, y + 1, flashed, true);
    FlashAt(x + 1, y - 1, flashed, true);
    FlashAt(x + 1, y, flashed, true);
    FlashAt(x + 1, y + 1, flashed, true);
}

Console.WriteLine(totalFlashes);