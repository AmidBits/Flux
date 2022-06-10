namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Determines whether the source collection contains all of the elements in the specified target sequence.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      if (target is System.Collections.Generic.ICollection<T> tc && !tc.Any())
        return true; // If target is empty, all is included, the result is true.

      if (!source.Any()) // If source is empty, it cannot contain anything, the result is false.
        return false;

      return !target.Any(t => !source.Contains(t));
    }
  }
}
