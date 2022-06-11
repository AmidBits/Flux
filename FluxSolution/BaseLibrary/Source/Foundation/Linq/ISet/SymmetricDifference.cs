namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (ReferenceEquals(source, target)) // A symmetric difference of a set with itself is an empty set.
        return System.Linq.Enumerable.Empty<T>();

      if (!source.Any())
        return target;

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (!ths.Any()) 
        return source;

      var ihs = new System.Collections.Generic.HashSet<T>(source.Intersect(ths));

      return System.Linq.Enumerable.Concat(source.Except(ihs), ths.Except(ihs));
    }
  }
}
