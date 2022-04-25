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

    public void Add(TKey key, int count)
    {
      if (TryGetValue(key, out var currentCount))
        m_bins[key] = currentCount + count;
      else
        m_bins.Add(key, count);
    }
    public void Add(TKey key)
      => Add(key, 1);
    public void Add(System.Collections.Generic.IEnumerable<TKey> keys)
    {
      foreach (var key in keys)
        Add(key, 1);
    }
    public void Add<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
    {
      foreach (var element in collection)
        Add(keySelector(element), frequencySelector(element));
    }

    #region Implemented interfaces
    // IReadOnlyDictionary<>
    public int this[TKey key] => m_bins[key];
    public System.Collections.Generic.IEnumerable<TKey> Keys => m_bins.Keys;
    public System.Collections.Generic.IEnumerable<int> Values => m_bins.Values;
    public int Count => m_bins.Count;
    public bool ContainsKey(TKey key) => m_bins.ContainsKey(key);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey, int>> GetEnumerator() => m_bins.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => m_bins.GetEnumerator();
    public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out int value) => m_bins.TryGetValue(key, out value);
    #endregion Implemented interfaces
  }
}
