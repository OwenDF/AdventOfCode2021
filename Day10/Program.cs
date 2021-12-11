// See https://aka.ms/new-console-template for more information
var characterRows = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.ToCharArray()).ToArray();
var characters = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

var syntaxScore = 0;
var completionScoreList = new List<long>();

foreach (var row in characterRows)
{
    long completionScore = 0;
    var stack = new Stack<char>();
    foreach (var character in row)
    {
        if (characters.Values.Contains(character))
        {
            stack.Push(character);
            continue;
        }

        if (stack.Peek() == characters[character])
        {
            stack.Pop();
            continue;
        }

        syntaxScore += GetSyntaxScoreForCharacter(character);
        goto nextRow;
    }
    
    if (stack.Any()) foreach (var remainingCharacter in stack)
    {
        completionScore *= 5;
        completionScore += GetCompletionScoreForCharacter(remainingCharacter);
    }

    completionScoreList.Add(completionScore);
    nextRow: ;
}

Console.WriteLine(syntaxScore);
completionScoreList = completionScoreList.OrderBy(x => x).ToList();
Console.WriteLine(completionScoreList[(completionScoreList.Count + 1) / 2 - 1]);

int GetSyntaxScoreForCharacter(char character)
    => character switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137
    };

int GetCompletionScoreForCharacter(char character)
    => character switch
    {
        '(' => 1,
        '[' => 2,
        '{' => 3,
        '<' => 4
    };
4361305341

