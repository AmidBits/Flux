namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> TargetDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target))
        return System.Linq.Enumerable.Empty<T>(); // A set minus itself is an empty set.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (!System.Linq.Enumerable.Any(ths))
        return System.Linq.Enumerable.Empty<T>(); // If target is empty, the result must be empty.

      if (System.Linq.Enumerable.Any(source))
        ths.ExceptWith(source.Intersect(ths)); // If source has elements, adjust target.

      return ths;
    }
  }
}
