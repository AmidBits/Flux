namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new dictionary with all indices of all occurences in the source. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.HashSet<int>> CreateIndexMap<T, TKey>(this System.ReadOnlySpan<T> source, System.Func<T, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.HashSet<int>>(equalityComparer);

      for (var index = 0; index < source.Length; index++)
      {
        var key = keySelector(source[index]);

        if (!map.ContainsKey(key))
          map[key] = new System.Collections.Generic.HashSet<int>();

        map[key].Add(index);
      }

      return map;
    }
  }
}
