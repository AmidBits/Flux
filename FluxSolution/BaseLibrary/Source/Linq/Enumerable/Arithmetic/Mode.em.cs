using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TSource, int>> Mode<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, out TSource mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<TSource, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }
  }
}
