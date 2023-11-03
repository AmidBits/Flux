using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Indicates whether <paramref name="source"/> contains the same elements (in any order) as <paramref name="target"/>, by Xor'ing the hash codes of all elements.</summary>
    public static bool SequenceEqualHashCodesByXor<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0)) == target.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0));

    /// <summary>Indicates whether <paramref name="source"/> contains the same elements as <paramref name="target"/>, by ordering both and using the built-in SequenceEqual extension method.</summary>
    public static bool SequenceEqualWithOrderBy<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector)
      where TKey : notnull
      => source.OrderBy(keySelector).SequenceEqual(target.OrderBy(keySelector));

    /// <summary>Indicates whether <paramref name="source"/> contains the same grouped keys as <paramref name="target"/>, by grouping and using the built-in SequenceEqual extension method. If <paramref name="disregardCounts"/> is false the counts for each key has to also match.</summary>
    public static bool SequenceEqualWithGroupBy<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Func<TSource, TKey> keySelector, bool disregardCounts = false)
      where TKey : notnull
      => disregardCounts
      ? source.GroupBy(keySelector).Select(g => g.Key).SequenceEqual(target.GroupBy(keySelector).Select(g => g.Key))
      : source.GroupBy(keySelector).Select(g => (g.Key, g.Count())).SequenceEqual(target.GroupBy(keySelector).Select(g => (g.Key, g.Count())));
  }
}
