namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (subset is System.Collections.Generic.ICollection<T> ss && !ss.Any())
        return true; // If target is empty, all is included, the result is true.

      var shs = source is System.Collections.Generic.HashSet<T> tmp ? tmp : new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any()) // If source is empty, it cannot contain anything, the result is false.
        return false;

      return !subset.Any(t => !source.Contains(t));
    }
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAll(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
