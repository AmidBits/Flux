namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> SourceDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (object.ReferenceEquals(source, target)) // A set minus itself is an empty set.
        return new System.Collections.Generic.HashSet<T>(equalityComparer);

      var shs = new System.Collections.Generic.HashSet<T>(source);

      if (shs.Count == 0)// An empty set cannot have any difference it's already empty.
        return new System.Collections.Generic.HashSet<T>(equalityComparer);

      var ths = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      if (ths.Count == 0) // A set minus an empty set, is the resulting set.
        return shs;

      ths.IntersectWith(shs);

      shs.ExceptWith(ths);

      return shs;
    }
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> SourceDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SourceDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
