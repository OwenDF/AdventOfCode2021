// See https://aka.ms/new-console-template for more information
var inputInts = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.ToCharArray())
    .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
    .ToArray();

var width = inputInts[0].Length - 1;
var height = inputInts.Length - 1;
var riskLevel = 0;

for (var i = 0; i <= width; i++)
for (var j = 0; j <= height; j++)
{
    var elevation = inputInts[j][i];
    var above = j != 0 ? inputInts[j - 1][i] : 10;
    var below = j != height ? inputInts[j + 1][i] : 10;
    var left = i != 0 ? inputInts[j][i - 1] : 10;
    var right = i != width ? inputInts[j][i + 1] : 10;
    if (elevation < above && elevation < below && elevation < left && elevation < right) riskLevel += elevation + 1;
}

Console.WriteLine(riskLevel);
