//namespace Flux
//{
//  public static partial class Enumerable
//  {
//    /// <summary>Creates a new dictionary with all indices of all target occurences in the source. Uses the specified (default if null) <paramref name="equalityComparer"/>.</summary>
//    /// <exception cref="System.ArgumentNullException"/>
//    public static System.Collections.Generic.IDictionary<TKey, System.Collections.Generic.List<int>> GetIndexMap<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey>? equalityComparer = null)
//      where TKey : notnull
//    {
//      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

//      equalityComparer ??= System.Collections.Generic.EqualityComparer<TKey>.Default;

//      var map = System.Linq.Enumerable.ToDictionary(System.Linq.Enumerable.Distinct(target), keySelector, t => new System.Collections.Generic.List<int>(), equalityComparer);

//      var index = 0;

//      foreach (var item in source)
//      {
//        var key = keySelector(item);

//        if (map.ContainsKey(key))
//          map[key].Add(index);

//        index++;
//      }

//      return map;
//    }
//  }
//}
