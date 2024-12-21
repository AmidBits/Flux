namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Mode"/></para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TSource, int>> Mode<TSource>(this System.Collections.Generic.IEnumerable<TSource> source/*, out TSource mode, out int count*/)
      => source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<TSource, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);
  }
}
