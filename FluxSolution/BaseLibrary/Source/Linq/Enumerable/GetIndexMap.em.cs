namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the specified (default if null) <paramref name="equalityComparer"/>.</summary>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

      var map = System.Linq.Enumerable.ToDictionary(System.Linq.Enumerable.Distinct(target), keySelector, t => new System.Collections.Generic.List<int>(), equalityComparer);

      var index = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        var key = keySelector(e.Current);

        if (map.ContainsKey(key))
          map[key].Add(index);

        index++;
      }

      return map;
    }
  }
}