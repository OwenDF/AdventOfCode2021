namespace Day8;

internal static class Extensions
{
    public static string Sort(this string source) => new string(source.OrderBy(x => x).ToArray());
}