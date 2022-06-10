namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the specified comparer.</summary>
    public static (TSource elementLt, int indexLt, TSource elementGt, int indexGt) ExtremaClosestToKey<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, TKey targetKey, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var elementLt = default(TSource)!;
      var indexLt = -1;
      var keyLt = targetKey;

      var elementGt = default(TSource)!;
      var indexGt = -1;
      var keyGt = targetKey;

      using var e = source.GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
      {
        var key = keySelector(e.Current);
        var cmp = comparer.Compare(key, targetKey);

        if (cmp < 0 && (indexLt < 0 || comparer.Compare(key, keyLt) > 0))
        {
          elementLt = e.Current;
          indexLt = index;
          keyLt = key;
        }
        if (cmp > 0 && (indexGt < 0 || comparer.Compare(key, keyGt) < 0))
        {
          elementGt = e.Current;
          indexGt = index;
          keyGt = key;
        }
      }

      return (elementLt, indexLt, elementGt, indexGt);
    }
    /// <summary>Locate the max element that is less than and the min element that is greater than the specified reference value identified by the <see cref="keySelector"/>. Uses the default comparer.</summary>
    public static (TSource elementLt, int indexLt, TSource elementGt, int indexGt) ExtremaClosestToKey<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> valueSelector, TKey targetKey)
      => ExtremaClosestToKey(source, valueSelector, targetKey, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
