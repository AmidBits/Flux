namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException">The <paramref name="source"/> cannot be null.</exception>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if ((target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) || target is null)
        return true; // If target is empty (or null), all is included, the result is true.

      var shs = source is System.Collections.Generic.HashSet<T> hsTemporary
        ? hsTemporary
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any()) // If source is empty, it cannot contain anything, the result is false.
        return false;

      return target.All(t => shs.Contains(t));
    }
  }
}
