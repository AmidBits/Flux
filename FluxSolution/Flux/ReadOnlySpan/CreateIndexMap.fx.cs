namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> CreateIndexMap<T, TKey>(this System.ReadOnlySpan<T> source, System.Func<T, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);

      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<int>>(equalityComparer ?? System.Collections.Generic.EqualityComparer<TKey>.Default);

      for (var index = 0; index < source.Length; index++)
      {
        var key = keySelector(source[index]);

        if (!map.TryGetValue(key, out var value))
        {
          value = [];

          map[key] = value;
        }

        value.Add(index);
      }

      return map;
    }
  }
}
