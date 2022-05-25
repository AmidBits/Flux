namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SourceDifference<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (source.Count == 0 || object.ReferenceEquals(source, target)) // An empty set cannot have any difference it's already empty, or a set minus itself is an empty set.
        return System.Linq.Enumerable.Empty<T>();

      if (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // A set minus an empty set, is the resulting set.
        return source;

      var intersection = new System.Collections.Generic.HashSet<T>(System.Linq.Enumerable.Where(target, t => System.Linq.Enumerable.Contains(source, t)), equalityComparer);

      return System.Linq.Enumerable.Where(source, t => !System.Linq.Enumerable.Contains(intersection, t));
    }
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SourceDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SourceDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
