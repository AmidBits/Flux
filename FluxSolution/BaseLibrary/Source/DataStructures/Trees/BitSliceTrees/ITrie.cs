namespace Flux.DataStructure
{
  /// <summary>
  /// <para>A simple implementation of a trie data structure, which essentially is a storage structure for storing sequential data by their smaller components, e.g. <see cref="string"/>s (where the storage unit is <see cref="char"/>).</para>
  /// <para>This data structure is good for things like file paths and names (e.g. street names) where many variations of similar names exists.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Trie"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Bit-slice_trees"/></para>
  /// <para><seealso href="https://github.com/gmamaladze/trienet/tree/master/TrieNet"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  public interface ITrie<TKey, TValue>
    where TKey : notnull
  {
    /// <summary>
    /// <para>All keys and their respective tries that belong to the <see cref="ITrie{TKey, TValue}"/>.</para>
    /// </summary>
    System.Collections.Generic.IReadOnlyDictionary<TKey, ITrie<TKey, TValue>> Children { get; }

    /// <summary>
    /// <para>The value of a <see cref="ITrie{TKey, TValue}"/>.</para>
    /// </summary>
    /// <remarks>Only a terminal <see cref="ITrie{TKey, TValue}"/> will ever have a value.</remarks>
    TValue Value { get; }

    /// <summary>
    /// <para>Delete a <paramref name="keySpan"/>, and its associated value, from the <see cref="ITrie{TKey, TValue}"/>.</para>
    /// </summary>
    /// <param name="keySpan"></param>
    /// <returns>The number of keys that were actually deleted.</returns>
    int Delete(System.ReadOnlySpan<TKey> keySpan);

    /// <summary>
    /// <para>Find a <paramref name="keySpan"/> in the <see cref="ITrie{TKey, TValue}"/>.</para>
    /// </summary>
    /// <param name="keySpan"></param>
    /// <param name="value"></param>
    /// <returns>Whether the <paramref name="keySpan"/> was found, and the associated <paramref name="value"/> as an out parameter.</returns>
    bool Find(System.ReadOnlySpan<TKey> keySpan, out TValue value);

    /// <summary>
    /// <para>Insert a <paramref name="keySpan"/>, with the associated <paramref name="value"/>, into the <see cref="ITrie{TKey, TValue}"/>.</para>
    /// </summary>
    /// <param name="keySpan"></param>
    /// <param name="value"></param>
    /// <returns>The number of keys that were actually inserted.</returns>
    int Insert(System.ReadOnlySpan<TKey> keySpan, TValue value);
  }
}
