namespace Flux.DataStructures.Immutable
{
  // https://blogs.msdn.microsoft.com/bclteam/2012/12/18/preview-of-immutable-collections-released-on-nuget/
  public interface IMap<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    System.Collections.Generic.IEnumerable<TKey> Keys { get; }
    System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get; }
    System.Collections.Generic.IEnumerable<TValue> Values { get; }
    bool Contains(TKey key);
    TValue Lookup(TKey key);
    IMap<TKey, TValue> Add(TKey key, TValue value);
    IMap<TKey, TValue> Remove(TKey key);
  }
}
