namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A map, or key/value, a.k.a. dictionary, interface.</para>
  /// <para><see href="https://ericlippert.com/2008/01/18/immutability-in-c-part-eight-even-more-on-binary-trees/"/></para>
  /// </summary>
  /// <typeparam name="TKey">The type of key for the MAP node. This is used to access the associated <typeparamref name="TValue"/>.</typeparam>
  /// <typeparam name="TValue">The type of value for the MAP node.</typeparam>
  /// <remarks>The original implementation is courtesy Eric Lippert.</remarks>
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
