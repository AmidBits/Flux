namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate the nearest (less than/greater than) elements and indices to <paramref name="targetKey"/>, as evaluated by the <paramref name="keySelector"/>, in <paramref name="source"/>. Uses the specified (default if null) <paramref name="comparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (TSource ltItem, int ltIndex, TSource gtItem, int gtIndex) ExtremaNearest<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, TKey targetKey, System.Collections.Generic.IComparer<TKey>? comparer = null)
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var ltItem = default(TSource)!;
        var ltIndex = -1;
        var ltKey = targetKey;

        var gtItem = default(TSource)!;
        var gtIndex = -1;
        var gtKey = targetKey;

        for (var index = 0; e.MoveNext(); index++)
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
        }

        return (ltItem, ltIndex, gtItem, gtIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
