namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified <paramref name="targetKey"/> in <paramref name="source"/>. Keys are identified using the <see cref="keySelector"/>. Uses the specified <paramref name="comparer"/> (or the default comparer, if null).</summary>
    public static (TSource ltItem, int ltIndex, TSource gtItem, int gtIndex) ExtremaNearest<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, TKey targetKey, System.Collections.Generic.IComparer<TKey>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

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
  }
}
