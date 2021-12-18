namespace Day18;

public static class NumberParser
{
    private static readonly HashSet<char> Nums = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public static Number ParseNumber(string numberString)
        => GetNumber(numberString.ToCharArray());

    private static Number GetNumber(ReadOnlySpan<char> str)
    {
        if (Nums.Contains(str[0])) return new TipNumber(int.Parse(str[0].ToString()));

        ReadOnlySpan<char> startOfRight;
        var depthCount = 0;
        for (var i = 1;; i++)
        {
            if (str[i] == '[') depthCount++;
            if (str[i] == ']') depthCount--;
            if (str[i] == ',' && depthCount is 0)
            {
                startOfRight = str.Slice(i + 1);
                break;
            }
        }

        return new BranchNumber(GetNumber(str.Slice(1)), GetNumber(startOfRight));
    }
}