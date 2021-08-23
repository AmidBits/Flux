namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Get all indices of target occurences in the source. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var map = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<int>>();

      foreach (var targetItem in target)
        map.Add(keySelector(targetItem), new System.Collections.Generic.List<int>());

      var index = 0;

      foreach (var sourceItem in source)
      {
        var sourceKey = keySelector(sourceItem);

        foreach (var targetItem in map.Keys)
        {
          if (equalityComparer.Equals(sourceKey, targetItem))
          {
            if (!map.ContainsKey(sourceKey))
              map.Add(sourceKey, new System.Collections.Generic.List<int>());

            map[sourceKey].Add(index);

            break;
          }
        }

        index++;
      }

      return map;
    }
    /// <summary>Get all indices of target occurences in the source. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector)
      where TKey : notnull
      => GetIndexMap(source, target, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
  }
}
