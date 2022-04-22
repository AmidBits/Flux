namespace Flux.DataStructures
{
  /// <summary>A simple generic histogram.</summary>
  public sealed class SimpleHistogram<TKey>
    : System.Collections.Generic.IReadOnlyDictionary<TKey, int>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.SortedDictionary<TKey, int> m_bins;

    public SimpleHistogram()
      => m_bins = new System.Collections.Generic.SortedDictionary<TKey, int>();

    public void Append(TKey key, int count)
    {
      if (TryGetValue(key, out var currentCount))
        m_bins[key] = currentCount + count;
      else
        m_bins.Add(key, count);
    }
    public void Append(TKey key)
      => Append(key, 1);
    public void Append(System.Collections.Generic.IEnumerable<TKey> keys)
    {
      foreach (var key in keys)
        Append(key, 1);
    }
    public void Append<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
    {
      foreach (var element in collection)
        Append(keySelector(element), frequencySelector(element));
    }
    public void Append<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector)
      => Append(collection, keySelector, element => 1);

    #region Implemented interfaces
    // IReadOnlyDictionary<>
    public int this[TKey key] => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins)[key];
    public System.Collections.Generic.IEnumerable<TKey> Keys => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).Keys;
    public System.Collections.Generic.IEnumerable<int> Values => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).Values;
    public int Count => ((System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, int>>)m_bins).Count;
    public bool ContainsKey(TKey key) => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).ContainsKey(key);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, int>> GetEnumerator() => ((System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>>)m_bins).GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ((System.Collections.IEnumerable)m_bins).GetEnumerator();
    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value) => ((System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_bins).TryGetValue(key, out value);
    #endregion Implemented interfaces
  }
}
