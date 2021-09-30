namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of elements that are present either in the collection or in the specified sequence, but not both. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetSymmetricDifference<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => System.Linq.Enumerable.Concat(GetDifferenceInSource(source, target, equalityComparer), GetDifferenceInTarget(source, target));
    /// <summary>Creates a new sequence of elements that are present either in the collection or in the specified sequence, but not both. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetSymmetricDifference<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => GetSymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
