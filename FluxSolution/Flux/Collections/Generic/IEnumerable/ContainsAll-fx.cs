namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    /// <remarks>This extension method leverages (and re-use) the type <see cref="System.Collections.Generic.ISet{T}"/> for speed.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if ((target is System.Collections.Generic.ICollection<T> tct && tct.Count == 0) || (target is System.Collections.ICollection tc && tc.Count == 0) || target is null) // If target is empty (or null), all is included, the result is true.
        return true;

      var shs = source is System.Collections.Generic.ISet<T> hsTemporary // For speed...
        ? hsTemporary // Re-use the ISet<T> if available.
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default); // Otherwise, create a HashSet<T>.

      if (shs.Count == 0) // If source is empty, it cannot contain anything, the result is false.
        return false;

      return target.All(shs.Contains);
    }
  }
}
