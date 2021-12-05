// See https://aka.ms/new-console-template for more information
var lines = (await File.ReadAllLinesAsync("Input.txt")).Select(ParseLine).ToList();

var maxX = lines.Max(x => Math.Max(x.A.X, x.B.X));
var maxY = lines.Max(x => Math.Max(x.A.Y, x.B.Y));

var grid = new int[maxX + 1, maxY + 1];

foreach (var (a, b) in lines.Where(x => x.A.X == x.B.X || x.A.Y == x.B.Y))
{
    if (a.X > b.X) for (var i = b.X; i <= a.X; i++) grid[i, a.Y]++;
    if (a.X < b.X) for (var i = a.X; i <= b.X; i++) grid[i, a.Y]++;
    if (a.Y > b.Y) for (var i = b.Y; i <= a.Y; i++) grid[a.X, i]++;
    if (a.Y < b.Y) for (var i = a.Y; i <= b.Y; i++) grid[a.X, i]++;
}

// Part 2
foreach (var ((aX, aY), (bX, bY)) in lines.Where(x => x.A.X != x.B.X && x.A.Y != x.B.Y))
{
    if (aX > bX && aY > bY) for (var (i, j) = (bX, bY); i <= aX; i++, j++) grid[i, j]++;
    if (aX > bX && aY < bY) for (var (i, j) = (bX, bY); i <= aX; i++, j--) grid[i, j]++;
    if (aX < bX && aY > bY) for (var (i, j) = (aX, aY); i <= bX; i++, j--) grid[i, j]++;
    if (aX < bX && aY < bY) for (var (i, j) = (aX, aY); i <= bX; i++, j++) grid[i, j]++;
}

var count = 0;

for (var i = 0; i < maxX; i++)
for (var j = 0; j < maxY; j++)
    if (grid[i, j] > 1) count++;

Console.WriteLine(count);

Line ParseLine(string line)
{
    var x = line.Split(" -> ").Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();
    return new(new(x[0][0], x[0][1]), new(x[1][0], x[1][1]));
}

internal record struct Point(int X, int Y);
internal record struct Line(Point A, Point B);
