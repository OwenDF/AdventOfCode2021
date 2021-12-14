using Day14;

var lines = await File.ReadAllLinesAsync("Input.txt");

var sequence = lines[0];

Dictionary<(char first, char second), char> instructions = lines.Skip(2)
    .Select(x => x.Split(" -> "))
    .ToDictionary(x => (x[0][0], x[0][1]), x => x[1][0]);

var pairs = new Dictionary<CharacterPair, long>();
var operations = new Dictionary<CharacterPair, (CharacterPair, CharacterPair)>();

foreach (var item in instructions)
{
    var inputPair = new CharacterPair(item.Key.first, item.Key.second);
    var firstOutput = new CharacterPair(item.Key.first, item.Value);
    var secondOutput = new CharacterPair(item.Value, item.Key.second);

    pairs.AddOrUpdate(inputPair, 0, x => x);
    pairs.AddOrUpdate(firstOutput, 0, x => x);
    pairs.AddOrUpdate(secondOutput, 0, x => x);

    operations.Add(inputPair, (firstOutput, secondOutput));
}

for (var i = 0; i < sequence.Length - 1; i++)
{
    var pair = new CharacterPair(sequence[i], sequence[i + 1]);
    pairs.AddOrUpdate(pair, 1, x => ++x);
}

for (var i = 0; i < 40; i++)
{
    var newPairs = new Dictionary<CharacterPair, long>();
    foreach (var (key, count) in pairs)
    {
        var (firstOutput, secondOutput) = operations[key];
        newPairs.AddOrUpdate(firstOutput, count, x => x + count);
        newPairs.AddOrUpdate(secondOutput, count, x => x + count);
    }

    pairs = newPairs;
}

var characterCounts = new Dictionary<char, long>();

foreach (var pair in pairs)
{
    characterCounts.AddOrUpdate(pair.Key.First, pair.Value, x => x + pair.Value);
    characterCounts.AddOrUpdate(pair.Key.Second, pair.Value, x => x + pair.Value);
}

// The answer may be out by 1 due to the fact that one of the characters may be at the beginning or end of the sequence.
Console.WriteLine((characterCounts.Max(x => x.Value) - characterCounts.Min(x => x.Value)) / 2);

internal record struct CharacterPair(char First, char Second);