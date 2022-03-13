using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Results in a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in decreasing count of a appearance in the sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> Mode<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector)
      => System.Linq.Enumerable.OrderByDescending(System.Linq.Enumerable.Select(System.Linq.Enumerable.GroupBy(source, keySelector), g => new System.Collections.Generic.KeyValuePair<TKey, int>(g.Key, System.Linq.Enumerable.Count(g))), kvp => kvp.Value);
  }
}
