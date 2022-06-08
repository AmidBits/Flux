namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection contains all of the elements in the specified target sequence.</summary>
    public static bool ContainsAll<T>(System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (target is System.Collections.Generic.ICollection<T> tc && tc.Count == 0)
        return true; // If target is empty, all is included, the result is true.

      var shs = new System.Collections.Generic.HashSet<T>(source);

      if (!shs.Any()) // If source is empty, it cannot contain any, so the result is false.
        return false;

      return !target.Any(t => !source.Contains(t));
    }
  }
}
