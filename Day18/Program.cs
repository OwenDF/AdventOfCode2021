using Day18;

var numbers = (await File.ReadAllLinesAsync("Input.txt")).Select(NumberParser.ParseNumber).ToList();

var max = 0;

foreach (var first in numbers) foreach (var second in numbers) if (first != second)
    max = Math.Max(max, first.Add(second).Magnitude());

Console.WriteLine(max);