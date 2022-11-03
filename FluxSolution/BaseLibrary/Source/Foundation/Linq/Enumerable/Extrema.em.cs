namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Locate both the minimum and the maximum element of the sequence. Uses the specified comparer.</summary>
    public static (TSource minItem, int minIndex, TSource maxItem, int maxIndex) Extrema<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var key = keySelector(e.Current);

        var minItem = e.Current;
        var minIndex = 0;
        var minKey = key;

        var maxItem = e.Current;
        var maxIndex = 0;
        var maxKey = key;

        var index = 1;

        while (e.MoveNext())
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

          index++;
        }

        return (minItem, minIndex, maxItem, maxIndex);
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
