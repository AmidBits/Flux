using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var shs = source is System.Collections.Generic.HashSet<T> hsTemporary
        ? hsTemporary
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any())
        return false; // If source is empty, it cannot contain any, so the result is false.

      if ((target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) || target is null) // If target is empty (or null), there is nothing to contain, so the result is false.
        return false;

      return target.Any(t => shs.Contains(t));
    }
  }
}
