var dangerGrid = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

var pathGrid = new int[dangerGrid.Length, dangerGrid[0].Length];
pathGrid[0, 0] = dangerGrid[0][0];

for (var i = 1; i < dangerGrid.Length; i++) pathGrid[i, 0] = pathGrid[i - 1, 0] + dangerGrid[i][0];
for (var i = 1; i < dangerGrid[0].Length; i++) pathGrid[0, i] = pathGrid[0, i - 1] + dangerGrid[0][i];

for (var i = 1; i < dangerGrid.Length; i++)
for (var j = 1; j < dangerGrid[0].Length; j++)
{
    pathGrid[i, j] = dangerGrid[i][j] + Math.Min(pathGrid[i - 1, j], pathGrid[i, j - 1]);
}

Console.WriteLine(pathGrid[dangerGrid.Length - 1, dangerGrid[0].Length - 1] - dangerGrid[0][0]);