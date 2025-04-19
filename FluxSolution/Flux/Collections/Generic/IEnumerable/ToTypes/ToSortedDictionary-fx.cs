namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Creates a new <see cref="SortedDictionary{TKey, TValue}"/> with the specified <paramref name="comparer"/> and all items from <paramref name="source"/> using <paramref name="keySelector"/> and <paramref name="valueSelector"/> for each item.</para>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="valueSelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer ?? System.Collections.Generic.Comparer<TKey>.Default);

      using var e = source.GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
        sd.Add(keySelector(e.Current, index), valueSelector(e.Current, index));

      return sd;
    }

    /// <summary>
    /// <para>Creates a new <see cref="SortedDictionary{TKey, TValue}"/> with all key-value-pairs from <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      => source.ToSortedDictionary((e, i) => e.Key, (e, i) => e.Value, comparer);
  }
}
