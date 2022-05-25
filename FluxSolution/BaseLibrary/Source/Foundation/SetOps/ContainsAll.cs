namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection contains all of the elements in the specified target sequence.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // If target is empty, there is nothing to exclude, all is true.
      || !System.Linq.Enumerable.Any(target, t => !System.Linq.Enumerable.Contains(source, t));
  }
}
