namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SourceDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target)) // A set minus itself is an empty set.
        return System.Linq.Enumerable.Empty<T>();

      if (!source.Any())
        return System.Linq.Enumerable.Empty<T>(); // If source is empty, the result must be empty.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (ths.Any()) // If target is empty, source is the result.
        return source.Except(ths.Intersect(source)); // Return the IEnumerable<> rather than changing the source and then returning it.

      return source;
    }
  }
}
