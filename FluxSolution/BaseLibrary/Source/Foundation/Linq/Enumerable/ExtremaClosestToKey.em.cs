namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the specified comparer.</summary>
    public static (TSource ltItem, int ltIndex, TSource gtItem, int gtIndex) ExtremaClosestToKey<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, TKey targetKey, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var ltItem = default(TSource)!;
        var ltIndex = -1;
        var ltKey = targetKey;

        var gtItem = default(TSource)!;
        var gtIndex = -1;
        var gtKey = targetKey;

        var index = 0;

        while (e.MoveNext())
        {
          var key = keySelector(e.Current);
          var cmp = comparer.Compare(key, targetKey);

          if (cmp < 0 && (ltIndex < 0 || comparer.Compare(key, ltKey) > 0))
          {
            ltItem = e.Current;
            ltIndex = index;
            ltKey = key;
          }
          if (cmp > 0 && (gtIndex < 0 || comparer.Compare(key, gtKey) < 0))
          {
            gtItem = e.Current;
            gtIndex = index;
            gtKey = key;
          }

          index++;
        }

        return (ltItem, ltIndex, gtItem, gtIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the default comparer.</summary>
    public static (TSource ltItem, int ltIndex, TSource gtItem, int gtIndex) ExtremaClosestToKey<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> valueSelector, TKey targetKey)
      => ExtremaClosestToKey(source, valueSelector, targetKey, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
