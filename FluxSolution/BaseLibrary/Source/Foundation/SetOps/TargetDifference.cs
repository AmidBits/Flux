namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set.</summary>
    public static System.Collections.Generic.IEnumerable<T> TargetDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target) 
      => (source.Count == 0) // A set minus an empty set, is the resulting set.
      ? target
      : (object.ReferenceEquals(source, target) || target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // A set minus itself is an empty set, or an empty set cannot have any difference it's already empty.
      ? System.Linq.Enumerable.Empty<T>()
      : System.Linq.Enumerable.Where(target, t => !source.Contains(t));
  }
}
