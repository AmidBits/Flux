namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate the minimum/maximum elements and indices, as evaluated by the <paramref name="keySelector"/>, in <paramref name="source"/>. Uses the specified (default if null) <paramref name="comparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (TSource minItem, int minIndex, TSource maxItem, int maxIndex) Extrema<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var key = keySelector(e.Current);

        var minItem = e.Current;
        var minIndex = 0;
        var minKey = key;

        var maxItem = e.Current;
        var maxIndex = 0;
        var maxKey = key;

        for (var index = 1; e.MoveNext(); index++)
        {
          key = keySelector(e.Current);

          if (comparer.Compare(key, minKey) < 0)
          {
            minItem = e.Current;
            minIndex = index;
            minKey = key;
          }
          if (comparer.Compare(key, maxKey) > 0)
          {
            maxItem = e.Current;
            maxIndex = index;
            maxKey = key;
          }
        }

        return (minItem, minIndex, maxItem, maxIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
