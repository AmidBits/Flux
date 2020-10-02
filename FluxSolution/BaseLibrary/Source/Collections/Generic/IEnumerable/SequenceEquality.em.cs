using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by ordering and using the built-in SequenceEqual extension method.</summary>
    public static bool SequenceContentEqualOrderBy<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.OrderBy(e => e).SequenceEqual(target.OrderBy(e => e));

    /// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by Xor'ing the hash codes of all elements.</summary>
    public static bool SequenceContentEqualByXor<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Xor(e => e?.GetHashCode() ?? 0) == target.Xor(e => e?.GetHashCode() ?? 0);
  }
}
