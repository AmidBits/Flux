namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => System.Linq.Enumerable.Concat(SourceDifference(source, target, equalityComparer), TargetDifference(source, target));
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
