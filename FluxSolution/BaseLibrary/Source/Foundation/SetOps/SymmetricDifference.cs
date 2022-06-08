namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (ReferenceEquals(source, target)) // A symmetric difference of a set with itself is an empty set.
        return new System.Collections.Generic.HashSet<T>(equalityComparer);

      var shs = new System.Collections.Generic.HashSet<T>(source, equalityComparer);
      var ths = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      if (!shs.Any()) return ths;
      if (!ths.Any()) return shs;

      var ihs = new System.Collections.Generic.HashSet<T>(shs.Intersect(ths));

      return System.Linq.Enumerable.Concat(shs.Except(ihs), ths.Except(ihs));
    }

    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
