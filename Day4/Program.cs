// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("Input.txt");
var numbersToDraw = lines[0].Split(',').Select(int.Parse).ToList();

var boards = new List<Board>();

for (var i = 2; i < lines.Length; i += 6)
{
    var boardStrings = new Span<string>(lines, i, 5).ToArray();
    var boardRows = boardStrings.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .Select(x => x.Select(int.Parse).ToArray())
        .ToArray();
    
    var dict = new Dictionary<int, bool>();
    var sets = new List<HashSet<int>>(YieldNewHashSets().Take(10));
    
    for(var j = 0; j < 5; j++)
    for (var k = 0; k < 5; k++)
    {
        var num = boardRows[j][k];
        dict[num] = false;
        sets[j].Add(num);
        sets[k + 5].Add(num);
    }

    boards.Add(new(dict, sets));
}

var boardsNotWon = new HashSet<Board>(boards);

var numbersDrawn = new HashSet<int>();
foreach (var num in numbersToDraw)
{
    numbersDrawn.Add(num);
    foreach (var board in boards.Where(board => board.Numbers.ContainsKey(num)))
    {
        board.Numbers[num] = true;
        if (board.PossibleWinningSets.Any(set => set.IsSubsetOf(numbersDrawn)) && boardsNotWon.Contains(board))
        {
            Console.WriteLine(board.Numbers.Where(x => !x.Value).Sum(x => x.Key) * num);
            boardsNotWon.Remove(board);
        }
    }
}

IEnumerable<HashSet<int>> YieldNewHashSets()
{
    while (true) yield return new();
}

internal record Board(Dictionary<int, bool> Numbers, IEnumerable<ISet<int>> PossibleWinningSets);