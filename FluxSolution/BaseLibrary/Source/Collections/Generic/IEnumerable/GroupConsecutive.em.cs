namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Creates a new sequence of equal (based on the specified keySelector) consecutive (adjacent) items grouped together as a key and a list.</summary>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupConsecutive<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      using var e = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      if (e.MoveNext())
      {
        var goa = new ConsecutiveGrouping<TKey, TSource>(keySelector(e.Current), e.Current);

        while (e.MoveNext())
        {
          var key = keySelector(e.Current);

          if (comparer.Equals(goa.Key, key))
          {
            goa.Consecutive.Add(e.Current);
          }
          else
          {
            yield return goa;

            goa = new ConsecutiveGrouping<TKey, TSource>(key, e.Current);
          }
        }

        if (goa.Consecutive.Count > 0) yield return goa;
      }
    }
    /// <summary>Creates a new sequence of equal (based on the specified keySelector) consecutive (adjacent) items grouped together as a key and a list.</summary>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupConsecutive<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector)
      => GroupConsecutive(source, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);

    private class ConsecutiveGrouping<TKey, TSource> : System.Collections.Generic.IEnumerable<TSource>, System.Linq.IGrouping<TKey, TSource>
    {
      public TKey Key { get; }

      public System.Collections.Generic.List<TSource> Consecutive { get; } = new System.Collections.Generic.List<TSource>();

      System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
      {
        foreach (var groupItem in Consecutive)
          yield return groupItem;
      }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ((System.Collections.Generic.IEnumerable<TSource>)this).GetEnumerator();

      public ConsecutiveGrouping(TKey key, TSource source)
      {
        Key = key;
        Consecutive.Add(source);
      }
    }
  }
}
