namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Creates a new <see cref="DataStructures.OrderedDictionary{TKey, TValue}"/> with the specified <paramref name="equalityComparer"/> and all items from <paramref name="source"/> using <paramref name="keySelector"/> and <paramref name="valueSelector"/> for each item.</para>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="valueSelector"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static DataStructures.OrderedDictionary<TKey, TValue> ToOrderedDictionary<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TValue> valueSelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);
      System.ArgumentNullException.ThrowIfNull(valueSelector);

      var od = new DataStructures.OrderedDictionary<TKey, TValue>(equalityComparer ?? System.Collections.Generic.EqualityComparer<TKey>.Default);

      using var e = source.GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
        od.Add(keySelector(e.Current, index), valueSelector(e.Current, index));

      return od;
    }
  }
}
