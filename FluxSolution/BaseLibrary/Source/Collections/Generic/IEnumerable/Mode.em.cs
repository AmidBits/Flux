using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Results in a new sequence consisting of mode (i.e. the most frequent or common) elements in decreasing count of a appearance in the sequence.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> Mode<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector)
      where TKey : notnull
      => source.Histogram(keySelector).OrderByDescending(h => h.Value);
  }
}
