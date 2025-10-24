namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This interface represents an ordered dictionary.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  public interface IOrderedDictionary<TKey, TValue>
    : System.Collections.Generic.IDictionary<TKey, TValue>, System.Collections.IDictionary
    where TKey : notnull
  {
    /// <summary>
    /// <para>Gets or sets the value at the specified index in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    TValue this[int index] { get; set; }

    /// <summary>
    /// <para>Creates a new sequence of all indices in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    /// <remarks>Added for a more complete symmetry between a <see cref="IDictionary{TKey, TValue}"/> and a <see cref="IOrderedDictionary{TKey, TValue}"/>.</remarks>
    public System.Collections.Generic.IEnumerable<int> Indices { get; }

    /// <summary>
    /// <para>Determines if the <paramref name="value"/> exists in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool ContainsValue(TValue value);

    /// <summary>
    /// <para>Inserts an <paramref name="key"/>/<paramref name="value"/> pair into the <see cref="IOrderedDictionary{TKey, TValue}"/> at the specified index.</para>
    /// </summary>
    void Insert(int index, TKey key, TValue value);

    /// <summary>
    /// <para>Gets the <paramref name="index"/> associated with the specified <paramref name="key"/> in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool TryGetIndex(TKey key, out int index);

    /// <summary>
    /// <para>Gets the <paramref name="index"/> associated with the specified <paramref name="value"/> in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool TryGetIndex(TValue value, out int index);

    /// <summary>
    /// <para>Gets the <paramref name="key"/> associated with the specified <paramref name="index"/> in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool TryGetKey(int index, out TKey key);

    /// <summary>
    /// <para>Gets the <paramref name="key"/> associated with the specified <paramref name="value"/> in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool TryGetKey(TValue value, out TKey key);

    /// <summary>
    /// <para>Gets the <paramref name="value"/> associated with the specified <paramref name="index"/> in the <see cref="IOrderedDictionary{TKey, TValue}"/>.</para>
    /// </summary>
    bool TryGetValue(int index, out TValue value);
  }
}
