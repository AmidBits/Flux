namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether <paramref name="source"/> contains the same elements as <paramref name="target"/>, by ordering both and using the built-in SequenceEqual extension method.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static bool OrderedSequenceEqual<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector)
      where TKey : notnull
      => source.OrderBy(keySelector).SequenceEqual(target.OrderBy(keySelector));

    /// <summary>
    /// <para>Indicates whether <paramref name="source"/> contains the same grouped keys as <paramref name="target"/>, by grouping and using the built-in SequenceEqual extension method. If <paramref name="disregardCounts"/> is false the counts for each key has to also match.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="keySelector"></param>
    /// <param name="disregardCounts"></param>
    /// <returns></returns>
    public static bool GroupedSequenceEqual<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector, bool disregardCounts = false)
      where TKey : notnull
      => disregardCounts
      ? source.GroupBy(keySelector).Select(g => g.Key).SequenceEqual(target.GroupBy(keySelector).Select(g => g.Key))
      : source.GroupBy(keySelector).Select(g => (g.Key, g.Count())).SequenceEqual(target.GroupBy(keySelector).Select(g => (g.Key, g.Count())));
  }
}
