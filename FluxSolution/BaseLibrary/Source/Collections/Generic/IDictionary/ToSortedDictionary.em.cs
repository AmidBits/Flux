namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer);

      var index = 0;

      foreach (var item in source)
      {
        sd.Add(keySelector(item, index), valueSelector(item, index));

        index++;
      }

      return sd;
    }
    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector)
      where TKey : notnull
      => ToSortedDictionary(source, keySelector, valueSelector, System.Collections.Generic.Comparer<TKey>.Default);

    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));

      return ToSortedDictionary(source, (e, i) => keySelector(e), (e, i) => valueSelector(e), comparer);
    }
    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector)
      where TKey : notnull
      => ToSortedDictionary(source, keySelector, valueSelector, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
