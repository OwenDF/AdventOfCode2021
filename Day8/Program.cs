// See https://aka.ms/new-console-template for more information

using Day8;

var inputOutput = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.Split('|')).ToList();
var outputs = inputOutput.Select(x => x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();
var dicts = inputOutput
    .Select(x => x[0].Split(' ', StringSplitOptions.RemoveEmptyEntries))
    .Select(ToLightDictionary).ToList();

var sum = 0;
for (var i = 0; i < inputOutput.Count; i++)
{
    sum += int.Parse(outputs[i].Select(x => dicts[i][x.Sort()]).Aggregate("", (c, n) => c + n));
}

Console.WriteLine(sum);

Dictionary<string, string> ToLightDictionary(string[] numCodes)
{
    var letters = new [] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
    
    var dict = new Dictionary<string, string>
    {
        { numCodes.Single(x => x.Length is 7).Sort(), "8" },
        { numCodes.Single(x => x.Length is 4).Sort(), "4" },
        { numCodes.Single(x => x.Length is 3).Sort(), "7" },
        { numCodes.Single(x => x.Length is 2).Sort(), "1" }
    };

    var sixNineZero = numCodes.Where(x => x.Length is 6);
    var twoThreeFive = numCodes.Where(x => x.Length is 5);
    var f = numCodes.Single(x => x.Length is 2).Single(x => sixNineZero.All(y => y.Contains(x)));
    var c = numCodes.Single(x => x.Length is 2).Single(x => x != f);
    var six = sixNineZero.Single(x => !x.Contains(c));
    var two = twoThreeFive.Single(x => !x.Contains(f));
    var three = twoThreeFive.Single(x => x != two && x.Contains(c));
    var e = letters.Single(x => !twoThreeFive.Where(y => y != two).Any(y => y.Contains(x)));
    dict.Add(six.Sort(), "6");
    dict.Add(two.Sort(), "2");
    dict.Add(three.Sort(), "3");
    dict.Add(twoThreeFive.Single(x => x != three && x != two).Sort(), "5");
    dict.Add(sixNineZero.Single(x => x != six && x.Contains(e)).Sort(), "0");
    dict.Add(sixNineZero.Single(x => x != six && !x.Contains(e)).Sort(), "9");
    
    return dict;
}
