using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var shs = source is System.Collections.Generic.HashSet<T> tmp ? tmp : new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any())
        return false; // If source is empty, it cannot contain any, so the result is false.

      if (subset is System.Collections.Generic.ICollection<T> ss && !ss.Any()) // If target is empty, there is nothing to contain, so the result is false.
        return false;

      return subset.Any(t => shs.Contains(t));
    }
    /// <summary>Returns whether the source contains any of the items in subset.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAny(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
