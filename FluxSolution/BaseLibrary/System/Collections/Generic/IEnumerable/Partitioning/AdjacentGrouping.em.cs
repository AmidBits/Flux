namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var g = new Flux.Grouping<TKey, TSource>(keySelector(e.Current), e.Current);

        while (e.MoveNext())
        {
          var key = keySelector(e.Current);

          if (!equalityComparer.Equals(g.Key, key))
          {
            yield return g;

            g = new Flux.Grouping<TKey, TSource>(key);
          }

          g.Add(e.Current);
        }

        if (g.Count > 0)
          yield return g;
      }
    }
  }
}