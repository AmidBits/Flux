namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      return source.Count == 0 // If source is an empty set, then the symmetric difference is target.
      ? target
      : object.ReferenceEquals(source, target) // A symmetric difference of a set with itself is an empty set.
      ? System.Linq.Enumerable.Empty<T>()
      : (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // If target is an empty set, then the symmetric difference is source.
      ? source
      : System.Linq.Enumerable.Concat(SourceDifference(source, target, equalityComparer), TargetDifference(source, target));
    }

    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
