namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a new sequence with the specified items removed and replaced with the specified items.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      comparer ??= System.Collections.Generic.Comparer<TKey>.Default;

      var sd = new System.Collections.Generic.SortedDictionary<TKey, TValue>(comparer);

      using var e = source.GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
        sd.Add(keySelector(e.Current, index), valueSelector(e.Current, index));

      return sd;
    }
  }
}
