namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new dictionary with all keys (by <paramref name="keySelector"/>) and indices of all occurences in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.HashSet<int>> CreateIndexMap<T, TKey>(this System.ReadOnlySpan<T> source, System.Func<T, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.HashSet<int>>(equalityComparer);

      for (var index = 0; index < source.Length; index++)
      {
        var key = keySelector(source[index]);

        if (!map.TryGetValue(key, out var value))
        {
          value = new();

          map[key] = value;
        }

        value.Add(index);
      }

      return map;
    }
  }
}
