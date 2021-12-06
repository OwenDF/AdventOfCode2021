// See https://aka.ms/new-console-template for more information
const int days = 256;

var fish = (await File.ReadAllTextAsync("Input.txt")).Split(',')
    .Select(int.Parse)
    .Select(x => new LanternFish(x))
    .ToList<ILanterFish>();

for (var i = 0; i < days; i++)
{
    var length = fish.Count;
    for (var j = 0; j < length; j++)
    {
        switch (fish[j])
        {
            case LanternFish { DaysToSpawn: > 0 } x:
                fish[j] = x with { DaysToSpawn = x.DaysToSpawn - 1 };
                continue;
            case LanternFish x:
                fish[j] = x with { DaysToSpawn = 6 };
                fish.Add(new ImmatureLanternFish(1));
                continue;
            case ImmatureLanternFish { DaysToMaturity: > 0 } x:
                fish[j] = x with { DaysToMaturity = x.DaysToMaturity - 1 };
                continue;
            case ImmatureLanternFish:
                fish[j] = new LanternFish(6);
                continue;
        }
    }
}

Console.WriteLine(fish.Count);

internal interface ILanterFish {}
internal record LanternFish(int DaysToSpawn) : ILanterFish;
internal record ImmatureLanternFish(int DaysToMaturity) : ILanterFish;
