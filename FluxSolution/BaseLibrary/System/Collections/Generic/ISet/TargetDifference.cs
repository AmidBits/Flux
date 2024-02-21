namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> TargetDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target))
        return System.Linq.Enumerable.Empty<T>(); // A set minus itself is an empty set.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (ths.Count == 0)
        return System.Linq.Enumerable.Empty<T>(); // If target is empty, the result must be empty.

      if (source.Count > 0)
        ths.ExceptWith(source.Intersect(ths)); // If source has elements, adjust target.

      return ths;
    }
  }
}
