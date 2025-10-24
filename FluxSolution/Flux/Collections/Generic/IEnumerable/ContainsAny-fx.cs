namespace Flux
{
  public static partial class IEnumerables
  {
    #region ContainsAny

    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    /// <remarks>This extension method leverages (and re-use) the type <see cref="System.Collections.Generic.ISet{T}"/> for speed.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      var shs = source is System.Collections.Generic.ISet<T> hsTemporary // For speed...
        ? hsTemporary // Re-use the ISet<T> if available.
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default); // Otherwise, create a HashSet<T>.

      if (
        shs.Count == 0 // If source is empty, it cannot contain any, so the result is false.
        || (target is System.Collections.Generic.ICollection<T> tct && tct.Count == 0) || (target is System.Collections.ICollection tc && tc.Count == 0) || target is null // If target is empty (or null), there is nothing to contain, so the result is false.
      )
        return false;

      return target.Any(shs.Contains);
    }

    #endregion
  }
}
