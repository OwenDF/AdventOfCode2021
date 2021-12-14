namespace Day14;

internal static class Extensions
{
    public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> source)
    {
        var list = new LinkedList<T>();
        foreach (var item in source) list.AddLast(item);
        return list;
    }

    public static void AddOrUpdate<TKey, TValue>(
        this Dictionary<TKey, TValue> dict,
        TKey key,
        TValue addValue,
        Func<TValue, TValue> updateFactory)
    {
        if (dict.ContainsKey(key)) dict[key] = updateFactory(dict[key]);
        else dict[key] = addValue;
    }
}