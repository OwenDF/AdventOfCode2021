// See https://aka.ms/new-console-template for more information

using System.Numerics;

const int days = 256;

var fishies = (await File.ReadAllTextAsync("Input.txt")).Split(',')
    .Select(int.Parse);

var arr = new BigInteger[9];
foreach (var fish in fishies) arr[fish]++;

for (var i = 0; i < days; i++)
{
    var newArr = new BigInteger[9];
    newArr[8] = arr[0];
    for (var j = 7; j >= 0; j--)
    {
        if (j == 6)
        {
            newArr[6] = arr[7] + arr[0];
            continue;
        }

        newArr[j] = arr[j + 1];
    }

    arr = newArr;
}

Console.WriteLine(arr.Aggregate(new BigInteger(), (c, n) => c + n));