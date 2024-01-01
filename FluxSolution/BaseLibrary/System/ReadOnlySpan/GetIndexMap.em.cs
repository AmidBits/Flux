namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the specified (default if null) <paramref name="equalityComparer"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.ReadOnlySpan<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      System.ArgumentNullException.ThrowIfNull(keySelector);

      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<int>>(equalityComparer);

      var index = 0;

      foreach (var item in source)
      {
        var key = keySelector(item);

        if (map.TryGetValue(key, out System.Collections.Generic.List<int>? value))
          value.Add(index);
        else
          map[key] = new() { index };

        index++;
      }

      return map;
    }
  }
}
