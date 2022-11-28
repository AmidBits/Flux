namespace Flux.DataStructures.Immutable
{
  // https://blogs.msdn.microsoft.com/bclteam/2012/12/18/preview-of-immutable-collections-released-on-nuget/
  public interface IMap<TKey, TValue>
    where TKey : System.IComparable<TKey>
  {
    /// <summary>Get all keys in the map.</summary>
    System.Collections.Generic.IEnumerable<TKey> Keys { get; }
    /// <summary>Get all key/value pairs in the map.</summary>
    System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get; }
    /// <summary>Get all values in the map.</summary>
    System.Collections.Generic.IEnumerable<TValue> Values { get; }

    /// <summary>Determines whether the key exists in the map.</summary>
    bool Contains(TKey key);

    /// <summary>Gets the value of the key in the map.</summary>
    TValue Lookup(TKey key);
    /// <summary>Add the key with the value to the map.</summary>
    IMap<TKey, TValue> Add(TKey key, TValue value);
    /// <summary>Remove the key from the map.</summary>
    IMap<TKey, TValue> Remove(TKey key);
  }
}
