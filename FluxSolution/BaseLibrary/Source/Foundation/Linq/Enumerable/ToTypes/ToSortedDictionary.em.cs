namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer);

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        var current = e.Current;

        sd.Add(keySelector(current, index), valueSelector(current, index));

        index++;
      }

      return sd;
    }

    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      return ToSortedDictionary(source, (e, i) => keySelector(e), (e, i) => valueSelector(e), comparer);
    }
  }
}
