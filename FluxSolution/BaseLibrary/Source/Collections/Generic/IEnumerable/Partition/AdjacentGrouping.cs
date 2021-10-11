namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var g = new Flux.Grouping<TKey, TSource>(keySelector(e.Current), e.Current);

        while (e.MoveNext())
        {
          var key = keySelector(e.Current);

          if (equalityComparer.Equals(g.Key, key))
          {
            g.Add(e.Current);
          }
          else
          {
            yield return g;

            g = new Flux.Grouping<TKey, TSource>(key, e.Current);
          }
        }

        if (g.Count > 0)
          yield return g;
      }
    }
    /// <summary>Creates a new sequence of equal (based on the specified keySelector) consecutive (adjacent) items grouped together as a key and a list. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector)
      => GroupAdjacent(source, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
  }

  ///// <summary>Class used to group adjacent elements in a sequence. Derives from System.Linq.IGrouping<TKey, TSource>.</summary>
  //internal class AdjacentGrouping<TKey, TElement>
  //  : System.Collections.Generic.IEnumerable<TElement>, System.Linq.IGrouping<TKey, TElement>
  //{
  //  private readonly System.Collections.Generic.List<TElement> m_elements = new System.Collections.Generic.List<TElement>();

  //  public int Count
  //    => m_elements.Count;

  //  public void Add(TElement element)
  //    => m_elements.Add(element);

  //  // IEnumerable
  //  System.Collections.Generic.IEnumerator<TElement> System.Collections.Generic.IEnumerable<TElement>.GetEnumerator()
  //    => m_elements.GetEnumerator();
  //  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  //    => m_elements.GetEnumerator();

  //  // IGrouping
  //  public TKey Key { get; }

  //  public AdjacentGrouping(TKey key, TElement source)
  //  {
  //    Key = key;
  //    m_elements.Add(source);
  //  }

  //  /// <summary>Creates a new sequence of equal (based on the specified keySelector) adjacent (consecutive) items grouped together as a key and a list. Uses the specified equality comparer.</summary>
  //  public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TElement>> GroupAdjacent(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> comparer)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));
  //    if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
  //    if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

  //    using var e = source.GetEnumerator();

  //    if (e.MoveNext())
  //    {
  //      var ag = new Flux.Grouping<TKey, TElement>(keySelector(e.Current), e.Current);

  //      while (e.MoveNext())
  //      {
  //        var key = keySelector(e.Current);

  //        if (comparer.Equals(ag.Key, key))
  //        {
  //          ag.Add(e.Current);
  //        }
  //        else
  //        {
  //          yield return ag;

  //          ag = new Flux.Grouping<TKey, TElement>(key, e.Current);
  //        }
  //      }

  //      if (ag.Count > 0)
  //        yield return ag;
  //    }
  //  }
  //  /// <summary>Creates a new sequence of equal (based on the specified keySelector) consecutive (adjacent) items grouped together as a key and a list. Uses the default equality comparer.</summary>
  //  public static System.Collections.Generic.IEnumerable<System.Linq.IGrouping<TKey, TElement>> GroupAdjacent(System.Collections.Generic.IEnumerable<TElement> source, System.Func<TElement, TKey> keySelector)
  //    => GroupAdjacent(source, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
  //}
}
