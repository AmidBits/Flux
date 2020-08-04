namespace Flux.Collections.Immutable
{
  // https://blogs.msdn.microsoft.com/bclteam/2012/12/18/preview-of-immutable-collections-released-on-nuget/

  public interface ISortedDictionary<TKey, TValue>
    : IMap<TKey, TValue>, System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>
    //: System.Collections.Generic.IDictionary<K, V>
    where TKey : System.IComparable<TKey>
  {
    new TValue this[TKey key] { get; }
    bool IsEmpty { get; }
    new ISortedDictionary<TKey, TValue> Add(TKey key, TValue value);
    new ISortedDictionary<TKey, TValue> Remove(TKey key);
  }
}
