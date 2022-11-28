namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> TargetDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target)) // A set minus itself is an empty set.
        return System.Linq.Enumerable.Empty<T>();

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (!ths.Any())
        return System.Linq.Enumerable.Empty<T>(); // If target is empty, the result must be empty.

      if (source.Any()) // If source is empty, target is the result.
        ths.ExceptWith(source.Intersect(ths));

      return ths;
    }
  }
}
