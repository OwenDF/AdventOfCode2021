// See https://aka.ms/new-console-template for more information
var lines = (await File.ReadAllLinesAsync("Input.txt")).Select(int.Parse).ToArray();

var count = 0;
for (var i = 3; i < lines.Length; i++)
{
    if (SumWindow(lines, i) > SumWindow(lines, i - 1)) count++;
}

Console.WriteLine(count);

int SumWindow(int[] arr, int i)
    => arr[i] + arr[i - 1] + arr[i - 2];