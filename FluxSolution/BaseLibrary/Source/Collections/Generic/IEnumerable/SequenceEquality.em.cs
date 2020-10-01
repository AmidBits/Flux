using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Indicates the equality of two sequences by ordering and using the built-in SequenceEqual extension method.</summary>
    public static bool SequenceContentEqualOrderBy<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.OrderBy(e => e).SequenceEqual(target.OrderBy(e => e));

    public static bool SequenceContentEqualByXor<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Xor(e => e?.GetHashCode() ?? 0) == target.Xor(e => e?.GetHashCode() ?? 0);
  }
}
