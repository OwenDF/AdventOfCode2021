// See https://aka.ms/new-console-template for more information

var linesAsBits = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(x => x.ToCharArray())
    .Select(x => x.Select(y => int.Parse(y.ToString())).ToList())
    .ToList();

var width = linesAsBits.First().Count;

var gammaBits = new bool[width];
var oxygenList = linesAsBits.ToList();
var carbonList = linesAsBits.ToList();

for (var i = 0; i < width; i++)
{
    var oneCount = 0;
    var zeroCount = 0;
    foreach (var line in linesAsBits)
    {
        if (line[i] is 0) zeroCount++;
        else oneCount++;
    }

    if (oneCount > zeroCount) gammaBits[i] = true;
    else gammaBits[i] = false;
}

var epsilonBits = gammaBits.Select(x => !x).ToArray();

var gamma = ConvertArrayToBinary(gammaBits);
var epsilon = ConvertArrayToBinary(epsilonBits);

Console.WriteLine(gamma * epsilon);

for (var i = 0; i < width; i++)
{
    var oneCount = 0;
    var zeroCount = 0;
    foreach (var line in oxygenList)
    {
        if (line[i] is 0) zeroCount++;
        else oneCount++;
    }

    oxygenList = CleanList(oxygenList, zeroCount > oneCount ? 0 : 1, i);
    if (oxygenList.Count is 1) break;
}

for (var i = 0; i < width; i++)
{
    var oneCount = 0;
    var zeroCount = 0;
    foreach (var line in carbonList)
    {
        if (line[i] is 0) zeroCount++;
        else oneCount++;
    }

    carbonList = CleanList(carbonList, oneCount < zeroCount ? 1 : 0, i);
    if (carbonList.Count is 1) break;
}

Console.WriteLine(ConvertListToBinary(oxygenList.Single().AsEnumerable().Reverse().ToList()) *
                  ConvertListToBinary(carbonList.Single().AsEnumerable().Reverse().ToList()));

int ConvertListToBinary(List<int> list)
{
    var total = 0;
    for (var i = 0; i < list.Count; i++)
        total += list[i] << i;

    return total;
}

int ConvertArrayToBinary(bool[] arr)
    => ConvertListToBinary(arr.Select(x => x ? 1 : 0).Reverse().ToList());

List<List<int>> CleanList(List<List<int>> list, int numToKeep, int atIndex)
    => list.Where(x => x[atIndex] == numToKeep).ToList(); 

