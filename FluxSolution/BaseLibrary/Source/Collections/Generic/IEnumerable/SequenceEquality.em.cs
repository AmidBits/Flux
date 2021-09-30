using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by Xor'ing the hash codes of all elements.</summary>
    public static bool SequenceContentEqualByXor<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0)) == target.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0));

    /// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by ordering and using the built-in SequenceEqual extension method.</summary>
    public static bool SequenceContentEqualOrderBy<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      where T : System.IEquatable<T>
      => source.OrderBy(k => k).Zip(target.OrderBy(k => k), (s, t) => s.Equals(t)).All(b => b);

    public static bool SequenceContentEqualGroupBy<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      where T : notnull 
      => source.GroupBy(v => v).ToSortedDictionary(g => g.Key, g => g.Count()).SequenceEqual(target.GroupBy(v => v).ToSortedDictionary(g => g.Key, g => g.Count()));
  }
}
