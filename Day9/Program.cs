// See https://aka.ms/new-console-template for more information
var inputInts = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.ToCharArray())
    .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
    .ToArray();

var width = inputInts[0].Length;
var height = inputInts.Length;
var lowPoints = new Dictionary<(int row, int column), int>();

for (var i = 0; i < height; i++)
for (var j = 0; j < width; j++)
{
    var elevation = inputInts[j][i];
    var above = j != 0 ? inputInts[j - 1][i] : 10;
    var below = j != height - 1 ? inputInts[j + 1][i] : 10;
    var left = i != 0 ? inputInts[j][i - 1] : 10;
    var right = i != width - 1 ? inputInts[j][i + 1] : 10;
    if (elevation < above && elevation < below && elevation < left && elevation < right) lowPoints.Add((i, j), 0);
}

var basinMap = new int[height, width];
for (var i = 0; i < height; i++)
for (var j = 0; j < width; j++)
    if (inputInts[i][j] < 9)
        basinMap[i, j] = 1;

foreach (var (row, column) in lowPoints.Keys)
{
    lowPoints[(row, column)] = GetAdjacentPoints(row, column);
}

int GetAdjacentPoints(int row, int column)
{
    if (row >= height || column >= width || row < 0 || column < 0) return 0;
    if (basinMap[row, column] is not 1) return 0;

    basinMap[row, column] = 0;
    return 1 + GetAdjacentPoints(row + 1, column) + GetAdjacentPoints(row - 1, column) +
           GetAdjacentPoints(row, column + 1) + GetAdjacentPoints(row, column - 1);
}

Console.WriteLine(lowPoints.Values.OrderByDescending(x => x).Take(3).Aggregate(1, (c, n) => c * n));