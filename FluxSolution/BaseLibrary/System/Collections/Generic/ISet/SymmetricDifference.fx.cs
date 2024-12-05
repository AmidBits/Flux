namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target))
        return System.Linq.Enumerable.Empty<T>(); // A symmetric difference of a set with itself is an empty set.

      if (!source.Any())
        return target; // If source is empty, target is the result.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (ths.Count == 0)
        return source; // If target is empty, source is the result.

      var ihs = new System.Collections.Generic.HashSet<T>(source.Intersect(ths));

      return System.Linq.Enumerable.Concat(source.Except(ihs), ths.Except(ihs));
    }
  }
}
