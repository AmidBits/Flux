namespace Flux.DataStructure
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
    /// <summary>
    /// <para>Creates a new sequence of all <typeparamref name="TKey"/> items in the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    System.Collections.Generic.IEnumerable<TKey> Keys { get; }

    /// <summary>
    /// <para>Creates a new sequence of all <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> items in the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Pairs { get; }

    /// <summary>
    /// <para>Creates a new sequence of all <typeparamref name="TValue"/> items in the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    System.Collections.Generic.IEnumerable<TValue> Values { get; }

    /// <summary>
    /// <para>Add the <paramref name="key"/> with the <paramref name="value"/> to the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    IMap<TKey, TValue> Add(TKey key, TValue value);

    /// <summary>
    /// <para>Indicates whether the <paramref name="key"/> exists in the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    bool Contains(TKey key);

    /// <summary>
    /// <para>Gets the <typeparamref name="TValue"/> of the <paramref name="key"/> in the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    TValue Lookup(TKey key);

    /// <summary>
    /// <para>Remove the <paramref name="key"/> from the <see cref="IMap{TKey, TValue}"/>.</para>
    /// </summary>
    IMap<TKey, TValue> Remove(TKey key);
  }
}
