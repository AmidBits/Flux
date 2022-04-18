using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Results in a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in decreasing count of a appearance in the sequence. Uses the specified equality comparer.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> Mode<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
      => source.GroupBy(keySelector, equalityComparer).Select(g => new System.Collections.Generic.KeyValuePair<TKey, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);
    /// <summary>Results in a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in decreasing count of a appearance in the sequence. Uses the default equality comparer.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> Mode<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector)
      => Mode(source, keySelector, System.Collections.Generic.EqualityComparer<TKey>.Default);
  }
}
