namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source set is a superset of a specified target set.</summary>
    public static bool IsSupersetOf<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // If target is an empty set then source is a superset.
      || ContainsAll(source, target);
  }
}
