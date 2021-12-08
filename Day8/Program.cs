// See https://aka.ms/new-console-template for more information
var inputOutput = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.Split('|')).ToList();
var outputs = inputOutput.Select(x => x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();

var uniques = outputs.SelectMany(output => output).Count(num => num.Length is 2 or 3 or 4 or 7);

Console.WriteLine(uniques);
