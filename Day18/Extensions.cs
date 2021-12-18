namespace Day18;

public static class Extensions
{
    public static int Magnitude(this Number node) => node switch
    {
        TipNumber x => x.Value,
        BranchNumber(var left, var right) => Magnitude(left) * 3 + Magnitude(right) * 2
    };
}