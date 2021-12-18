using Day18;

Console.WriteLine((await File.ReadAllLinesAsync("Input.txt"))
    .Select(NumberParser.ParseNumber)
    .Aggregate(NumberOperations.Add)
    .Magnitude());







