namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> TargetDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (ReferenceEquals(source, target)) // A set minus itself is an empty set.
        return new System.Collections.Generic.HashSet<T>(equalityComparer);

      var ths = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      if (!ths.Any()) return new System.Collections.Generic.HashSet<T>(equalityComparer); // If target is empty, the result must be empty.

      var shs = new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      if (!shs.Any()) return ths; // If source is empty, target is the result.

      shs.IntersectWith(ths);
      ths.ExceptWith(shs);

      return ths;
    }
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.HashSet<T> TargetDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => TargetDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
