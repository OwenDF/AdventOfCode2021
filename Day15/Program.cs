var dangerGrid = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

var dimensionOne = dangerGrid.Length;
var dimensionOneBig = dimensionOne * 5;
var dimensionTwo = dangerGrid[0].Length;
var dimensionTwoBig = dimensionTwo * 5;

var bigDangerGrid = new int[dimensionOneBig, dimensionTwoBig];
for (var i = 0; i < dimensionOneBig; i++)
for (var j = 0; j < dimensionTwoBig; j++)
{
    var dimOneFactor = i / dimensionOne;
    var dimTwoFactor = j / dimensionTwo;
    bigDangerGrid[i, j] = (dangerGrid[i - dimOneFactor * dimensionOne][j - dimTwoFactor * dimensionTwo] + dimOneFactor + dimTwoFactor - 1) % 9 + 1;
}

var distances = new int[dimensionOneBig, dimensionTwoBig];
var unvisitedNodes = new HashSet<(int, int)>();
var touchedNodes = new HashSet<(int, int)>();

for (var i = 0; i < dimensionOneBig; i++)
for (var j = 0; j < dimensionTwoBig; j++)
{
    distances[i, j] = Int32.MaxValue;
    unvisitedNodes.Add((i, j));
}

distances[0, 0] = 0;
var current = (0, 0);
unvisitedNodes.Remove((0, 0));
while (true)
{
    foreach (var (x, y) in GetNeighbours(current).Where(x => unvisitedNodes.Contains(x)))
    {
        var tentative = distances[current.Item1, current.Item2] + bigDangerGrid[x, y];
        distances[x, y] = Math.Min(distances[x, y], tentative);
        touchedNodes.Add((x, y));
    }
    
    unvisitedNodes.Remove(current);
    if (distances[dimensionOneBig - 1, dimensionTwoBig - 1] < int.MaxValue) break;
    touchedNodes.IntersectWith(unvisitedNodes);
    current = touchedNodes.Select(x => (x, distances[x.Item1, x.Item2])).OrderBy(x => x.Item2).First().x;
}

Console.WriteLine(distances[dimensionOneBig - 1, dimensionTwoBig - 1]);

(int,int)[] GetNeighbours((int i, int j) x) => new[] { (x.i - 1, x.j), (x.i + 1, x.j), (x.i, x.j - 1), (x.i, x.j + 1) };