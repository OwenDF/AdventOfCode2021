// See https://aka.ms/new-console-template for more information
var characterRows = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.ToCharArray()).ToArray();
var characters = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

var syntaxScore = 0;

foreach (var row in characterRows)
{
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

        syntaxScore += GetScoreForCharacter(character);
        break;
    }
}

Console.WriteLine(syntaxScore);

int GetScoreForCharacter(char character)
    => character switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137
    };
