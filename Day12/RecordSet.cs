using System.Collections;

namespace Day12;

public class RecordSet<T> : IReadOnlySet<T>
{
    private readonly IReadOnlySet<T> _inner;

    public RecordSet(HashSet<T> inner)
    {
        _inner = inner;
    }

    public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();
    public int Count => _inner.Count;
    public bool Contains(T item) => _inner.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<T> other) => _inner.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => _inner.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => _inner.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => _inner.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => _inner.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => _inner.SetEquals(other);

    public override bool Equals(object? obj)
        => obj is RecordSet<T> other && (ReferenceEquals(other, this) || Equals(other));

    public bool Equals(RecordSet<T> other) => this.SequenceEqual(other);
    public override int GetHashCode() => this.Aggregate(GetType().GetHashCode(), HashCode.Combine);
}