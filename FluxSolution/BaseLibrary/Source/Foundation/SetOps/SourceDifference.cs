namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> SourceDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (ReferenceEquals(source, target)) // A set minus itself is an empty set.
        return new System.Collections.Generic.HashSet<T>(equalityComparer);

      var shs = new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any()) return new System.Collections.Generic.HashSet<T>(equalityComparer); // If source is empty, the result must be empty.

      var ths = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      if (!ths.Any()) return shs; // If target is empty, source is the result.

      ths.IntersectWith(shs);
      shs.ExceptWith(ths);

      return shs;
    }
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> SourceDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SourceDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
