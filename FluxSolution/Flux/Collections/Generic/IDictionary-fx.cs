namespace Flux
{
  public static partial class Dictionaries
  {
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TValue, TKey>> FlipPairs<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source)
      => source.GroupBy(kvp => kvp.Value).SelectMany(g => g.Select(gi => System.Collections.Generic.KeyValuePair.Create(gi.Value, gi.Key)));

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> SelectManyMany<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, System.Collections.Generic.IEnumerable<TKey>> keysSelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
    {
      foreach (var item in source)
        foreach (var kvp in keysSelector(item).SelectMany(key => valuesSelector(item), (key, value) => System.Collections.Generic.KeyValuePair.Create(key, value)))
          yield return kvp;
    }

    #region Swich functionality

    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> ToSwitchable<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source)
      where TKey : notnull
    {
      var switchable = new DataStructures.OrderedDictionary<TKey, System.Collections.Generic.IList<TValue>>();

      foreach (var kvp in source)
      {
        if (kvp.Value is System.Collections.Generic.IList<TValue> list)
          switchable.Add(kvp.Key, list);
        else
          switchable.Add(kvp.Key, [kvp.Value]);
      }

      return switchable;
    }

    public static System.Collections.Generic.IDictionary<TValue, System.Collections.Generic.IList<TKey>> Switch<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> source, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      where TKey : notnull
      where TValue : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      var switched = new DataStructures.OrderedDictionary<TValue, System.Collections.Generic.IList<TKey>>(equalityComparer);

      foreach (var kvp in source)
        foreach (var value in kvp.Value)
        {
          if (!switched.TryGetValue(value, out var ilist))
            ilist = new System.Collections.Generic.List<TKey>();
          ilist.Add(kvp.Key);
          switched[value] = ilist;
        }

      return switched;
    }

    public static System.Collections.Generic.IDictionary<TKey, TValue> ToUnswitchable<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.IList<TValue>> source)
      where TKey : notnull
    {
      var unswitchable = new DataStructures.OrderedDictionary<TKey, TValue>();

      foreach (var kvp in source)
        unswitchable.Add(kvp.Key, kvp.Value.Single());

      return unswitchable;
    }

    #endregion // Swich functionality

    /// <summary>
    /// <para>Merge-overwrite <paramref name="source"/> with elements from the <paramref name="other"/> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> or enumerable of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>.</para>
    /// <para>Essentially a merge of the elements in <paramref name="other"/> into <paramref name="source"/>. If keys are equal, <paramref name="other"/> values will overwrite the values in <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="other"></param>
    public static void MergeOverwrite<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
      where TKey : notnull
    {
      foreach (var (key, value) in other)
        source[key] = value;
    }

    public static System.Collections.Generic.IEnumerable<TKey> TryGetKeys<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, TValue value, System.Collections.Generic.IEqualityComparer<TValue>? equalityComparer = null)
      where TKey : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<TValue>.Default;

      return source.Where(kvp => equalityComparer.Equals(kvp.Value, value)).Select(kvp => kvp.Key);
    }
  }
}
